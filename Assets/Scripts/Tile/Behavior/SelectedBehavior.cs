using Assets.Scripts.Actions;
using Assets.Scripts.Misc;
using Assets.Scripts.Triggers;
using Assets.Scripts.World.Tile;
using UnityEngine;

namespace Assets.Scripts.Tile.Behavior
{
    public class SelectedBehavior : MonoBehaviour
    {
        private Action _selectedAction;

        public Action SelectedAction
        {
            get { return _selectedAction; }
            set
            {
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
                var oldValue = _selectedTrigger;
                _selectedTrigger = value;
                var newValue = _selectedTrigger;

                RemoveActivation(oldValue);
                ApplyActivation(newValue);

            }
        }

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

        public BehaviorBase GetBehavior(string name)
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
            return GetBehavior(name) != null;
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

        // Will return oldbehavior
        public string SelectBehavior(string name)
        {
            if (name == null)
            {
                return null;
            }
            
            var behavior = _behaviors.GetBehavior(name);
            var oldSelection = SelectBehavior(behavior);
            return oldSelection != null ? oldSelection.Name : null;
        }

        public void RemoveSelection(BehaviorBase behavior)
        {
            if (IsNameSelected(behavior.Name))
            {
                RemoveSelection(behavior.Name);
            }
        }

        public BehaviorBase SelectBehavior(BehaviorBase behavior)
        {
            BehaviorBase oldBehavior = null;
            if (IsNameSelected(behavior.Name))
            {
                return GetBehavior(behavior.Name);
            }

            if (behavior.BehaviorType == BehaviorTypes.Actions)
            {
                oldBehavior = SelectedAction;
                SelectedAction = behavior as Action;
            }
            if (behavior.BehaviorType == BehaviorTypes.Triggers)
            {
                oldBehavior = SelectedTrigger;
                SelectedTrigger = behavior as Trigger;
            }

            return oldBehavior;
        }
    }
}
