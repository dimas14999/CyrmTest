using System.Collections.Generic;
using Data;
using Infrastructure.Factory;
using Model;
using UnityEngine;

namespace View
{
    public class ItemPanelView : MonoBehaviour
    {
        [SerializeField] private Transform _container;
        
        private WorldModel _worldModel;
        private UIFactory _uiFactory;
        private List<ItemView> _itemViews;

        public void Construct(WorldModel worldModel, UIFactory uiFactory)
        {
            _worldModel = worldModel;
            _uiFactory = uiFactory;
            _itemViews = new List<ItemView>();
            LoadProgress();
            _worldModel.LootModel.OnChanged += UpdateItem;
            _worldModel.LootModel.OnAdd += AddItem;
        }

        private void LoadProgress()
        {
            if (_worldModel.LootModel.ItemDatas != null || _worldModel.LootModel.ItemDatas.Count > 0)
            {
                foreach (var item in _worldModel.LootModel.ItemDatas)
                {
                    AddItem(item);
                }
            }
        }

        private void UpdateItem(Resource resource)
        {
            var item = _itemViews.Find(r => r.ItemType == resource.ItemData.Type);
            if (item != null)
            {
                item.UpdateCount(resource.Amount);
                if (resource.Amount <= 0)
                {
                    Destroy(item.gameObject);
                    _itemViews.Remove(item);
                }
            }
        }

        private async void AddItem(Resource resource)
        {
           var item = await _uiFactory.CreateItem(_container);
           item.Init(resource);
           _itemViews.Add(item);
        }
    }
}
