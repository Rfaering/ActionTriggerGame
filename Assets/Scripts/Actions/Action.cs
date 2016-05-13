using System;
using Assets.Scripts.Misc;
using UnityEngine;

namespace Assets.Scripts.Actions
{
    public abstract class Action : BehaviorBase
    {
        public abstract void Execute(GameObject gameObject);

        public override BehaviorTypes BehaviorType
        {
            get {
                return BehaviorTypes.Actions;
            }
        }
    }
}
