using Data;
using Model;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class MarketPopupView : MonoBehaviour
    {
        [SerializeField] private Button _closeButton;
        [SerializeField] private Button _sellButton;
        [SerializeField] private Button _choiceButton;
        [SerializeField] private TMP_Text _priceText;
        [SerializeField] private TMP_Text _countItemsText;
        [SerializeField] private Image _icon;
        
        private WorldModel _worldModel;
        private int _currentNumber;
        private Resource _currentResources;
        private const string PriceName = "Цена";

        public void Construct(WorldModel worldModel)
        {
            _worldModel = worldModel;
        }

        private void OnEnable()
        {
            _closeButton.onClick.AddListener(CloseHandler);
            _sellButton.onClick.AddListener(SellItemHandler);
            _choiceButton.onClick.AddListener(ChoiceResourceHandler);
        }
        
        private void OnDisable()
        {
            _closeButton.onClick.RemoveListener(CloseHandler);
            _sellButton.onClick.RemoveListener(SellItemHandler);
            _choiceButton.onClick.RemoveListener(ChoiceResourceHandler);
        }
        
        public void Show()
        {
            gameObject.SetActive(true);
            if(_worldModel.LootModel.ItemDatas == null || _worldModel.LootModel.ItemDatas.Count <= 0)
                return;
            SetItem();
            if(_worldModel.LootModel.ItemDatas.Count > 1)
                _currentNumber++;
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
        
        private void CloseHandler()
        {
            Hide();
        }
        
        private void ChoiceResourceHandler()
        {
            if(_worldModel.LootModel.ItemDatas == null || _worldModel.LootModel.ItemDatas.Count <= 0)
                return;
            
            SetItem();

            if (_currentNumber >= _worldModel.LootModel.ItemDatas.Count - 1)
            {
                _currentNumber = 0;
                return;
            }
            
            _currentNumber++;
        }

     
        private void SellItemHandler()
        {
            if(_worldModel.LootModel.ItemDatas == null || _worldModel.LootModel.ItemDatas.Count <= 0)
                return;

            if (_currentResources.Amount <= 0)
            {
                _currentNumber = 0;
                ChoiceResourceHandler();
                return;
            }
            
            _worldModel.LootModel.Decrease(_currentResources, 1);
            _worldModel.CoinModel.Add(_currentResources.ItemData.Price);
            _countItemsText.text = _currentResources.Amount.ToString();
        }
        
        private void SetItem()
        {
            _currentResources = _worldModel.LootModel.ItemDatas[_currentNumber];
            _priceText.text = $"{PriceName} {_currentResources.ItemData.Price.ToString()}";
            _icon.sprite = _currentResources.ItemData.Icon;
            _countItemsText.text = _currentResources.Amount.ToString();
        }

    } 
}
