using Assets.Scripts.Utils;
using UnityEngine;

namespace Assets.Scripts.Triggers
{
    public class Down : Next
    {
        public Down(GameObject owner) : base(owner)
        {
        }

        public override Direction[] WaterDirections
        {
            get
            {
                return new[] { Direction.Down };
            }
        }

        public override void UpdateUI(GameObject gameobject, bool preview = false)
        {
            gameobject.GetComponent<ImageSetter>().SetHoseVisual(ImageSetter.HoseTypes.Center, ImageSetter.Angle.Down, preview);
            base.UpdateUI(gameobject);
        }
    }
}
