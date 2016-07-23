using UnityEngine;
using Assets.Scripts.Misc;
using Assets.Scripts.Utils;

namespace Assets.Scripts.Actions
{
    public abstract class Action : BehaviorBase
    {
        public Action(GameObject owner) : base(owner)
        {
        }

        public abstract void Execute(GameObject gameObject);

        public override BehaviorTypes BehaviorType
        {
            get
            {
                return BehaviorTypes.Actions;
            }
        }

        public override void ClearUI(GameObject gameobject)
        {
            gameobject.GetComponent<ImageSetter>().ClearImages("Action");
        }


        public override void UpdateUI(GameObject gameobject)
        {
            gameobject.GetComponent<ImageSetter>().SetImage("Action/Full", Icon);
        }
    }
}
