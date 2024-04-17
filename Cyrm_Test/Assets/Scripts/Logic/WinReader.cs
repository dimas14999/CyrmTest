using Data;
using Infrastructure.Services;
using UnityEngine;

namespace Logic
{
    public class WinReader : MonoBehaviour
    {
        [SerializeField] private WorldData _worldData;
        private IWinService _winService;
        private IPersistentProgressService _persistentProgressService;
        private void Awake()
        {
            _winService = AllServices.Container.Single<IWinService>();
            _persistentProgressService = AllServices.Container.Single<IPersistentProgressService>();
            _persistentProgressService.Progress.WorldModel.CoinModel.Changed += WinChecker;
        }

        private void OnDisable()
        {
            _persistentProgressService.Progress.WorldModel.CoinModel.Changed -= WinChecker;
        }

        private void WinChecker()
        {
            if (_persistentProgressService.Progress.WorldModel.CoinModel.Collected >= _worldData.MaxCoin)
            {
                _winService.Open();
            }
        }
    }
}
