using System.Threading.Tasks;
using Infrastructure.AssetsManagement;
using Infrastructure.Services;
using Logic;
using UnityEngine;
using View;

namespace Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssets _assets;
        private readonly IPersistentProgressService _persistentProgressService;

        public GameFactory(IAssets assets, IPersistentProgressService persistentProgressService)
        {
            _assets = assets;
            _persistentProgressService = persistentProgressService;
        }
        
        public async Task<Market> CreateMarket()
        {
            var market = await _assets.Instantiate(AssetsAddress.Market);
            return market.GetComponent<Market>();
        }

        public async Task<BaseBuilding> CreateProcessingBuilding()
        {
            var processingBuilding = await _assets.Instantiate(AssetsAddress.ProcessingBuilding);
            BaseBuilding building = processingBuilding.GetComponent<ProcessingBuilding>();
            building.Construct(_persistentProgressService.Progress.WorldModel);
            return building;
        }

        public async Task<BaseBuilding> CreateResourceBuilding(Vector3 at)
        {
            var resourceBuilding = await _assets.Instantiate(AssetsAddress.ResourceBuilding, at);
            BaseBuilding building = resourceBuilding.GetComponent<ResourceBuilding>();
            building.Construct(_persistentProgressService.Progress.WorldModel);
            return building;
        }
    }
}