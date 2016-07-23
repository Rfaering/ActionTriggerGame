using Assets.Scripts.Utils;
using UnityEngine;

namespace Assets.Scripts.Triggers
{
    public class Right : Next
    {
        public Right(GameObject owner) : base(owner)
        {
        }

        public override Direction TriggerDirection
        {
            get
            {
                return Direction.Right;
            }
        }

        public override void UpdateUI(GameObject gameobject)
        {
            gameobject.GetComponent<ImageSetter>().SetImage("Trigger/Right", Resources.Load<Sprite>("Icons/Right"));
            base.UpdateUI(gameobject);
        }
    }
}
