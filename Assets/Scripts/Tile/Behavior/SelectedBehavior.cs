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

        public bool IsNameSelected(string name)
        {
            if (SelectedTrigger != null && SelectedTrigger.Name == name)
            {
                return true;
            }

            if (SelectedAction != null && SelectedAction.Name == name)
            {
                return true;
            }

            return false;
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

        public void RemoveSelection(BehaviorBase behavior)
        {
            if (IsNameSelected(behavior.Name))
            {
                RemoveSelection(behavior.Name);
            }
        }

        public void SelectBehavior(BehaviorBase behavior)
        {
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
}
