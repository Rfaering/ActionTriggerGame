using Assets.Scripts.Triggers;
using Assets.Scripts.Utils;
using UnityEngine;

namespace Assets.Scripts.Tile.Behavior.Triggers.Directions
{
    public class LeftUp : Next
    {
        public LeftUp(GameObject owner) : base(owner)
        {
        }

        public override Direction TriggerDirection
        {
            get
            {
                return Direction.LeftUp;
            }
        }

        public override void UpdateUI(GameObject gameobject)
        {
            gameobject.GetComponent<ImageSetter>().SetImage("Trigger/Left", Resources.Load<Sprite>("Icons/Left"));
            gameobject.GetComponent<ImageSetter>().SetImage("Trigger/Up", Resources.Load<Sprite>("Icons/Up"));
            base.UpdateUI(gameobject);
        }
    }
}
