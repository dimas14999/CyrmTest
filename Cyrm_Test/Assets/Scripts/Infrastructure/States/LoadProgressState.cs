using Data;
using Infrastructure.Services;
using UnityEngine;

namespace Infrastructure.States
{
    public class LoadProgressState : IState
    {
        private const string Menu = "Menu";
        
        private readonly GameStateMachine _gameStateMachine;
        private readonly IPersistentProgressService _progressService;
        private readonly ISaveLoadService _saveLoadService;
        private readonly SceneLoader _sceneLoader;

        public LoadProgressState(GameStateMachine gameStateMachine, IPersistentProgressService progressService, ISaveLoadService saveLoadService, SceneLoader sceneLoader)
        {
            _gameStateMachine = gameStateMachine;
            _progressService = progressService;
            _saveLoadService = saveLoadService;
            _sceneLoader = sceneLoader;
        }

        public void Enter()
        {
            LoadProgressOrInitNew();
            _sceneLoader.Load(Menu, onLoaded: EnterLoadMenu);
        }

        private void EnterLoadMenu()
        {
            _gameStateMachine.Enter<MenuState>();
        }

        public void Exit()
        {
      
        }

        private void LoadProgressOrInitNew()
        {
            _progressService.Progress = _saveLoadService.LoadProgress() ?? NewProgress();
        }

        private GameProgress NewProgress()
        {
            var progress = new GameProgress();
            return progress;    
        } 
    }
}