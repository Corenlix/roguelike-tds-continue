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
        
        public T Instantiate<T>(string path, Vector3 at) where T : MonoBehaviour
        {
            var prefab = Resources.Load<T>(path);
            return _diContainer.InstantiatePrefabForComponent<T>(prefab, at, Quaternion.identity, null);
        }

        public GameObject Instantiate(string path, Vector3 at)
        {
            var prefab = Resources.Load(path);
            return _diContainer.InstantiatePrefab(prefab, at, Quaternion.identity, null);
        }

        public T Instantiate<T>(string path) where T : MonoBehaviour
        {
            return Instantiate<T>(path, Vector3.zero);
        }

        public GameObject Instantiate(string path)
        {
            return Instantiate(path, Vector3.zero);
        }

        public T Load<T>(string path) where T : Object => Resources.Load<T>(path);
    }
}