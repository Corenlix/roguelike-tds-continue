using TMPro;
using UnityEngine;

namespace Items
{
    public class ItemPickTip : MonoBehaviour
    {
        [SerializeField] private TextMeshPro _tipText;
        
        public void ShowTip(Item item)
        {
            transform.parent = null;
            gameObject.SetActive(true);
            _tipText.text = $"[E] {item.Name}";
            _tipText.transform.position = item.transform.position;
        }

        public void HideTip()
        {
            gameObject.SetActive(false);
        }

        private void Start()
        {
            HideTip();
        }
    }
}
