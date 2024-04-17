using Data;
using Model;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class ItemView : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _countText;

        public ItemData.ItemType ItemType => _itemType;
        private ItemData.ItemType _itemType;
        
        public void Init(Resource resource)
        {
            _icon.sprite = resource.ItemData.Icon;
            _countText.text = resource.Amount.ToString();
            _itemType = resource.ItemData.Type;
        }
        
        public void UpdateCount(int count)
        {
            _countText.text = count.ToString();
        }
    }
}
