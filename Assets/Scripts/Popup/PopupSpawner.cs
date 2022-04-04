using Unity.Mathematics;
using UnityEngine;

namespace Popup
{
    public class PopupSpawner : MonoBehaviour
    {
        public static  PopupSpawner Instance => _instance;
    
        [SerializeField] private DamagePopup _prefabPopup;
        private static PopupSpawner _instance;

        private void Awake()
        {
            if(_instance)
                Destroy(_instance);
            _instance = this;
        }
   
        public void SpawnPopup(Vector2 position, int damage)
        {
            DamagePopup popup = Instantiate(_prefabPopup, position, quaternion.identity);
            popup.Init(damage);
        }
    }
}
