using Assets.Scripts.Misc;
using UnityEngine;

namespace Assets.Scripts.Buttons
{
    public class Layout : MonoBehaviour
    {
        public void SetLayoutBasedOnBehavior(BehaviorBase behavior)
        {
            behavior.ClearUI(gameObject);
            behavior.UpdateUI(gameObject, false);
            gameObject.SetActive(true);
            gameObject.name = behavior.Name;
            if( Globals.InputMode == InputMode.Buttons)
            {
                GetComponent<ButtonEnabled>().ContentEnabled = false;
            } else
            {
                GetComponent<ButtonEnabled>().ContentEnabled = true;
            }
            
        }
    }
}
