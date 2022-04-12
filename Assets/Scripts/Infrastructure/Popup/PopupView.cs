using TMPro;
using UnityEngine;

namespace Infrastructure.Popup
{
   public class PopupView : MonoBehaviour
   {
      public PopupType Type;
      [SerializeField] private TextMeshPro _text;
      
      public void Init(string popupText)
      {
         _text.text = popupText;
      }
      
      private void Destroy()
      {
         Destroy(gameObject);
      }
   }
}
