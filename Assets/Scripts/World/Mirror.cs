using Assets.Scripts.Misc;
using Assets.Scripts.Tile;
using Assets.Scripts.Tile.Behavior;
using UnityEngine;

public class Mirror : MonoBehaviour
{
    public bool IsMirrorEnabled;

    public void Awake()
    {
        IsMirrorEnabled = false;
    }

    public void Update()
    {
        if (IsMirrorEnabled)
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
        if (Input.GetKeyDown(KeyCode.M) && Globals.BuildMode == BuilderMode.DesignMode)
        {
            IsMirrorEnabled = !IsMirrorEnabled;
        }
    }

    public void SetMirror(GameObject gameObject, string behaviorName)
    {
        if (IsMirrorEnabled && Globals.BuildMode == BuilderMode.Running)
        {
            var position = gameObject.GetComponent<Position>();
            var horizontalMirror = position.HorizontalMirror;
            horizontalMirror.GetComponent<SelectedBehavior>().SelectBehavior(SwapBehavior(behaviorName));
        }
    }

    public void SetMirrorPreview(GameObject gameObject, string behaviorName)
    {
        if (IsMirrorEnabled && Globals.BuildMode == BuilderMode.Running)
        {
            var position = gameObject.GetComponent<Position>();
            var horizontalMirror = position.HorizontalMirror;
            horizontalMirror.GetComponent<SelectedBehavior>().SetPreview(SwapBehavior(behaviorName));
        }
    }

    public void ClearMirrorPreview(GameObject gameObject)
    {
        if (IsMirrorEnabled && Globals.BuildMode == BuilderMode.Running)
        {
            var position = gameObject.GetComponent<Position>();
            var horizontalMirror = position.HorizontalMirror;
            horizontalMirror.GetComponent<SelectedBehavior>().ClearPreview();
        }
    }

    public void RemoveSelection(GameObject gameObject, string behaviorName)
    {
        if (IsMirrorEnabled && Globals.BuildMode == BuilderMode.Running)
        {
            var position = gameObject.GetComponent<Position>();
            var horizontalMirror = position.HorizontalMirror;
            horizontalMirror.GetComponent<SelectedBehavior>().RemoveSelection(SwapBehavior(behaviorName));
        }
    }

    public string SwapBehavior(string behaviorName)
    {
        if (behaviorName == "Left")
        {
            return "Right";
        }
        else if (behaviorName == "Right")
        {
            return "Left";
        }
        else if (behaviorName == "LeftUp")
        {
            return "UpRight";
        }
        else if (behaviorName == "UpRight")
        {
            return "LeftUp";
        }
        else if (behaviorName == "RightDown")
        {
            return "DownLeft";
        }
        else if (behaviorName == "DownLeft")
        {
            return "RightDown";
        }
        return behaviorName;
    }
}
