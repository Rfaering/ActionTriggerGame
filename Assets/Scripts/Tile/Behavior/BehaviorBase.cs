using Assets.Scripts.Tile;
using Assets.Scripts.Tile.Behavior;
using Assets.Scripts.World.Tile;
using UnityEngine;
using System.Linq;

namespace Assets.Scripts.Misc
{
    public abstract class BehaviorBase
    {
        internal readonly GameObject _owner;
        internal readonly SelectedBehavior _selectedBehavior;
        internal readonly Visual _visual;
        internal readonly Behaviors _behaviors;
        internal readonly Sprite Icon;

        public string Name
        {
            get { return this.GetType().Name; }
        }

        public virtual string IconName
        {
            get
            {
                return Name;
            }
        }

        public abstract BehaviorTypes BehaviorType { get; }

        public BehaviorBase(GameObject owner)
        {
            _owner = owner;
            _selectedBehavior = _owner.GetComponent<SelectedBehavior>();
            _visual = _owner.GetComponent<Visual>();
            _behaviors = _owner.GetComponent<Behaviors>();
            Icon = Resources.Load<Sprite>("Icons/" + IconName);
        }

        private bool _active;
        public bool Active
        {
            get { return _active; }
            set
            {
                _active = value;
                if (value)
                {
                    UpdateUI();
                    // Ensures that the behavior is also the selected behavior 
                    // for the tile.
                    _owner.GetComponent<SelectedBehavior>()
                        .SelectBehavior(this);
                }
                else
                {
                    ClearUI();
                    _owner.GetComponent<SelectedBehavior>()
                        .RemoveSelection(this);
                }

            }
        }

        public virtual void ResetUI()
        {
            if (_active)
            {
                UpdateUI();
            }
        }

        private bool _availible;
        public bool Available
        {
            get { return _availible; }
            set
            {
                _availible = value;
                _visual.Editable = _behaviors.AllBehaviors.Any(x => x.Available);
            }
        }

        public void UpdateUI()
        {
            if (Active)
            {
                UpdateUI(_owner);
            }
        }

        public void ClearUI()
        {            
            ClearUI(_owner);
        }

        public abstract void UpdateUI(GameObject gameobject);
        public abstract void ClearUI(GameObject gameobject);
    }
}
