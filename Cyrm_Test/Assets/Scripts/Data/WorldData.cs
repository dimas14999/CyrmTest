using UnityEngine;

namespace Data
{
    [CreateAssetMenu(menuName = "World Data/Create World Data", fileName = "WorldData")]
    public class WorldData : ScriptableObject
    {
        [field: SerializeField] public int MaxCoin { get; private set; }
    }
}
