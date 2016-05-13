using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using Assets.Scripts.Misc;
using Assets.Scripts.Tiles;

namespace Assets.Scripts.UI
{
    public class CanvasMenu : MonoBehaviour
    {
        public GameObject[] TriggerButtons;
        public GameObject[] ActionButtons;

        public void Start()
        {
            DisableAllButtons();            
        }
        

        public void RenderOptionsForGameObject(GameObject gameobject)
        {
            var triggerList = gameobject.GetComponent<Behaviors>();
            
            DisableAllButtons();
            SetTriggers(gameobject, triggerList.AllTriggers.Where(RenderBehaviorButton)
                .ToArray(), TriggerButtons);

            SetTriggers(gameobject, triggerList.AllActions.Where(RenderBehaviorButton)
                .ToArray(), ActionButtons);
        }

        private bool RenderBehaviorButton(BehaviorBase behavior)
        {
            var isInBuildMode = Globals.IsInBuildMode();
            return isInBuildMode || behavior.Available;

        }

        public void DisableAllButtons()
        {
            foreach (var button in TriggerButtons)
            {
                button.gameObject.SetActive(false);
            }

            foreach (var button in ActionButtons)
            {
                button.gameObject.SetActive(false);
            }
        }

        private void SetTriggers( GameObject gameobject, BehaviorBase[] behaviors, GameObject[] buttons )
        {            
            for (int i = 0; i < behaviors.Length; i++)
            {
                var action = behaviors[i];
                var button = buttons[i];
                button.gameObject.SetActive(true);
                button.GetComponent<ButtonClick>().SetBehavior(gameobject, action);
            }
        }
    }
}

