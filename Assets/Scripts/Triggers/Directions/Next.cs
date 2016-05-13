using UnityEngine;

namespace Assets.Scripts.Triggers
{
    public abstract class Next : Trigger
    {
        public abstract Direction TriggerDirection { get; } 

        public override bool Check()
        {
            var position = GetComponent<Position>();

            switch (TriggerDirection)
            {
                case Direction.Up:
                    return Check(position.Up);
                case Direction.Down:
                    return Check(position.Down);
                case Direction.Left:
                    return Check(position.Left);
                case Direction.Right:
                    return Check(position.Right);
            }

            return false;
        }

        private bool Check( GameObject gameObject )
        {
            if ( gameObject != null )
            {
                var position = gameObject.GetComponent<Position>();
                if ( position.Death )
                {
                    return true;
                }
            }
            return false;
        }
    }
}
