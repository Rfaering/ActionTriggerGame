﻿
using UnityEngine;

public class UpDown : Next
{
    public UpDown(GameObject owner) : base(owner)
    {
    }

    public override Direction[] WaterDirections
    {
        get
        {
            return new[] { Direction.Up, Direction.Down };
        }
    }

    public override void UpdateUI(GameObject gameobject, bool preview = false)
    {
        gameobject.GetComponent<ImageSetter>().SetHoseVisual(ImageSetter.HoseTypes.Straight, ImageSetter.Angle.Down, preview);
        base.UpdateUI(gameobject);
    }
}
