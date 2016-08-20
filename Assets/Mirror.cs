using Assets.Scripts.Tile;
using Assets.Scripts.Tile.Behavior;
using UnityEngine;

public class Mirror : MonoBehaviour
{
    public bool IsMirrorEnabled;

    public void Start()
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
        if (Input.GetKeyDown(KeyCode.M))
        {
            IsMirrorEnabled = !IsMirrorEnabled;
        }
    }

    public void SetMirror(GameObject gameObject, string behaviorName)
    {
        if (IsMirrorEnabled)
        {
            var position = gameObject.GetComponent<Position>();
            var horizontalMirror = position.HorizontalMirror;
            horizontalMirror.GetComponent<SelectedBehavior>().SelectBehavior(SwapBehavior(behaviorName));
        }
    }

    public void RemoveSelection(GameObject gameObject, string behaviorName)
    {
        if (IsMirrorEnabled)
        {
            var position = gameObject.GetComponent<Position>();
            var horizontalMirror = position.HorizontalMirror;
            horizontalMirror.GetComponent<SelectedBehavior>().RemoveSelection(SwapBehavior(behaviorName));
        }
    }

    public string SwapBehavior(string behaviorName)
    {
        if (behaviorName == "LeftUp")
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
