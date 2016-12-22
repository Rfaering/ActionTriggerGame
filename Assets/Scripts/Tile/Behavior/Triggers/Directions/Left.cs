
using UnityEngine;

public class Left : Next
{
    public Left(GameObject owner) : base(owner)
    {
    }

    public override Direction[] WaterDirections
    {
        get
        {
            return new[] { Direction.Left };
        }
    }

    public override void UpdateUI(GameObject gameobject, bool preview = false)
    {
        gameobject.GetComponent<ImageSetter>().SetHoseVisual(ImageSetter.HoseTypes.Center, ImageSetter.Angle.Left, preview);
        base.UpdateUI(gameobject);
    }
}
