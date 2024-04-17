using System;
using Data;
using Model;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace Logic
{
    public class ChoiceResource : MonoBehaviour
    {
        [SerializeField] private Button _choiceButton;
        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _countText;

        public Resource Resource => _resource;
        public event Action OnSwitch;
        private int _currentNumber;
        private Resource _resource;

        private WorldModel _worldModel;

        public void Init(WorldModel worldModel)
        {
            _worldModel = worldModel;
        }

        private void OnEnable()
        {
            _choiceButton.onClick.AddListener(ChoiceResourceHandler);
        }

        private void OnDisable()
        {
            _choiceButton.onClick.RemoveListener(ChoiceResourceHandler);
        }

        public void UpdateInformation(int amount)
        {
            _countText.text = amount.ToString();
            _currentNumber = 0;
        }

        private void ChoiceResourceHandler()
        {
            _resource = _worldModel.LootModel.ItemDatas[_currentNumber];
            _icon.sprite = _resource.ItemData.Icon;
            _countText.text = _resource.Amount.ToString();
            OnSwitch?.Invoke();
            if (_currentNumber >= _worldModel.LootModel.ItemDatas.Count - 1)
            {
                _currentNumber = 0;
                return;
            }
            
            _currentNumber++;
        }
    }
}
