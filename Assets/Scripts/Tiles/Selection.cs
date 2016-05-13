using UnityEngine;
using Assets.Scripts.UI;

public class Selection : MonoBehaviour {    
    public void Update()
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
                    Selected = false;
                }
            }
        }

        if ( Selected )
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
            UpdateColor(value);
        }
    }

    private void UpdateColor(bool value)
    {
        var backgroundMaterial = gameObject.transform.GetChild(0).GetComponent<Renderer>().material;

        if (value)
        {
            backgroundMaterial.color = new Color(0.5f, 0.2f, 0.2f);

            GameObject.Find("Canvas")
                .GetComponent<CanvasMenu>()
                .RenderOptionsForGameObject(this.gameObject);
        }
        else
        {
            backgroundMaterial.color = new Color(1.0f, 1.0f, 1.0f);
        }
    }
    #endregion
}

