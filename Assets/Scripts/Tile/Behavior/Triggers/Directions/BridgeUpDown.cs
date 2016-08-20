using Assets.Scripts.Triggers;
using Assets.Scripts.Utils;
using UnityEngine;
using System.Linq;

namespace Assets.Scripts.Tile.Behavior.Triggers.Directions
{
    public class BridgeUpDown : Next
    {
        public BridgeUpDown(GameObject owner) : base(owner)
        {
        }

        public override Direction[] WaterDirections
        {
            get
            {
                return new[] { Direction.Up, Direction.Down };
            }
        }

        public bool UnderWaterFlow;
        public bool IsGoingToExecuteWaterFlow;

        public Direction[] UnderBridgeDirections
        {
            get
            {
                return new[] { Direction.Left, Direction.Right };
            }
        }

        public void ExecuteUnderWaterFlow()
        {
            UnderWaterFlow = true;
            IsGoingToExecuteWaterFlow = false;
            var animation = _owner.GetComponent<Animation>();
            animation.Blend("Bridge");
        }

        public override bool Check()
        {
            if (!UnderWaterFlow)
            {
                var waterCommingFromAnyDirection = UnderBridgeDirections.Any(IsWaterCommingFromDirection);
                if (waterCommingFromAnyDirection)
                {
                    IsGoingToExecuteWaterFlow = true;
                }
            }

            return base.Check();
        }

        public override void Reset()
        {
            UnderWaterFlow = false;
            IsGoingToExecuteWaterFlow = false;
            base.Reset();
        }

        public override void UpdateUI(GameObject gameobject)
        {
            gameobject.GetComponent<ImageSetter>().SetHoseVisual(ImageSetter.HoseTypes.Bridge, ImageSetter.Angle.Down);
            base.UpdateUI(gameobject);
        }
    }
}
