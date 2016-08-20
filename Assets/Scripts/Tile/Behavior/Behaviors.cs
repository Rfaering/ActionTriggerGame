using Assets.Scripts.Actions;
using Assets.Scripts.Misc;
using Assets.Scripts.Tile;
using Assets.Scripts.Tile.Behavior.Actions;
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
                    GetComponent<WaterState>().Watered = false;

                    var flower = GetComponent<Behaviors>().GetAction<Flower>();
                    if (flower != null)
                    {
                        flower.Done = false;
                    }

                    var blueFlower = GetComponent<Behaviors>().GetAction<FlowerBlue>();
                    if (blueFlower != null)
                    {
                        blueFlower.Done = false;
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
                behavior.Reset();
            }
        }

        public bool StartTrigger;

        public void Initialize()
        {
            AllTriggers = new Trigger[]
            {
                new UpDown(gameObject),
                new LeftRight(gameObject),
                new UpRight(gameObject),
                new DownLeft(gameObject),
                new LeftUp(gameObject),
                new RightDown(gameObject),
                new Cross(gameObject),
                new BridgeUpDown(gameObject)
            };

            AllActions = new Action[]
            {
                new Water(gameObject),
                new Flower(gameObject),
                new FlowerBlue(gameObject),
                new Timer(gameObject),
                new Lock(gameObject),
                new Key(gameObject),
                new Well(gameObject),
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
            return GetAction<Flower>().Active;
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
                var activeActions = AllActions.Where(x => x.Active);
                foreach (var item in activeActions)
                {
                    item.Execute(gameObject);
                }

                // Water if no actions is set
                if (!activeActions.Any())
                {
                    GetComponent<WaterState>().Watered = true;
                }
            }
        }
    }
}


