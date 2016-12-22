using UnityEngine;

public class DownLeft : Next
{
    public DownLeft(GameObject owner) : base(owner)
    {
    }

    public override Direction[] WaterDirections
    {
        get
        {
            return new[] { Direction.Down, Direction.Left };
        }
    }

    public override void UpdateUI(GameObject gameobject, bool preview = false)
    {
        gameobject.GetComponent<ImageSetter>().SetHoseVisual(ImageSetter.HoseTypes.Turn, ImageSetter.Angle.Down, preview);
        base.UpdateUI(gameobject);
    }
}
