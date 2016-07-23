using Assets.Scripts.Actions;
using Assets.Scripts.Misc;
using Assets.Scripts.Tile;
using Assets.Scripts.Tile.Behavior.Triggers.Directions;
using Assets.Scripts.Triggers;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.World.Tile
{
    public class Behaviors : MonoBehaviour
    {
        public Trigger[] AllTriggers = new Trigger[0];
        public Action[] AllActions = new Action[0];

        public BehaviorBase[] AllBehaviors
        {
            get
            {
                List<BehaviorBase> behaviors = new List<BehaviorBase>();
                behaviors.AddRange(AllTriggers);
                behaviors.AddRange(AllActions);
                return behaviors.ToArray();
            }
        }

        private bool _active;

        public bool Active
        {
            get { return _active; }
            set
            {
                _active = value;
                if (!_active)
                {
                    GetComponent<Position>().Death = false;
                    var win = GetComponent<Behaviors>().GetAction<Win>();
                    if (win != null)
                    {
                        win.Done = false;
                    }
                }
            }
        }

        public void Reset()
        {
            if (AllActions.Any(x => x.Available))
            {
                foreach (var action in AllActions.Where(x => x.Active))
                {
                    action.Active = false;
                }
            }

            if (AllTriggers.Any(x => x.Available))
            {
                foreach (var trigger in AllTriggers.Where(x => x.Active))
                {
                    trigger.Active = false;
                }
            }
        }

        public virtual void ResetUI()
        {
            Active = false;
            foreach (var behavior in AllBehaviors)
            {
                behavior.ResetUI();
            }
        }

        public bool StartTrigger;

        public void Initialize()
        {
            AllTriggers = new Trigger[]
            {
                new Start(gameObject),
                new Up(gameObject),
                new Down(gameObject),
                new Left(gameObject),
                new Right(gameObject),
                new LeftRight(gameObject),
                new UpDown(gameObject),
            };

            AllActions = new Action[]
            {
                new Kill(gameObject),
                new Win(gameObject),
                new Timer(gameObject),
            };
        }

        public T GetAction<T>() where T : Action
        {
            return AllActions.OfType<T>().FirstOrDefault();
        }

        public T GetTrigger<T>() where T : Trigger
        {
            return AllTriggers.OfType<T>().FirstOrDefault();
        }

        internal Action GetAction(string name)
        {
            return AllActions.FirstOrDefault(x => x.Name == name);
        }

        internal Trigger GetTrigger(string name)
        {
            return AllTriggers.FirstOrDefault(x => x.Name == name);
        }

        internal BehaviorBase GetBehavior(string name)
        {
            return AllBehaviors.FirstOrDefault(x => x.Name == name);
        }

        internal BehaviorBase[] GetBehaviorList(BehaviorTypes behaviorType)
        {
            if (behaviorType == BehaviorTypes.Triggers)
            {
                return AllTriggers;
            }
            if (behaviorType == BehaviorTypes.Actions)
            {
                return AllActions;
            }

            return new BehaviorBase[0];
        }

        public bool HasActiveWinCondition()
        {
            return GetAction<Win>().Active;
        }

        public void UpdateTrigger()
        {
            if (AllTriggers.Where(x => x.Active).Any(x => x.Check()))
            {
                Active = true;
            }
        }

        public void UpdateActions()
        {
            if (Active)
            {
                foreach (var item in AllActions.Where(x => x.Active))
                {
                    item.Execute(gameObject);
                }
            }
        }
    }
}

