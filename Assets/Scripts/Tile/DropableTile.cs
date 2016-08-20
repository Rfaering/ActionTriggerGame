using Assets.Scripts.Buttons;
using Assets.Scripts.Tile.Behavior;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Tile
{
    public class DropableTile : MonoBehaviour, IDropHandler
    {
        public void OnDrop(PointerEventData eventData)
        {
            if (DraggableButton.itemBeingDragged != null)
            {
                GetComponent<SelectedBehavior>().SelectBehavior(DraggableButton.itemBeingDragged.name);
            }
        }
    }
}
