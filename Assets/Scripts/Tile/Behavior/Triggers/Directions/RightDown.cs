using Assets.Scripts.Triggers;
using Assets.Scripts.Utils;
using UnityEngine;

namespace Assets.Scripts.Tile.Behavior.Triggers.Directions
{
    public class RightDown : Next
    {
        public RightDown(GameObject owner) : base(owner)
        {
        }

        public override Direction TriggerDirection
        {
            get
            {
                return Direction.RightDown;
            }
        }

        public override void UpdateUI(GameObject gameobject)
        {
            gameobject.GetComponent<ImageSetter>().SetImage("Trigger/Right", Resources.Load<Sprite>("Icons/Right"));
            gameobject.GetComponent<ImageSetter>().SetImage("Trigger/Down", Resources.Load<Sprite>("Icons/Down"));
            base.UpdateUI(gameobject);
        }
    }
}
