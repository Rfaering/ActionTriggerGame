using UnityEngine;
using System.Linq;

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

    internal bool IsWaterCommingFromDirection(Direction waterDirection)
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

    public override void UpdateUI(GameObject gameobject, bool preview = false)
    {
    }

    private bool Check(GameObject gameObject, Direction isWaterCommingFromTheseDirections)
    {
        if (gameObject != null)
        {
            var waterState = gameObject.GetComponent<WaterState>();
            var selectedBehavior = gameObject.GetComponent<SelectedBehavior>().SelectedTrigger as Next;

            bool check = false;

            if (selectedBehavior == null)
            {
                return false;
            }

            var bridge = selectedBehavior as BridgeUpDown;
            if (bridge != null)
            {
                check = bridge.UnderWaterFlow && bridge.UnderBridgeDirections.Any(x => x == isWaterCommingFromTheseDirections);
            }

            check = check || ((waterState.Watered) && selectedBehavior.WaterDirections.Any(x => x == isWaterCommingFromTheseDirections));

            return check;
        }
        return false;
    }
}
