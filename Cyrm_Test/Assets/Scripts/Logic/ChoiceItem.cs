using System;
using Data;
using UnityEngine;
using UnityEngine.UI;

namespace Logic
{
    public class ChoiceItem : MonoBehaviour
    {
        [SerializeField] private Button _choiceButton;
        [SerializeField] private Image _icon;
        [SerializeField] private ResourceData _resourceData;

        public ItemData CurrentItem => _currentItem;
        public event Action OnSwitch;
        private int _currentNumber;
        private ItemData _currentItem;

        private void OnEnable()
        {
            _choiceButton.onClick.AddListener(ChoiceItemHandler);
        }

        private void OnDisable()
        {
            _choiceButton.onClick.RemoveListener(ChoiceItemHandler);
        }

        private void ChoiceItemHandler()
        {
            _currentItem = _resourceData.ItemDatas[_currentNumber];
            _icon.sprite = _currentItem.Icon;
            OnSwitch?.Invoke();
            if (_currentNumber >= _resourceData.ItemDatas.Count - 1)
            {
                _currentNumber = 0;
                return;
            }
            
            _currentNumber++;
        }
    }
}
