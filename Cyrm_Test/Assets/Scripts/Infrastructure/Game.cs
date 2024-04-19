using Infrastructure.Services;
using Infrastructure.States;
using UnityEngine;

namespace Infrastructure
{
    public class Game : MonoBehaviour
    {
        public readonly GameStateMachine GameStateMachine;

        public Game(ICoroutineRunner coroutineRunner) => 
            GameStateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), AllServices.Container, coroutineRunner);
    }
}
