
using UnityEngine;

public class RightDown : Next
{
    public RightDown(GameObject owner) : base(owner)
    {
    }

    public override Direction[] WaterDirections
    {
        get
        {
            return new[] { Direction.Right, Direction.Down };
        }
    }

    public override void UpdateUI(GameObject gameobject, bool preview = false)
    {
        gameobject.GetComponent<ImageSetter>().SetHoseVisual(ImageSetter.HoseTypes.Turn, ImageSetter.Angle.Right, preview);
        base.UpdateUI(gameobject);
    }
}
