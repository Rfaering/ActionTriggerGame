
using UnityEngine;

public class Right : Next
{
    public Right(GameObject owner) : base(owner)
    {
    }

    public override Direction[] WaterDirections
    {
        get
        {
            return new[] { Direction.Right };
        }
    }

    public override void UpdateUI(GameObject gameobject, bool preview = false)
    {
        gameobject.GetComponent<ImageSetter>().SetHoseVisual(ImageSetter.HoseTypes.Center, ImageSetter.Angle.Right, preview);
        base.UpdateUI(gameobject);
    }
}
