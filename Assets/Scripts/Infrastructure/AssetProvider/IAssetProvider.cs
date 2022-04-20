using UnityEngine;

namespace Infrastructure.AssetProvider
{
    public interface IAssetProvider : IService
    {
        T Instantiate<T>(string path, Vector3 at) where T : MonoBehaviour;
        GameObject Instantiate(string path, Vector3 at);
        T Instantiate<T>(string path) where T : MonoBehaviour;
        GameObject Instantiate(string path);
        T Load<T>(string path) where T : Object;
    }
}