using Assets.Scripts.Misc;
using UnityEngine;
using UnityEngine.EventSystems;
using Assets.Scripts.Tile;
using Assets.Scripts.Tile.Behavior;
using System.Linq;

namespace Assets.Scripts.Buttons
{
    public class ButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        void Start()
        {
            if (!(Globals.InputMode == InputMode.Buttons))
            {
                this.enabled = false;
            }

            this.enabled = false;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            var behaviors = FindObjectsOfType<Selection>();
            foreach (var item in behaviors.Where(x => x.Selected))
            {
                item.GetComponent<SelectedBehavior>().ClearPreview();
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            var behaviors = FindObjectsOfType<Selection>();

            foreach (var item in behaviors.Where(x => x.Selected))
            {
                item.GetComponent<SelectedBehavior>().SetPreview(gameObject.name);
            }
        }
    }
}
