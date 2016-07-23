using UnityEngine;
using Assets.Scripts.Utils;
using Assets.Scripts.Buttons;

namespace Assets.Scripts.Tile
{
    public class Selection : MonoBehaviour
    {
        private Position _position;

        void Start()
        {
            _position = GetComponent<Position>();
        }


        public void Update()
        {
            if (!GetComponent<Animation>().isPlaying && !_position.Death)
            {
                UpdateColor();
            }

            if (!GlobalProperties.IsOverlayPanelOpen())
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                    {
                        if (hit.transform.gameObject == this.gameObject)
                        {
                            Selected = true;
                        }
                        else
                        {
                            // Only deselect unit if left shift is not clicked and you are during runtime
                            if (!Input.GetKey(KeyCode.LeftShift) || !GlobalProperties.IsInBuildMode())
                            {
                                Selected = false;
                            }
                        }
                    }
                }

                if (Selected)
                {
                    if (Input.GetKeyDown(KeyCode.UpArrow) && GetComponent<Position>().Up != null)
                    {
                        GiveTileSelection(GetComponent<Position>().Up);
                    }
                    if (Input.GetKeyDown(KeyCode.DownArrow) && GetComponent<Position>().Down != null)
                    {
                        GiveTileSelection(GetComponent<Position>().Down);
                    }
                    if (Input.GetKeyDown(KeyCode.LeftArrow) && GetComponent<Position>().Left != null)
                    {
                        GiveTileSelection(GetComponent<Position>().Left);
                    }
                    if (Input.GetKeyDown(KeyCode.RightArrow) && GetComponent<Position>().Right != null)
                    {
                        GiveTileSelection(GetComponent<Position>().Right);
                    }
                }


                if (_gotSelectionFromKeyboardNavigation)
                {
                    Selected = true;
                    _gotSelectionFromKeyboardNavigation = false;
                }
            }

        }

        private void GiveTileSelection(GameObject gameObject)
        {
            gameObject.GetComponent<Selection>()._gotSelectionFromKeyboardNavigation = true;
            Selected = false;
        }

        #region Selected
        // This delays the selection to after keyboard check, abit of a hack not to change selection twice for one press
        public bool _gotSelectionFromKeyboardNavigation;

        private bool _selected;

        public bool Selected
        {
            get { return _selected; }
            set
            {
                _selected = value;
                UpdateMenu();
            }
        }

        private void UpdateMenu()
        {
            if (Selected)
            {
                GameObject.Find("Canvas")
                    .GetComponent<CanvasMenu>()
                    .RenderOptionsForGameObject(this.gameObject);
            }
        }

        private void UpdateColor()
        {
            if (Selected)
            {
                gameObject.transform.Find("Foreground").GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f);


            }
            else
            {
                if (!_position.Death)
                {
                    gameObject.transform.Find("Foreground").GetComponent<SpriteRenderer>().color = new Color(0.7f, 0.7f, 0.7f);
                }
            }
        }
        #endregion
    }
}


