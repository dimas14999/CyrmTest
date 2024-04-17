using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Infrastructure.AssetsManagement
{
    public class AssetProvider : IAssets
    {
        public void Init()
        {
            Addressables.InitializeAsync();
        }
        public Task<GameObject> Instantiate(string address)
        {
            return Addressables.InstantiateAsync(address).Task;
        }
        
        public Task<GameObject> Instantiate(string address, Vector3 at)
        {
            return Addressables.InstantiateAsync(address, at, Quaternion.identity).Task;
        }

        public Task<GameObject> InstantiateWithParent(string address, Transform parent)
        {
            return Addressables.InstantiateAsync(address, parent).Task;
        }
    }
}