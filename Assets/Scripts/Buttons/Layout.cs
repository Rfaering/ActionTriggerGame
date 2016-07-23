using Assets.Scripts.Misc;
using Assets.Scripts.Utils;
using Assets.Scripts.World.Tile;
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
            this.gameObject.SetActive(true);
            gameObject.name = behavior.Name;
            this.GetComponent<Image>().color = Color.gray;
        }
    }
}
