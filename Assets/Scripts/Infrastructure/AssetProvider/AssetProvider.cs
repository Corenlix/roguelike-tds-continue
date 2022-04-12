using UnityEngine;
using Zenject;

namespace Infrastructure.AssetProvider
{
    public class AssetProvider : IAssetProvider
    {
        private readonly DiContainer _diContainer;
        
        public AssetProvider(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }
        
        public T Instantiate<T>(string path, Vector3 at) where T : Object
        {
            var prefab = Resources.Load<T>(path);
            return _diContainer.InstantiatePrefabForComponent<T>(prefab, at, Quaternion.identity, null);
        }

        public T Instantiate<T>(string path) where T : Object
        {
            var prefab = Resources.Load<T>(path);
            return _diContainer.InstantiatePrefabForComponent<T>(prefab);
        }
    }
}