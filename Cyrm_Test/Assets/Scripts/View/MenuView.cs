using System;
using System.Collections.Generic;
using Data.StaticData;
using Infrastructure.States;
using Model;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class MenuView : MonoBehaviour
    {
        [SerializeField] private Button _oneBuildingButton;
        [SerializeField] private Button _twoBuildingButton;
        [SerializeField] private Button _threeBuildingButton;
        [SerializeField] private Button _startButton;

        private GameStateMachine _gameStateMachine;
        private IStaticDataService _staticDataService;

        private int _currentNumber;
        private Image _lastSelectedImage;
        private Dictionary<Button, int> _countButtons = new Dictionary<Button, int>();

        public void Init(GameStateMachine gameStateMachine, IStaticDataService staticDataService)
        {
            _gameStateMachine = gameStateMachine;
            _staticDataService = staticDataService;
            AssignButton(_oneBuildingButton, 1);
            AssignButton(_twoBuildingButton, 2);
            AssignButton(_threeBuildingButton, 3);
            
            _startButton.interactable = false;
            _startButton.onClick.AddListener(StartGame);
        }

        private void AssignButton(Button button, int count)
        {
            _countButtons.Add(button, count);
            button.onClick.AddListener(() => UpdateSelection(button));
        }

        private void UpdateSelection(Button selectedButton)
        {
            int count = _countButtons[selectedButton];

            if (_currentNumber == count)
            {
                _currentNumber = 0;
                _startButton.interactable = false;
            }
            else
            {
                _currentNumber = count;
                _startButton.interactable = true;

                if (_lastSelectedImage != null)
                {
                    _lastSelectedImage.color = Color.white;
                }
            }

            Image buttonImage = selectedButton.GetComponent<Image>();
            buttonImage.color = (_currentNumber == count) ? Color.green : Color.white;
            _lastSelectedImage = buttonImage;
        }

        private void OnDisable()
        {
            foreach (var pair in _countButtons)
            {
                pair.Key.onClick.RemoveListener(() => UpdateSelection(pair.Key));
            }
            
            _startButton.onClick.RemoveListener(StartGame);
        }
        
        private void StartGame()
        {
            _staticDataService.LevelModel = NewProgress();
            _gameStateMachine.Enter<LoadLevelState, string>("Game");
        }
        
        private LevelModel NewProgress()
        {
            var progress = new LevelModel()
            {
                ResourceBuildsCount = _currentNumber
            };
      
            return progress;    
        }
    }
}