using Infrastructure;
using Infrastructure.States;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class WinWindowView : MonoBehaviour
    {
        [SerializeField] private Button _backToMenu;

        private GameStateMachine _gameStateMachine;
        private SceneLoader _sceneLoader;

        public void Construct(GameStateMachine gameStateMachine, SceneLoader sceneLoader)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _backToMenu.onClick.AddListener(WinHandler); 
        }
        
        private void OnDisable()
        {
            _backToMenu.onClick.RemoveListener(WinHandler); 
        }

        private void WinHandler()
        {
            _sceneLoader.Load("Menu", _gameStateMachine.Enter<LoadProgressState>);
        }
    }
}
