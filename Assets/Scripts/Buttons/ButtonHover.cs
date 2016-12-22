using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;

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
        var behaviors = FindObjectsOfType<TileSelection>();
        foreach (var item in behaviors.Where(x => x.Selected))
        {
            item.GetComponent<SelectedBehavior>().ClearPreview();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        var behaviors = FindObjectsOfType<TileSelection>();

        foreach (var item in behaviors.Where(x => x.Selected))
        {
            item.GetComponent<SelectedBehavior>().SetPreview(gameObject.name);
        }
    }
}
