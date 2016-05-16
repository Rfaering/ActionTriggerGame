using Assets.Scripts.Misc;

namespace Assets.Scripts.Triggers
{
    public abstract class Trigger : BehaviorBase
    {
        public abstract bool Check();
        public bool Locked;

        public override BehaviorTypes BehaviorType
        {
            get
            {
                return BehaviorTypes.Triggers;
            }
        }
    }
}
