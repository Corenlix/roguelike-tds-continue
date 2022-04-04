using TMPro;
using UnityEngine;

namespace Popup
{
   public class DamagePopup : MonoBehaviour
   {
      [SerializeField] private TextMeshPro _damageText;
      
      public void Init(int damage)
      {
         _damageText.text = damage.ToString();
      }
      
      private void Destroy()
      {
         Destroy(gameObject);
      }
   }
}
