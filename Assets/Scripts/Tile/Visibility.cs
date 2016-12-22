
using UnityEngine;

public class Visibility : MonoBehaviour
{
    public bool IsVisible;
    public GameObject visibilityIcon;

    private GameObject background;
    private GameObject foreground;

    public void Start()
    {
        visibilityIcon = transform
                    .Find("Visible")
                    .gameObject;

        foreground = gameObject.transform.FindChild("Foreground").gameObject;
        background = gameObject.transform.FindChild("Background").gameObject;
    }

    public void Update()
    {
        UpdateInput();
        UpdateVisiblity();
    }

    private void UpdateInput()
    {
        if (GlobalProperties.IsInBuildMode() &&
            !GlobalProperties.IsOverlayPanelOpen &&
            GetComponent<TileSelection>().Selected &&
            Input.GetKeyDown(KeyCode.V))
        {
            IsVisible = !IsVisible;
        }

    }

    public void UpdateVisiblity()
    {
        if (visibilityIcon == null)
        {
            return;
        }

        if (IsVisible)
        {
            visibilityIcon.SetActive(false);
        }
        else
        {
            if (GlobalProperties.IsInBuildMode())
            {
                if (!isActiveAndEnabled)
                {
                    gameObject.SetActive(true);
                }

                visibilityIcon.SetActive(true);
            }
            else
            {
                background.SetActive(false);
                foreground.SetActive(false);
                visibilityIcon.SetActive(false);
            }
        }
    }
}


