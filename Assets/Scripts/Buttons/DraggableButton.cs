using UnityEngine;
using UnityEngine.EventSystems;
using Assets.Scripts.Misc;
using Assets.Scripts.Tile.Behavior;
using Assets.Scripts.World.Tile;

namespace Assets.Scripts.Buttons
{
    public class DraggableButton : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public static GameObject itemBeingDragged;
        public static Vector3 startPosition;
        public static SelectedBehavior selectedBehavior = null;

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (!DraggingEnabled())
            {
                return;
            }

            if (itemBeingDragged != null)
            {
                return;
            }

            itemBeingDragged = gameObject;
            startPosition = gameObject.transform.position;
        }

        private bool DraggingEnabled()
        {
            if (Globals.InputMode == InputMode.Buttons)
            {
                return false;
            }
            if (FindObjectOfType<Runner>().IsRunning())
            {
                return false;
            }

            return true;
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (!DraggingEnabled())
            {
                return;
            }
            if (!gameObject == itemBeingDragged)
            {
                return;
            }

            transform.position = Input.mousePosition;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (!hit.transform.gameObject.GetComponent<Behaviors>().GetBehavior(gameObject.name).Available)
                {
                    return;
                }

                var selection = hit.transform.gameObject.GetComponent<SelectedBehavior>();
                if (selection != null && selectedBehavior != selection)
                {
                    RemoveTempSelection();
                    selection.SetPreview(gameObject.name);
                    FindObjectOfType<Mirror>().SetMirrorPreview(selection.gameObject, gameObject.name);
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
                FindObjectOfType<Mirror>().ClearMirrorPreview(selectedBehavior.gameObject);
                selectedBehavior.ClearPreview();
            }

            selectedBehavior = null;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (!DraggingEnabled())
            {
                return;
            }

            if (!gameObject == itemBeingDragged)
            {
                return;
            }

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                GetComponent<Animation>().Play("ButtonEnter");

                if (hit.transform.gameObject.GetComponent<Behaviors>().GetBehavior(gameObject.name).Available)
                {
                    var selection = hit.transform.gameObject.GetComponent<SelectedBehavior>();
                    if (selection != null)
                    {
                        selection.SelectBehavior(gameObject.name);
                        FindObjectOfType<Mirror>().SetMirror(selectedBehavior.gameObject, gameObject.name);
                    }
                }
            }

            transform.position = startPosition;
            itemBeingDragged = null;
            RemoveTempSelection();
        }
    }
}
