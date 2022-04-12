using TMPro;
using UnityEngine;

namespace Items
{
    public class InteractTip : MonoBehaviour
    {
        [SerializeField] private TextMeshPro _tipText;
        
        public void ShowTip(IInteractable interactable)
        {
            transform.parent = null;
            transform.localScale = Vector3.one;
            gameObject.SetActive(true);
            _tipText.text = $"[E] {interactable.InteractText}";
            _tipText.transform.position = interactable.Position;
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
