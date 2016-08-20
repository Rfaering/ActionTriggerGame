using UnityEngine;
using UnityEngine.EventSystems;
using Assets.Scripts.Misc;
using Assets.Scripts.Tile.Behavior;
using Assets.Scripts.Tile;

namespace Assets.Scripts.Buttons
{
    public class DraggableButton : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public static GameObject itemBeingDragged;
        public static Vector3 startPosition;
        public static SelectedBehavior selectedBehavior = null;
        public static string oldSelection = null;

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (Globals.InputMode == InputMode.Buttons)
            {
                return;
            }

            itemBeingDragged = gameObject;
            startPosition = gameObject.transform.position;
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (Globals.InputMode == InputMode.Buttons)
            {
                return;
            }
            transform.position = Input.mousePosition;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (!hit.transform.gameObject.GetComponent<Visual>().Editable)
                {
                    return;
                }
                
                var selection = hit.transform.gameObject.GetComponent<SelectedBehavior>();
                if (selection != null && selectedBehavior != selection)
                {
                    RemoveTempSelection();
                    oldSelection = selection.SelectBehavior(gameObject.name);
                    selectedBehavior = selection;
                }
            }
            else if (selectedBehavior != null)
            {
                RemoveTempSelection();
            }
        }

        private void RemoveTempSelection()
        {
            if (selectedBehavior != null)
            {
                selectedBehavior.RemoveSelection(gameObject.name);
                selectedBehavior.SelectBehavior(oldSelection);
            }

            oldSelection = null;
            selectedBehavior = null;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (Globals.InputMode == InputMode.Buttons)
            {
                return;
            }
            transform.position = startPosition;
            itemBeingDragged = null;
            selectedBehavior = null;
        }
    }
}
