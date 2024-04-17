using System.Threading.Tasks;
using Infrastructure.Services;
using UnityEngine;

namespace Infrastructure.AssetsManagement
{
    public interface IAssets : IService
    {
        Task<GameObject> Instantiate(string address);
        Task<GameObject> Instantiate(string address, Vector3 at);
        Task<GameObject> InstantiateWithParent(string address, Transform parent);
    }
}