using Assets.Scripts.Triggers;
using Assets.Scripts.Utils;
using UnityEngine;

namespace Assets.Scripts.Tile.Behavior.Triggers.Directions
{
    public class UpDown : Next
    {
        public UpDown(GameObject owner) : base(owner)
        {
        }

        public override Direction TriggerDirection
        {
            get
            {
                return Direction.UpDown;
            }
        }

        public override void UpdateUI(GameObject gameobject)
        {
            gameobject.GetComponent<ImageSetter>().SetImage("Trigger/Up", Resources.Load<Sprite>("Icons/Up"));
            gameobject.GetComponent<ImageSetter>().SetImage("Trigger/Down", Resources.Load<Sprite>("Icons/Down"));
            base.UpdateUI(gameobject);
        }
    }
}
