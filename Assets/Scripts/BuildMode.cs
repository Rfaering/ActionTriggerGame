using UnityEngine;

public class BuildMode : MonoBehaviour
{
    public BuilderMode RuntimeMode
    {
        get { return Globals.BuildMode; }
        set
        {
            Globals.BuildMode = value;
            SetNewRuntimeMode(value);
        }
    }

    public void Awake()
    {
        RuntimeMode = Globals.BuildMode;
    }

    void Update()
    {
        if (!GlobalProperties.IsOverlayPanelOpen)
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                RuntimeMode = RuntimeMode == BuilderMode.DesignMode ? BuilderMode.Running : BuilderMode.DesignMode;
                var level = FindObjectOfType<LoadLevel>();
                level.LoadCurrentLevel();
            }
        }
    }

    private void SetNewRuntimeMode(BuilderMode value)
    {

        if (value == BuilderMode.DesignMode)
        {
            Globals.InputMode = InputMode.Buttons;
        }
        if (value == BuilderMode.Running)
        {
            Globals.InputMode = InputMode.Connect;
        }

        /*FindObjectOfType<CanvasMenu>()
            .GetComponentInChildren<DesignPanelVisibility>(true)
            .SetVisibility(value == BuilderMode.DesignMode);*/

        foreach (var visibility in FindObjectOfType<Runner>().GetComponentsInChildren<Visibility>(true))
        {
            visibility.UpdateVisiblity();
        }

        UnSelectAll();
        GetComponent<Runner>().StopRunning();
    }

    private void UnSelectAll()
    {
        TileSelection[] selections = GetComponentsInChildren<TileSelection>();
        foreach (var item in selections)
        {
            item.Selected = false;
        }

        GameObject.Find("Canvas")
            .GetComponent<CanvasMenu>()
            .DisableAllButtons();
    }
}
