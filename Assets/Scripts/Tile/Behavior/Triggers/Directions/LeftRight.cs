using Assets.Scripts.Triggers;
using Assets.Scripts.Utils;
using UnityEngine;

namespace Assets.Scripts.Tile.Behavior.Triggers.Directions
{
    public class LeftRight : Next
    {
        public LeftRight(GameObject owner) : base(owner)
        {
        }

        public override Direction TriggerDirection
        {
            get
            {
                return Direction.LeftRight;
            }
        }

        public override void UpdateUI(GameObject gameobject)
        {
            gameobject.GetComponent<ImageSetter>().SetImage("Trigger/Left", Resources.Load<Sprite>("Icons/Left"));
            gameobject.GetComponent<ImageSetter>().SetImage("Trigger/Right", Resources.Load<Sprite>("Icons/Right"));
            base.UpdateUI(gameobject);
        }
    }
}
