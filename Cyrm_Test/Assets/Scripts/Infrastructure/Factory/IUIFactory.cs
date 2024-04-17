using System.Threading.Tasks;
using Data.StaticData;
using Infrastructure.Services;
using Infrastructure.States;
using UnityEngine;
using View;

namespace Infrastructure.Factory
{
    public interface IUIFactory : IService
    {
        void CreateMenu(GameStateMachine gameStateMachine, IStaticDataService staticDataService);
        Task CreateRootUI();
        Task<ItemView> CreateItem(Transform parent);
        Task<CoinView> CreateCoinView();
        Task<ItemPanelView> CreateItemPanelView();
        Task<MarketPopupView> CreateMarketView();
        Task<BasePopupView> CreateProcessingBuildingView();
        Task<BasePopupView> CreateResourceBuildingView();
        Task<WinWindowView> CreateWinWindowView(GameStateMachine gameStateMachine, SceneLoader sceneLoader);
    }
}