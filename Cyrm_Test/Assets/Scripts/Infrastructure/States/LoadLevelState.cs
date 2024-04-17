using System.Threading.Tasks;
using Data.StaticData;
using Infrastructure.Factory;
using UnityEngine;

namespace Infrastructure.States
{
    public class LoadLevelState: IPayloadedState<string>
  {
    private readonly GameStateMachine _stateMachine;
    private readonly SceneLoader _sceneLoader;
    private readonly IGameFactory _gameFactory;
    private readonly IUIFactory _uiFactory;
    private readonly IStaticDataService _staticDataService;

    private Vector3[] _position = new []{new Vector3(-1.58f, -3.25f, 0), new Vector3(1.08f,-3.25f, 0), new Vector3(-0.21f, -1.53f, 0)};
    public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader,
      IGameFactory gameFactory, IUIFactory uiFactory, IStaticDataService staticDataService)
    {
      _stateMachine = stateMachine;
      _sceneLoader = sceneLoader;
      _gameFactory = gameFactory;
      _uiFactory = uiFactory;
      _staticDataService = staticDataService;
    }

    public void Enter(string sceneName)
    {
        _sceneLoader.Load(sceneName, onLoaded: OnLoaded);
    }

    public void Exit()
    {
    }

    private async void OnLoaded()
    {
      await InitGameWorld();
      
      _stateMachine.Enter<GameLoopState>();
    }
    
    private async Task InitGameWorld()
    {
      await InitUIRoot();
      await InitUI();
      await InitMarket();
      await InitBuilding();
      await InitResourcesBuild();
    }
    
    private async Task InitUIRoot() => 
        await _uiFactory.CreateRootUI();
    
    private async Task InitUI()
    {
      await _uiFactory.CreateCoinView();
      await _uiFactory.CreateItemPanelView();
    }

    private async Task InitMarket()
    {
      var marketView = await _uiFactory.CreateMarketView();
      var market = await _gameFactory.CreateMarket();
      market.Init(marketView);
    }
    
    private async Task InitBuilding()
    {
      var processingBuildingPopupView = await _uiFactory.CreateProcessingBuildingView();
      var processingBuilding = await _gameFactory.CreateProcessingBuilding();
      processingBuilding.Init(processingBuildingPopupView);
    }

    private async Task InitResourcesBuild()
    {
      for (int i = 0; i < _staticDataService.LevelModel.ResourceBuildsCount; i++)
      {
        var resourceBuildingPopupView= await _uiFactory.CreateResourceBuildingView();
        var resourceBuilding = await _gameFactory.CreateResourceBuilding(_position[i]);
        resourceBuilding.Init(resourceBuildingPopupView);
      }
    }
  }
}