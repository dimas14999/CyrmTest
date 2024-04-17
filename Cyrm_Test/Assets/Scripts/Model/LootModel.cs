using System;
using System.Collections.Generic;
using Data;

namespace Model
{
    [Serializable]
    public class LootModel
    {
        public List<Resource> ItemDatas;
        public event Action<Resource> OnChanged;
        public event Action<Resource> OnAdd;
        public event Action<Resource> OnRemove;

        public LootModel()
        {
            ItemDatas = new List<Resource>();
        }
        
        public void Collect(ItemData itemData, int amount)
        {
            Resource existingResource = ItemDatas.Find(r => r.ItemData.Type == itemData.Type);

            if(existingResource != null) {
                existingResource.Amount += amount;
                OnChanged?.Invoke(existingResource);
            } else {
                Resource newResource = new Resource { ItemData = itemData, Amount = amount };
                ItemDatas.Add(newResource);
                OnAdd?.Invoke(newResource);
            }
        }

        public void Decrease(Resource itemData, int amount)
        {
            Resource existingResource = ItemDatas.Find(r => r.ItemData.Type == itemData.ItemData.Type);

            if(existingResource != null) {
                existingResource.Amount -= amount;
                OnChanged?.Invoke(existingResource);
                if (existingResource.Amount <= 0)
                {
                    OnRemove?.Invoke(existingResource);
                    ItemDatas.Remove(existingResource);
                }
            } 
        }
    }
    
    [Serializable]
    public class Resource
    {
        public ItemData ItemData;
        public int Amount;
    }
}