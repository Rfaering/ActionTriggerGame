using Assets.Scripts.Utils;
using UnityEngine;

namespace Assets.Scripts.Triggers
{
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

        public override void UpdateUI(GameObject gameobject)
        {
            gameobject.GetComponent<ImageSetter>().SetHoseVisual(ImageSetter.HoseTypes.Cross, ImageSetter.Angle.Down);
            base.UpdateUI(gameobject);
        }
    }
}
