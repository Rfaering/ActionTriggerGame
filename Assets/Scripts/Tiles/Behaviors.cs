using Assets.Scripts.Actions;
using Assets.Scripts.Misc;
using Assets.Scripts.Serialization;
using Assets.Scripts.Triggers;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Tiles
{
    public class Behaviors : MonoBehaviour
    {
        public Trigger[] AllTriggers = new Trigger[0];
        public Actions.Action[] AllActions = new Actions.Action[0];

        public bool Active { get; set; }
        public bool StartTrigger;

        private TileData _tileData;

        public void Start()
        {
            AllTriggers = new Trigger[]
            {
                GetComponent<Start>(),
                GetComponent<Up>(),
                GetComponent<Down>(),
                GetComponent<Left>(),
                GetComponent<Right>(),
            };

            AllActions = new Actions.Action[]
            {
                GetComponent<Kill>(),
                GetComponent<Win>()
            };
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

        public void UpdateTrigger()
        {
            var position = GetComponent<Position>();

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

            Active = false;
        }
    }
}
