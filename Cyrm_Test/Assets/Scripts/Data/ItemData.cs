using UnityEngine;

namespace Data
{
    [CreateAssetMenu(menuName = "Item Data/Create Item Data", fileName = "ItemData")]
    public class ItemData : ScriptableObject
    {
        [field: SerializeField] public Sprite Icon { get; private set; }
        [field: SerializeField] public int Price { get; private set; }
        [field: SerializeField] public ItemType Type { get; private set; }

        public enum ItemType
        {
            None,
            Iron,
            Wood,
            Stone,
            Hammers,
            Pitchfork,
            Drill
        }
    }

  
}
