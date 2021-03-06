﻿using UnityEngine;
using System.Linq;

public class ButtonClick : MonoBehaviour
{
    public GameObject CurrentSelectedTile;

    public BehaviorBase CurrentBehavior
    {
        get
        {
            return CurrentSelectedTile != null ? CurrentSelectedTile.GetComponent<Behaviors>().GetBehavior(gameObject.name) : null;
        }
    }

    public void ActivateBehavior()
    {
        if (Globals.InputMode == InputMode.DragAndDrop)
        {
            return;
        }

        var isInBuildMode = GlobalProperties.IsInBuildMode();
        var currentBehavior = CurrentBehavior;
        if (currentBehavior == null)
        {
            return;
        }

        if (!isInBuildMode && currentBehavior != null && !currentBehavior.Available)
        {
            return;
        }

        GUI.UnfocusWindow();

        if (isInBuildMode && Input.GetKey(KeyCode.LeftControl))
        {
            ToggleAvailibity();
            SetColorBasedOnAvailibility();
        }
        else
        {
            ToggleSelection();
        }
    }

    private void ToggleSelection()
    {
        var selectedBehavior = CurrentSelectedTile.GetComponent<SelectedBehavior>();
        var removeSelection = selectedBehavior.IsNameSelected(gameObject.name);
        ToggleAllTilesSelection(removeSelection);
    }

    private void ToggleAllTilesSelection(bool removeSelection)
    {
        var behaviors = FindObjectsOfType<TileSelection>();
        foreach (var item in behaviors.Where(x => x.Selected))
        {
            ToggleSingleSelection(item.GetComponent<SelectedBehavior>(), removeSelection);
        }
    }

    private void ToggleSingleSelection(SelectedBehavior selectedBehavior, bool removeSelection)
    {
        if (removeSelection)
        {
            FindObjectOfType<Mirror>().RemoveSelection(selectedBehavior.gameObject, name);
            selectedBehavior.RemoveSelection(this.gameObject.name);
        }
        else
        {
            FindObjectOfType<Mirror>().SetMirror(selectedBehavior.gameObject, name);
            selectedBehavior.SelectBehavior(this.gameObject.name);
        }
    }

    private void ToggleAvailibity()
    {
        var behavior = CurrentSelectedTile.GetComponent<Behaviors>().GetBehavior(gameObject.name);
        var newValue = !behavior.Available;
        ToggleAvailibility(behavior, newValue);
    }

    private static void ToggleAvailibility(BehaviorBase behavior, bool newValue)
    {
        var behaviors = FindObjectOfType<Runner>().GetComponentsInChildren<TileSelection>();

        foreach (var item in behaviors.Where(x => x.Selected))
        {
            item.GetComponent<Behaviors>().GetBehavior(behavior.Name).Available = newValue;
        }
    }

    private static void DeactiveAllBehaviorsOfSameKind(BehaviorBase[] listOfSameType)
    {
        foreach (var item in listOfSameType)
        {
            item.Active = false;
        }
    }

    private void SetColorBasedOnAvailibility()
    {
        var currentBehavior = CurrentBehavior;
        if (Globals.InputMode == InputMode.Buttons && (currentBehavior == null || !currentBehavior.Available))
        {
            GetComponent<ButtonEnabled>().ContentEnabled = false;
        }
        else
        {
            GetComponent<ButtonEnabled>().ContentEnabled = true;
        }
    }

    public void MatchActionWithSelection(GameObject gameObject)
    {
        CurrentSelectedTile = gameObject;
        SetColorBasedOnAvailibility();
    }
}
