using System.Collections.Generic;
using System.Threading.Tasks;
using Infrastructure.Services;
using Logic;
using UnityEngine;

namespace Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        Task<Market> CreateMarket();
        Task<BaseBuilding> CreateProcessingBuilding();
        Task<BaseBuilding> CreateResourceBuilding(Vector3 at);
    }  
}