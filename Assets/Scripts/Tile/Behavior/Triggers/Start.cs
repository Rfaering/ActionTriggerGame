using UnityEngine;

namespace Assets.Scripts.Triggers
{
    public class Start : Trigger
    {
        public Start(GameObject owner) : base(owner)
        {
        }

        public override bool Check()
        {
            return true;
        }        
    }
}
