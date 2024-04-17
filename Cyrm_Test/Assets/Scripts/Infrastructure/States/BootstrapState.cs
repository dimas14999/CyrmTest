using Data;
using Data.StaticData;
using Infrastructure.AssetsManagement;
using Infrastructure.Factory;
using Infrastructure.Services;
using UnityEngine;

namespace Infrastructure.States
{
    public class BootstrapState : IState
    { 
      private const string Initial = "Initial";
      private readonly GameStateMachine _stateMachine;
      private readonly SceneLoader _sceneLoader;
      private readonly AllServices _services;

      public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader, AllServices services)
      {
        _stateMachine = stateMachine;
        _sceneLoader = sceneLoader;
        _services = services;

        RegisterServices();
      }


      public void Enter() =>
        _sceneLoader.Load(Initial, onLoaded: EnterLoadLevel);

      public void Exit()
      {
      }

      private void EnterLoadLevel() =>
        _stateMachine.Enter<LoadProgressState>();

      private void RegisterServices()
      {
        _services.RegisterSingle<IGameStateMachine>(_stateMachine);
        RegisterAssetProvider();
        _services.RegisterSingle<IPersistentProgressService>(new PersistentProgressService());
        _services.RegisterSingle<IStaticDataService>(new StaticDataService());
        _services.RegisterSingle<IUIFactory>(new UIFactory(_services.Single<IAssets>(), _services.Single<IPersistentProgressService>()));
        _services.RegisterSingle<IGameFactory>(new GameFactory(_services.Single<IAssets>(),_services.Single<IPersistentProgressService>()));
        _services.RegisterSingle<ISaveLoadService>(new SaveLoadService(_services.Single<IPersistentProgressService>()));
        _services.RegisterSingle<IWinService>(new WinService(_services.Single<IUIFactory>(), _services.Single<ISaveLoadService>(), _stateMachine,_sceneLoader));
      }
      
      private void RegisterAssetProvider()
      {
        AssetProvider assetProvider = new AssetProvider();
        assetProvider.Init();
        _services.RegisterSingle<IAssets>(assetProvider);
      }
    }
}
