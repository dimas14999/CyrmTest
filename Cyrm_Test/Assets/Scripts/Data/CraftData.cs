using System;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(menuName = "Craft Data/Create Craft Data", fileName = "CraftData")]
    public class CraftData : ScriptableObject
    {
        [field: SerializeField] public Craft[] Craft { get; private set; }
    }

    [Serializable]
    public class Craft
    {
        [field: SerializeField] public ItemData[] Items { get; private set; }
        [field: SerializeField] public ItemData EqualItem { get; private set; }
    }
}
