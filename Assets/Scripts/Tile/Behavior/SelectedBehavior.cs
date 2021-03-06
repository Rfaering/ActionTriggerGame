﻿using UnityEngine;

public class SelectedBehavior : MonoBehaviour
{
    private Action _selectedAction;

    public Action SelectedAction
    {
        get { return _selectedAction; }
        set
        {
            ClearPreview();
            var oldValue = _selectedAction;
            _selectedAction = value;
            var newValue = _selectedAction;

            RemoveActivation(oldValue);
            ApplyActivation(newValue);
        }
    }


    private Trigger _selectedTrigger;

    public Trigger SelectedTrigger
    {
        get { return _selectedTrigger; }
        set
        {
            ClearPreview();
            var oldValue = _selectedTrigger;
            _selectedTrigger = value;
            var newValue = _selectedTrigger;

            RemoveActivation(oldValue);
            ApplyActivation(newValue);
        }
    }

    private BehaviorBase _preview;
    private BehaviorBase _covering;


    private void ApplyActivation(BehaviorBase behavior)
    {
        if (behavior != null)
        {
            behavior.Active = true;
        }
    }

    private void RemoveActivation(BehaviorBase behavior)
    {
        if (behavior != null)
        {
            behavior.Active = false;
        }
    }

    private Behaviors _behaviors;

    void Start()
    {
        _behaviors = GetComponent<Behaviors>();
    }

    public BehaviorBase GetSelectedBehavior(string name)
    {
        if (SelectedTrigger != null && SelectedTrigger.Name == name)
        {
            return SelectedTrigger;
        }

        if (SelectedAction != null && SelectedAction.Name == name)
        {
            return SelectedAction;
        }

        return null;
    }

    public bool IsNameSelected(string name)
    {
        return GetSelectedBehavior(name) != null;
    }

    public void RemoveSelection(string name)
    {
        if (SelectedTrigger != null && SelectedTrigger.Name == name)
        {
            SelectedTrigger = null;
        }

        if (SelectedAction != null && SelectedAction.Name == name)
        {
            SelectedAction = null;
        }
    }

    public bool HasBothSelected()
    {
        return SelectedAction != null && SelectedTrigger != null;
    }

    public void SelectBehavior(string name)
    {
        var behavior = _behaviors.GetBehavior(name);
        SelectBehavior(behavior);
    }

    public string GetPreview()
    {
        return _preview != null ? _preview.Name : null;
    }

    public void SetPreview(string name)
    {
        var behavior = _behaviors.GetBehavior(name);
        if (behavior == null)
        {
            return;
        }

        if (_preview != null)
        {
            ClearPreview();
        }

        _preview = behavior;

        if (_preview is Action && SelectedAction != null)
        {
            _covering = SelectedAction;
            _covering.ClearUI();
        }

        if (_preview is Trigger && SelectedTrigger != null)
        {
            _covering = SelectedTrigger;
            _covering.ClearUI();
        }

        if (_preview != null)
        {
            _preview.UpdateUI(true);
        }
    }

    public void ClearPreview()
    {
        if (_preview != null)
        {
            _preview.ClearUI();
        }
        if (_covering != null)
        {
            _covering.UpdateUI();
        }

        _preview = null;
        _covering = null;
    }

    public void RemoveSelection(BehaviorBase behavior)
    {
        if (IsNameSelected(behavior.Name))
        {
            RemoveSelection(behavior.Name);
        }
    }

    public void SelectBehavior(BehaviorBase behavior)
    {
        if (behavior == null)
        {
            return;
        }
        if (IsNameSelected(behavior.Name))
        {
            return;
        }

        if (behavior.BehaviorType == BehaviorTypes.Actions)
        {
            SelectedAction = behavior as Action;
        }
        if (behavior.BehaviorType == BehaviorTypes.Triggers)
        {
            SelectedTrigger = behavior as Trigger;
        }
    }
}
