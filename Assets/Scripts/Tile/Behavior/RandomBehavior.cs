using Assets.Scripts.World.Tile;
using UnityEngine;
using System.Linq;

namespace Assets.Scripts.Tile.Behavior
{
    public class RandomBehavior : MonoBehaviour
    {
        private Behaviors _behaviors;
        private SelectedBehavior _selectedBehavior;

        void Start()
        {
            _behaviors = GetComponent<Behaviors>();
            _selectedBehavior = GetComponent<SelectedBehavior>();
        }

        public void SelectRandomAvailibleTriggerAndAction()
        {
            var avalibleActions = _behaviors.AllActions.Where(x => x.Available).ToArray();
            if (avalibleActions.Any())
            {
                _selectedBehavior.SelectedAction = avalibleActions[Random.Range(0, avalibleActions.Length)];
            }

            var avalibleTriggers = _behaviors.AllTriggers.Where(x => x.Available).ToArray();
            if (avalibleTriggers.Any())
            {
                _selectedBehavior.SelectedTrigger = avalibleTriggers[Random.Range(0, avalibleTriggers.Length)];
            }
        }
    }
}
