using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(menuName = "ResourceData/Create Resource Data", fileName = "ResourceData")]
    public class ResourceData : ScriptableObject
    {
        [field: SerializeField] public List<ItemData> ItemDatas { get; private set; }
    }
}
