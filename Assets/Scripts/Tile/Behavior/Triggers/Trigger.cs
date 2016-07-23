using Assets.Scripts.Misc;
using Assets.Scripts.Utils;
using UnityEngine;

namespace Assets.Scripts.Triggers
{
    public abstract class Trigger : BehaviorBase
    {
        public abstract bool Check();
        public bool Locked;

        public Trigger(GameObject owner) : base(owner)
        {
        }

        public override BehaviorTypes BehaviorType
        {
            get
            {
                return BehaviorTypes.Triggers;
            }
        }

        public override void ClearUI(GameObject gameobject)
        {
            gameobject.GetComponent<ImageSetter>().ClearImages("Trigger");
        }
        

        public override void UpdateUI(GameObject gameobject)
        {
            gameobject.GetComponent<ImageSetter>().SetImage("Trigger/Full", Icon);
        }
    }
}
