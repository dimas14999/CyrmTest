using Infrastructure.Factory;
using Infrastructure.States;
using View;

namespace Infrastructure.Services
{
    public class WinService : IWinService
    {
        private readonly IUIFactory _uiFactory;
        private readonly ISaveLoadService _saveLoadService;
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private WinWindowView _windowView;

        public WinService(IUIFactory uiFactory, ISaveLoadService saveLoadService, GameStateMachine gameStateMachine, SceneLoader sceneLoader)
        {
            _uiFactory = uiFactory;
            _saveLoadService = saveLoadService;
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
        }
        
        public async void Open()
        {
            _saveLoadService.DeleteProgress();
            await _uiFactory.CreateWinWindowView(_gameStateMachine, _sceneLoader);
        }
    }
}