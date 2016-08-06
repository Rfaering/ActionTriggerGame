using Assets.Scripts.Misc;
using Assets.Scripts.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Buttons
{
    public class Layout : MonoBehaviour
    {
        public void SetLayoutBasedOnBehavior(BehaviorBase behavior)
        {
            behavior.ClearUI(gameObject);
            behavior.UpdateUI(gameObject);
            gameObject.SetActive(true);
            gameObject.name = behavior.Name;
            GetComponent<ButtonEnabled>().ContentEnabled = false;
        }
    }
}
