using System.Threading.Tasks;
using Data;
using Data.StaticData;
using Infrastructure.AssetsManagement;
using Infrastructure.Services;
using Infrastructure.States;
using UnityEngine;
using View;

namespace Infrastructure.Factory
{
    public class UIFactory : IUIFactory
    {
        private readonly IAssets _assets;
        private readonly IPersistentProgressService _persistentProgressService;
        private Transform _uiRoot;
        public UIFactory(IAssets assets, IPersistentProgressService persistentProgressService)
        {
            _assets = assets;
            _persistentProgressService = persistentProgressService;
        }
        
        public async Task CreateRootUI()
        {
            var root = await _assets.Instantiate(AssetsAddressUI.UIRoot);
            _uiRoot = root.transform;
        }

        public async void CreateMenu(GameStateMachine gameStateMachine, IStaticDataService staticDataService)
        {
            GameObject menu = await _assets.Instantiate(AssetsAddressUI.Menu);
            menu.GetComponent<MenuView>().Init(gameStateMachine, staticDataService);
        }

        public async Task<ItemView> CreateItem(Transform parent)
        {
            var item = await _assets.InstantiateWithParent(AssetsAddressUI.ItemIcon, parent);
            return item.GetComponent<ItemView>();
        }

        public async Task<CoinView> CreateCoinView()
        {
            var coin = await _assets.InstantiateWithParent(AssetsAddressUI.Coin, _uiRoot);
            CoinView coinView = coin.GetComponent<CoinView>();
           coinView.Construct(_persistentProgressService.Progress.WorldModel);
           return coinView;
        }

        public async Task<ItemPanelView> CreateItemPanelView()
        {
            var itemPanel = await _assets.InstantiateWithParent(AssetsAddressUI.ItemsPanel, _uiRoot);
            var itemPanelView = itemPanel.GetComponent<ItemPanelView>();
            itemPanelView.Construct(_persistentProgressService.Progress.WorldModel, this);
            return itemPanelView;
        }
        
        public async Task<MarketPopupView> CreateMarketView()
        {
            var market = await _assets.InstantiateWithParent(AssetsAddressUI.MarketContainer, _uiRoot);
            MarketPopupView marketPopupView = market.GetComponent<MarketPopupView>();
            marketPopupView.Construct(_persistentProgressService.Progress.WorldModel);
            return marketPopupView;
        }

        public async Task<BasePopupView> CreateProcessingBuildingView()
        {
            var processingBuilding = await _assets.InstantiateWithParent(AssetsAddressUI.ProcessingBuildingContainer, _uiRoot);
            var processingBuildingPopupView = processingBuilding.GetComponent<ProcessingBuildingPopupView>();
            processingBuildingPopupView.Construct(_persistentProgressService.Progress.WorldModel);
            return processingBuildingPopupView;
        }

        public async Task<BasePopupView> CreateResourceBuildingView()
        {
            var resourceBuilding = await _assets.InstantiateWithParent(AssetsAddressUI.ResourceBuildingContainer, _uiRoot);
            return resourceBuilding.GetComponent<ResourceBuildingPopupView>();
        }

        public async Task<WinWindowView> CreateWinWindowView(GameStateMachine gameStateMachine, SceneLoader sceneLoader)
        {
            var winWindow = await _assets.InstantiateWithParent(AssetsAddressUI.WinWindow, _uiRoot);
            var winWindowView = winWindow.GetComponent<WinWindowView>();
            winWindowView.Construct(gameStateMachine, sceneLoader);
            return winWindowView;
        }
    }
}