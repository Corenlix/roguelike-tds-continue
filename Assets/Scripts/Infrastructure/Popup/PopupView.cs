using TMPro;
using UnityEngine;

namespace Popup
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
