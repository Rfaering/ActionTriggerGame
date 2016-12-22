using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.EventSystems;
using System;

public class ConnectTiles : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler
{
    public static List<GameObject> chain = new List<GameObject>();
    public static bool dragInProgress = false;
    public static GameObject startObject;

    public Direction IsConnectedInDirection { get; set; }
    public bool ChainWillBeAdded { get; internal set; }

    public void Start()
    {
        IsConnectedInDirection = Direction.NoDirection;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!DraggingEnabled())
        {
            return;
        }

        if (dragInProgress)
        {
            return;
        }

        startObject = gameObject;
        dragInProgress = true;
        PushToStack(gameObject);
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        if (!DraggingEnabled())
        {
            return;
        }

        if (dragInProgress && startObject == gameObject)
        {
            dragInProgress = false;
            SetValueToPreview();
            ClearStack();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!DraggingEnabled())
        {
            return;
        }

        if (dragInProgress && !IsInStack(gameObject))
        {
            var hoveredObject = gameObject;
            if (hoveredObject != null)
            {
                var isInStack = IsInStack(hoveredObject);
                var isBridgeTile = IsValidBridge(hoveredObject);
                var isNeighbour = IsNeighbour(hoveredObject);

                if ((!isInStack || isBridgeTile) && isNeighbour)
                {
                    PushToStack(hoveredObject);
                }

                chain.ForEach(x => x.GetComponent<ConnectTiles>().ChainWillBeAdded = true);
            }
            else
            {
                chain.ForEach(x => x.GetComponent<ConnectTiles>().ChainWillBeAdded = false);
            }
        }
    }

    private bool IsNeighbour(GameObject hoveredObject)
    {
        if (!chain.Any())
        {
            return true;
        }

        var last = chain.Last();
        return last.GetComponent<Position>().Neighbours.Any(x => x == hoveredObject);
    }

    private static bool IsValidBridge(GameObject hoveredObject)
    {
        return chain.Last() != hoveredObject
            && hoveredObject.GetComponent<Behaviors>().IsAvailableBridgeTile() &&
            (hoveredObject.GetComponent<SelectedBehavior>().GetPreview() == "UpDown" ||
             hoveredObject.GetComponent<SelectedBehavior>().GetPreview() == "LeftRight");
    }

    private bool DraggingEnabled()
    {
        if (Globals.InputMode != InputMode.Connect)
        {
            return false;
        }
        if (FindObjectOfType<Runner>().IsRunning())
        {
            return false;
        }

        if (!FindObjectOfType<Visibility>().IsVisible)
        {
            return false;
        }

        return true;
    }

    private bool IsInStack(GameObject objectInQuestion)
    {
        return chain.Any(x => x == objectInQuestion);
    }

    private void PushToStack(GameObject objectInQuestion)
    {
        objectInQuestion.GetComponent<ConnectTiles>().ChainWillBeAdded = true;
        objectInQuestion.GetComponent<Visual>().InChain = true;
        var last = chain.LastOrDefault();
        if (last != null)
        {
            SetPreview(last, objectInQuestion);
        }

        chain.Add(objectInQuestion);
    }

    private void SetPreview(GameObject previous, GameObject current)
    {
        var previousSelectedBehavior = previous.GetComponent<SelectedBehavior>();
        var currentSelectedBehavior = current.GetComponent<SelectedBehavior>();

        var previousPosition = previous.GetComponent<Position>();
        var currentPosition = current.GetComponent<Position>();

        var previousConnectTile = previous.GetComponent<ConnectTiles>();
        var currentConnectTile = current.GetComponent<ConnectTiles>();

        if (previousConnectTile.IsConnectedInDirection == Direction.NoDirection)
        {
            if (currentPosition.Up == previous)
            {
                SetCurrent(currentSelectedBehavior, "Up");
                currentConnectTile.IsConnectedInDirection = Direction.Up;
                SetPrevious(previousSelectedBehavior, currentConnectTile.IsConnectedInDirection, "UpDown");
            }
            else if (currentPosition.Down == previous)
            {
                SetCurrent(currentSelectedBehavior, "Down");
                currentConnectTile.IsConnectedInDirection = Direction.Down;
                SetPrevious(previousSelectedBehavior, currentConnectTile.IsConnectedInDirection, "UpDown");
            }
            else if (currentPosition.Right == previous)
            {
                SetCurrent(currentSelectedBehavior, "Right");
                currentConnectTile.IsConnectedInDirection = Direction.Right;
                SetPrevious(previousSelectedBehavior, currentConnectTile.IsConnectedInDirection, "LeftRight");
            }
            else if (currentPosition.Left == previous)
            {
                SetCurrent(currentSelectedBehavior, "Left");
                currentConnectTile.IsConnectedInDirection = Direction.Left;
                SetPrevious(previousSelectedBehavior, currentConnectTile.IsConnectedInDirection, "LeftRight");
            }
        }


        if (previousConnectTile.IsConnectedInDirection == Direction.Up)
        {
            if (currentPosition.Up == previous)
            {
                SetCurrent(currentSelectedBehavior, "Up");
                currentConnectTile.IsConnectedInDirection = Direction.Up;
                SetPrevious(previousSelectedBehavior, currentConnectTile.IsConnectedInDirection, "UpDown");
            }
            else if (currentPosition.Right == previous)
            {
                SetCurrent(currentSelectedBehavior, "Right");
                currentConnectTile.IsConnectedInDirection = Direction.Right;
                SetPrevious(previousSelectedBehavior, currentConnectTile.IsConnectedInDirection, "LeftUp");
            }
            else if (currentPosition.Left == previous)
            {
                SetCurrent(currentSelectedBehavior, "Left");
                currentConnectTile.IsConnectedInDirection = Direction.Left;
                SetPrevious(previousSelectedBehavior, currentConnectTile.IsConnectedInDirection, "UpRight");
            }
        }

        if (previousConnectTile.IsConnectedInDirection == Direction.Down)
        {
            if (currentPosition.Down == previous)
            {
                SetCurrent(currentSelectedBehavior, "Down");
                currentConnectTile.IsConnectedInDirection = Direction.Down;
                SetPrevious(previousSelectedBehavior, currentConnectTile.IsConnectedInDirection, "UpDown");
            }
            else if (currentPosition.Right == previous)
            {
                SetCurrent(currentSelectedBehavior, "Right");
                currentConnectTile.IsConnectedInDirection = Direction.Right;
                SetPrevious(previousSelectedBehavior, currentConnectTile.IsConnectedInDirection, "DownLeft");
            }
            else if (currentPosition.Left == previous)
            {
                SetCurrent(currentSelectedBehavior, "Left");
                currentConnectTile.IsConnectedInDirection = Direction.Left;
                SetPrevious(previousSelectedBehavior, currentConnectTile.IsConnectedInDirection, "RightDown");
            }
        }

        if (previousConnectTile.IsConnectedInDirection == Direction.Right)
        {
            if (currentPosition.Down == previous)
            {
                SetCurrent(currentSelectedBehavior, "Down");
                currentConnectTile.IsConnectedInDirection = Direction.Down;
                SetPrevious(previousSelectedBehavior, currentConnectTile.IsConnectedInDirection, "UpRight");
            }
            else if (currentPosition.Up == previous)
            {
                SetCurrent(currentSelectedBehavior, "Up");
                currentConnectTile.IsConnectedInDirection = Direction.Up;
                SetPrevious(previousSelectedBehavior, currentConnectTile.IsConnectedInDirection, "RightDown");
            }
            else if (currentPosition.Right == previous)
            {
                SetCurrent(currentSelectedBehavior, "Right");
                currentConnectTile.IsConnectedInDirection = Direction.Right;
                SetPrevious(previousSelectedBehavior, currentConnectTile.IsConnectedInDirection, "LeftRight");
            }
        }

        if (previousConnectTile.IsConnectedInDirection == Direction.Left)
        {
            if (currentPosition.Down == previous)
            {
                SetCurrent(currentSelectedBehavior, "Down");
                currentConnectTile.IsConnectedInDirection = Direction.Down;
                SetPrevious(previousSelectedBehavior, currentConnectTile.IsConnectedInDirection, "LeftUp");
            }
            else if (currentPosition.Up == previous)
            {
                SetCurrent(currentSelectedBehavior, "Up");
                currentConnectTile.IsConnectedInDirection = Direction.Up;
                SetPrevious(previousSelectedBehavior, currentConnectTile.IsConnectedInDirection, "DownLeft");
            }
            else if (currentPosition.Left == previous)
            {
                SetCurrent(currentSelectedBehavior, "Left");
                currentConnectTile.IsConnectedInDirection = Direction.Left;
                SetPrevious(previousSelectedBehavior, currentConnectTile.IsConnectedInDirection, "LeftRight");
            }
        }
    }

    private void SetPrevew(SelectedBehavior currentSelectedBehavior, string name)
    {
        var behavior = currentSelectedBehavior.GetComponent<Behaviors>().GetBehavior(name);
        if (behavior.Available)
        {
            FindObjectOfType<Mirror>().SetMirrorPreview(currentSelectedBehavior.gameObject, name);
            currentSelectedBehavior.SetPreview(name);
        }
    }

    private void SetCurrent(SelectedBehavior currentSelectedBehavior, string name)
    {
        if (IsInStack(currentSelectedBehavior.gameObject))
        {
            SetPrevew(currentSelectedBehavior, "BridgeUpDown");
        }
        else
        {
            SetPrevew(currentSelectedBehavior, name);
        }
    }

    private void SetPrevious(SelectedBehavior previousSelectedBehavior, Direction currentDirection, string direction)
    {
        if (previousSelectedBehavior.GetPreview() == "BridgeUpDown")
        {
            return;
        }

        if (chain.Any() && previousSelectedBehavior.gameObject != chain.First().gameObject)
        {
            SetPrevew(previousSelectedBehavior, direction);
        }
        else
        {
            switch (currentDirection)
            {
                case Direction.Up:
                    if (previousSelectedBehavior.IsNameSelected("Left"))
                    {
                        SetPrevew(previousSelectedBehavior, "DownLeft");
                    }
                    else if (previousSelectedBehavior.IsNameSelected("Right"))
                    {
                        SetPrevew(previousSelectedBehavior, "RightDown");
                    }
                    else if (previousSelectedBehavior.IsNameSelected("Up"))
                    {
                        SetPrevew(previousSelectedBehavior, "UpDown");
                    }
                    else
                    {
                        SetPrevew(previousSelectedBehavior, "Down");
                    }
                    break;
                case Direction.Down:
                    if (previousSelectedBehavior.IsNameSelected("Left"))
                    {
                        SetPrevew(previousSelectedBehavior, "LeftUp");
                    }
                    else if (previousSelectedBehavior.IsNameSelected("Right"))
                    {
                        SetPrevew(previousSelectedBehavior, "UpRight");
                    }
                    else if (previousSelectedBehavior.IsNameSelected("Down"))
                    {
                        SetPrevew(previousSelectedBehavior, "UpDown");
                    }
                    else
                    {
                        SetPrevew(previousSelectedBehavior, "Up");
                    }
                    break;
                case Direction.Right:
                    if (previousSelectedBehavior.IsNameSelected("Right"))
                    {
                        SetPrevew(previousSelectedBehavior, "LeftRight");
                    }
                    else if (previousSelectedBehavior.IsNameSelected("Up"))
                    {
                        SetPrevew(previousSelectedBehavior, "LeftUp");
                    }
                    else if (previousSelectedBehavior.IsNameSelected("Down"))
                    {
                        SetPrevew(previousSelectedBehavior, "DownLeft");
                    }
                    else
                    {
                        SetPrevew(previousSelectedBehavior, "Left");
                    }
                    break;
                case Direction.Left:
                    if (previousSelectedBehavior.IsNameSelected("Left"))
                    {
                        SetPrevew(previousSelectedBehavior, "LeftRight");
                    }
                    else if (previousSelectedBehavior.IsNameSelected("Up"))
                    {
                        SetPrevew(previousSelectedBehavior, "UpRight");
                    }
                    else if (previousSelectedBehavior.IsNameSelected("Down"))
                    {
                        SetPrevew(previousSelectedBehavior, "RightDown");
                    }
                    else
                    {
                        SetPrevew(previousSelectedBehavior, "Right");
                    }
                    break;
                default:
                    break;
            }
        }
    }

    private void SetValueToPreview()
    {
        foreach (var item in chain)
        {
            var preview = item.GetComponent<SelectedBehavior>().GetPreview();
            FindObjectOfType<Mirror>().SetMirror(item.gameObject, preview);
            item.GetComponent<SelectedBehavior>().SelectBehavior(preview);
        }
    }

    private void ClearStack()
    {
        foreach (var item in chain)
        {
            item.GetComponent<Visual>().InChain = false;
            item.GetComponent<ConnectTiles>().IsConnectedInDirection = Direction.NoDirection;
            item.GetComponent<SelectedBehavior>().ClearPreview();
            FindObjectOfType<Mirror>().ClearMirrorPreview(item.gameObject);
            item.GetComponent<ConnectTiles>().ChainWillBeAdded = false;
        }

        chain.Clear();
    }
}
