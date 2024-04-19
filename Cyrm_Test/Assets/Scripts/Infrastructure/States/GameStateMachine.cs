using System;
using System.Collections.Generic;
using Data.StaticData;
using Infrastructure.Factory;
using Infrastructure.Services;

namespace Infrastructure.States
{
    public class GameStateMachine: IGameStateMachine
    {
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly Dictionary<Type, IExitableState> _states;
        private IExitableState _activeState;

        public GameStateMachine(SceneLoader sceneLoader, AllServices services , ICoroutineRunner coroutineRunner)
        {
            _coroutineRunner = coroutineRunner;
            _states = new Dictionary<Type, IExitableState>
            {
                [typeof(BootstrapState)] = new BootstrapState(this, sceneLoader, services, _coroutineRunner),
                [typeof(LoadLevelState)] = new LoadLevelState(this, sceneLoader, services.Single<IGameFactory>(),services.Single<IUIFactory>(), services.Single<IStaticDataService>()),
                [typeof(LoadProgressState)] = new LoadProgressState(this, services.Single<IPersistentProgressService>(), services.Single<ISaveLoadService>(), sceneLoader),
                [typeof(MenuState)] = new MenuState(this, services.Single<IUIFactory>(), services.Single<IStaticDataService>()),
                [typeof(GameLoopState)] = new GameLoopState(),
            };
        }
    
        public void Enter<TState>() where TState : class, IState
        {
            IState state = ChangeState<TState>();
            state.Enter();
        }

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
        {
            IPayloadedState<TPayload> state = ChangeState<TState>();
            state.Enter(payload);
        }

        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            _activeState?.Exit();
            TState state = GetState<TState>();
            _activeState = state;
            return state;
        }

        private TState GetState<TState>() where TState : class, IExitableState => 
            _states[typeof(TState)] as TState;
    }
}