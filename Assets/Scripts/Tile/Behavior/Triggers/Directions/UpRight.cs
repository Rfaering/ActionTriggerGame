using Assets.Scripts.Utils;
using UnityEngine;

namespace Assets.Scripts.Triggers
{
    public class UpRight : Next
    {
        public UpRight(GameObject owner) : base(owner)
        {
        }

        public override Direction[] WaterDirections
        {
            get
            {
                return new[] { Direction.Up, Direction.Right };
            }
        }

        public override void UpdateUI(GameObject gameobject, bool preview = false)
        {
            gameobject.GetComponent<ImageSetter>().SetHoseVisual(ImageSetter.HoseTypes.Turn, ImageSetter.Angle.Up, preview);
            base.UpdateUI(gameobject);
        }
    }
}
