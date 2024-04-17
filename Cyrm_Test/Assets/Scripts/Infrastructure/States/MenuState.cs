using Data.StaticData;
using Infrastructure.Factory;

namespace Infrastructure.States
{
    public class MenuState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly IUIFactory _uiFactory;
        private readonly IStaticDataService _staticDataService;

        public MenuState(GameStateMachine stateMachine, IUIFactory uiFactory, IStaticDataService staticDataService)
        {
            _gameStateMachine = stateMachine;
            _uiFactory = uiFactory;
            _staticDataService = staticDataService;
        }

        public void Enter()
        {
            _uiFactory.CreateMenu(_gameStateMachine, _staticDataService);
        }
        public void Exit()
        {
        }

       
    }
}