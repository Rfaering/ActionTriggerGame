using Assets.Scripts.Utils;
using UnityEngine;

namespace Assets.Scripts.Triggers
{
    public class LeftRight : Next
    {
        public LeftRight(GameObject owner) : base(owner)
        {
        }

        public override Direction[] WaterDirections
        {
            get
            {
                return new[] { Direction.Left, Direction.Right };
            }
        }

        public override void UpdateUI(GameObject gameobject, bool preview = false)
        {
            gameobject.GetComponent<ImageSetter>().SetHoseVisual(ImageSetter.HoseTypes.Straight, ImageSetter.Angle.Right, preview);
            base.UpdateUI(gameobject);
        }
    }
}
