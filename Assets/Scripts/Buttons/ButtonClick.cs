using Assets.Scripts.Misc;
using Assets.Scripts.Tile.Behavior;
using Assets.Scripts.Utils;
using Assets.Scripts.World.Tile;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using Assets.Scripts.Tile;

namespace Assets.Scripts.Buttons
{
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
            var behaviors = GlobalGameObjects.World.Get().GetComponentsInChildren<Selection>();
            foreach (var item in behaviors.Where(x => x.Selected))
            {
                ToggleSingleSelection(item.GetComponent<SelectedBehavior>(), removeSelection);
            }
        }

        private void ToggleSingleSelection(SelectedBehavior selectedBehavior, bool removeSelection)
        {
            if (removeSelection)
            {
                selectedBehavior.RemoveSelection(this.gameObject.name);
            }
            else
            {
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
            var behaviors = GlobalGameObjects.World.Get().GetComponentsInChildren<Selection>();

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
            if (currentBehavior == null || !currentBehavior.Available)
            {
                this.GetComponent<Image>().color = Color.gray;
            }
            else
            {
                this.GetComponent<Image>().color = Color.white;
            }
        }

        public void MatchActionWithSelection(GameObject gameObject)
        {
            CurrentSelectedTile = gameObject;
            SetColorBasedOnAvailibility();
        }
    }
}
