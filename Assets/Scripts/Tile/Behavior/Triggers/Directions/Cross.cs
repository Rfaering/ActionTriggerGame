using UnityEngine;

public class Cross : Next
{
    public Cross(GameObject owner) : base(owner)
    {
    }

    public override Direction[] WaterDirections
    {
        get
        {
            return new[] { Direction.Up, Direction.Right, Direction.Left, Direction.Down };
        }
    }

    public override void UpdateUI(GameObject gameobject, bool preview = false)
    {
        gameobject.GetComponent<ImageSetter>().SetHoseVisual(ImageSetter.HoseTypes.Cross, ImageSetter.Angle.Down, preview);
        base.UpdateUI(gameobject);
    }
}
