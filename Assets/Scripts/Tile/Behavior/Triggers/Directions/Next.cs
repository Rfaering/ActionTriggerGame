using Assets.Scripts.Actions;
using Assets.Scripts.Tile;
using Assets.Scripts.Utils;
using Assets.Scripts.World.Tile;
using UnityEngine;
using System.Linq;
using Assets.Scripts.Tile.Behavior;

namespace Assets.Scripts.Triggers
{
    public abstract class Next : Trigger
    {
        public Next(GameObject owner) : base(owner)
        {
        }

        public abstract Direction[] WaterDirections { get; }

        public override bool Check()
        {            
            if (_owner.GetComponent<Behaviors>().GetAction<Water>().Active)
            {
                return true;
            }

            var waterCommingFromAnyDirection = WaterDirections.Any(IsWaterCommingFromDirection);
            return waterCommingFromAnyDirection;
        }

        private bool IsWaterCommingFromDirection(Direction waterDirection)
        {
            var position = _owner.GetComponent<Position>();

            switch (waterDirection)
            {
                case Direction.Up:
                    return Check(position.Up, Direction.Down);
                case Direction.Down:
                    return Check(position.Down, Direction.Up);
                case Direction.Left:
                    return Check(position.Left, Direction.Right);
                case Direction.Right:
                    return Check(position.Right, Direction.Left);
            }

            return false;
        }

        public override void UpdateUI(GameObject gameobject)
        {
        }

        private bool Check(GameObject gameObject, Direction waterCommingFromDirection)
        {
            if (gameObject != null)
            {
                var waterState = gameObject.GetComponent<WaterState>();
                var selectedBehavior = gameObject.GetComponent<SelectedBehavior>().SelectedTrigger as Next;
                if (selectedBehavior == null)
                {
                    return false;
                }

                return (waterState.Watered) && selectedBehavior.WaterDirections.Any(x => x == waterCommingFromDirection);
            }
            return false;
        }
    }
}
