
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class LeftUp : Next
{
    public LeftUp(GameObject owner) : base(owner)
    {
    }

    public override Direction[] WaterDirections
    {
        get
        {
            return new[] { Direction.Left, Direction.Up };
        }
    }

    public override void UpdateUI(GameObject gameobject, bool preview = false)
    {
        gameobject.GetComponent<ImageSetter>().SetHoseVisual(ImageSetter.HoseTypes.Turn, ImageSetter.Angle.Left, preview);
        base.UpdateUI(gameobject);
    }
}
