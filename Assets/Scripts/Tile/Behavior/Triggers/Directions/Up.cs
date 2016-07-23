using Assets.Scripts.Utils;
using UnityEngine;

namespace Assets.Scripts.Triggers
{
    public class Up : Next
    {
        public Up(GameObject owner) : base(owner)
        {
        }

        public override Direction TriggerDirection
        {
            get
            {
                return Direction.Up;
            }
        }

        public override void UpdateUI(GameObject gameobject)
        {
            gameobject.GetComponent<ImageSetter>().SetImage("Trigger/Up", Resources.Load<Sprite>("Icons/Up"));
            base.UpdateUI(gameobject);
        }
    }
}
