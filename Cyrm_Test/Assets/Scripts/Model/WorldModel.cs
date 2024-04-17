using System;
using Model;
using UnityEngine.Serialization;

namespace Data
{
    [Serializable]
    public class WorldModel
    {
        public CoinModel CoinModel;
        public LootModel LootModel;
        
        public WorldModel()
        {
            CoinModel = new CoinModel();
            LootModel = new LootModel();
        }
    }
}