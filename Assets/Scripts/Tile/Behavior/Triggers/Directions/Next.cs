using Assets.Scripts.Tile;
using Assets.Scripts.Utils;
using Assets.Scripts.World.Tile;
using UnityEngine;

namespace Assets.Scripts.Triggers
{
    public abstract class Next : Trigger
    {
        public Next(GameObject owner) : base(owner)
        {
        }

        public abstract Direction TriggerDirection { get; }

        public override bool Check()
        {
            var position = _owner.GetComponent<Position>();

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
                case Direction.UpDown:
                    return Check(position.Up) && Check(position.Down);
                case Direction.LeftRight:
                    return Check(position.Left) && Check(position.Right);
            }

            return false;
        }

        public override void UpdateUI(GameObject gameobject)
        {
            gameobject.GetComponent<ImageSetter>().SetImage("Trigger/Middle", Resources.Load<Sprite>("Icons/skull"));
        }

        private bool Check(GameObject gameObject)
        {
            if (gameObject != null)
            {
                var position = gameObject.GetComponent<Position>();
                if (position.Death)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
