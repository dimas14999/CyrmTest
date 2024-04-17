using System;
using Data;
using Model;
using TMPro;
using UnityEngine;

namespace View
{
    public class CoinView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        [SerializeField] private WorldData _worldData;
        
        private WorldModel _worldModel;
        
        public void Construct(WorldModel worldModel)
        {
            _worldModel = worldModel;
            _worldModel.CoinModel.Changed += UpdateCoin;
            UpdateCoin();
        }

        private void OnDisable()
        {
            _worldModel.CoinModel.Changed -= UpdateCoin;
        }

        private void UpdateCoin()
        {
            _text.text = $"{_worldModel.CoinModel.Collected} / {_worldData.MaxCoin}";
        }
    }
}
