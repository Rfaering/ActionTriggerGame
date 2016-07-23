using UnityEngine;
using Assets.Scripts.Misc;
using Assets.Scripts.Utils;
using System.Collections.Generic;

namespace Assets.Scripts.Buttons
{
    public class CanvasMenu : MonoBehaviour
    {
        public GameObject[] TriggerButtons;
        public GameObject[] ActionButtons;

        public void Start()
        {
            DisableAllButtons();

            TriggerButtons = GetChildrens("Triggers");
            ActionButtons = GetChildrens("Actions");
        }

        private static GameObject[] GetChildrens(string name)
        {
            List<GameObject> children = new List<GameObject>();
            foreach (Transform child in GameObject.Find(name).transform)
            {
                children.Add(child.gameObject);
            }
            return children.ToArray();
        }

        public void RenderOptionsForGameObject(GameObject gameobject)
        {
            SetTriggers(gameobject, TriggerButtons);
            SetTriggers(gameobject, ActionButtons);
        }

        public void SetOptions(BehaviorBase[] behaviors, BehaviorTypes type)
        {
            if (type == BehaviorTypes.Actions)
            {
                SetLookAndFeel(behaviors, ActionButtons);
            }

            if (type == BehaviorTypes.Triggers)
            {
                SetLookAndFeel(behaviors, TriggerButtons);
            }
        }

        private bool RenderBehaviorButton(BehaviorBase behavior)
        {
            var isInBuildMode = GlobalProperties.IsInBuildMode();
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

        private void SetLookAndFeel(BehaviorBase[] behaviors, GameObject[] buttons)
        {
            for (int i = 0; i < behaviors.Length; i++)
            {
                var behavior = behaviors[i];
                var button = buttons[i];
                button.GetComponent<Layout>().SetLayoutBasedOnBehavior(behavior);
            }
        }

        private void SetTriggers(GameObject gameobject, GameObject[] buttons)
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                var button = buttons[i];
                if (button.activeSelf)
                {
                    button.GetComponent<ButtonClick>().MatchActionWithSelection(gameobject);
                }

            }
        }
    }
}

