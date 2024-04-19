using Data;
using Logic;
using Model;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class ProcessingBuildingPopupView : BasePopupView
    {
        [SerializeField] private ChoiceResource[] _choiceResources;
        [SerializeField] private Image _equalsItemImage;
        [SerializeField] private CraftData _craftData;

        private ItemData _itemData;
        private WorldModel _worldModel;
        
        public void Init(WorldModel worldModel)
        {
            _worldModel = worldModel;
            foreach (var choiceResource in _choiceResources)
            {
                choiceResource.Init(_worldModel);
            }
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            foreach (var choiceResource in _choiceResources)
            {
                choiceResource.OnSwitch += GetCurrentItem;
            }
            _fillProgressService.OnCompleteProcess += OnComplete;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            foreach (var choiceResource in _choiceResources)
            {
                choiceResource.OnSwitch -= GetCurrentItem;
            }
            _fillProgressService.OnCompleteProcess -= OnComplete;
        }

        protected override void StartProcessHandler()
        {
            if(_itemData != null && (_choiceResources[0].Resource.Amount > 0 && _choiceResources[1].Resource.Amount > 0))
                _fillProgressService.Process(_itemData);
        }
        
        private void OnComplete(ItemData obj)
        {
            foreach (var choiceResource in _choiceResources)
                DecreaseResource(choiceResource, 1);

            if (_choiceResources[0].Resource.Amount <= 0 || _choiceResources[1].Resource.Amount <= 0)
                _fillProgressService.CancelFill();
        }

        private void DecreaseResource(ChoiceResource choiceResource, int amount)
        {
            _worldModel.LootModel.Decrease(choiceResource.Resource, amount);
            choiceResource.UpdateInformation(choiceResource.Resource.Amount);
        }

        private void GetCurrentItem()
        {
            foreach (Craft craft in _craftData.Craft)
            {
                if (_choiceResources[0].Resource == null || _choiceResources[1].Resource == null)
                {
                    return;
                }
                if (ItemsMatchCraft(_choiceResources[0].Resource.ItemData, _choiceResources[1].Resource.ItemData, craft))
                {
                    _equalsItemImage.sprite = craft.EqualItem.Icon;
                    _itemData = craft.EqualItem;
                    return;
                }
                
                _equalsItemImage.sprite = null;
                _itemData = null;
            }
        } 
     
        private bool ItemsMatchCraft(ItemData firstItem, ItemData secondItem, Craft craft)
        {
            return firstItem == craft.Items[0] && secondItem == craft.Items[1] ||
                   firstItem == craft.Items[1] && secondItem == craft.Items[0];
        }
    }
}
