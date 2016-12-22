using UnityEngine;
using UnityEngine.UI;

public class TileSelection : MonoBehaviour
{
    private WaterState _waterState;

    void Start()
    {
        _waterState = GetComponent<WaterState>();
    }


    public void Update()
    {
        if (Globals.InputMode != InputMode.Buttons)
        {
            return;
        }

        if (!GetComponent<Animation>().isPlaying && !_waterState.Watered)
        {
            UpdateColor();
        }

        if (!GlobalProperties.IsOverlayPanelOpen)
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
        gameObject.GetComponent<TileSelection>()._gotSelectionFromKeyboardNavigation = true;
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
        if (Selected && !FindObjectOfType<Runner>().IsRunning())
        {
            gameObject.transform.Find("Foreground").GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f);
        }
        else
        {
            gameObject.transform.Find("Foreground").GetComponent<Image>().color = new Color(0.7f, 0.7f, 0.7f);
        }
    }
    #endregion
}
