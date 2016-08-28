using Assets.Scripts.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Triggers
{
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
            gameobject.GetComponent<ImageSetter>().SetHoseVisual(ImageSetter.HoseTypes.Turn, ImageSetter.Angle.Right, preview);
            base.UpdateUI(gameobject);
        }
    }
}
