using Assets.Scripts.Buttons;
using Assets.Scripts.Misc;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Assets.Scripts.World
{
    public class CreateButtons : MonoBehaviour
    {
        public GameObject obj;

        public int buttons = 6;

        public void BuildObject()
        {
            Destroy("Actions");
            Destroy("Triggers");
            Create("Triggers", BehaviorTypes.Triggers, buttons);
            Create("Actions", BehaviorTypes.Actions, buttons);
        }

        public void BuildObject(BehaviorTypes type, int count)
        {
            if (type == BehaviorTypes.Actions)
            {
                Destroy("Actions");
                Create("Actions", BehaviorTypes.Actions, count);
            }
            else
            {
                Destroy("Triggers");
                Create("Triggers", BehaviorTypes.Triggers, count);
            }
        }

        private void Create(string underChild, BehaviorTypes buttonType, int count)
        {
            var canvasMenu = GetComponent<CanvasMenu>();
            List<GameObject> gameObjects = new List<GameObject>();
            for (int i = 0; i < count; i++)
            {
                GameObject button;
                if (count <= 5)
                {
                    var offSet = buttonType == BehaviorTypes.Actions ? -13.0f : 13.0f;

                    button = Instantiate(obj) as GameObject;
                    button.transform.SetParent(this.transform.FindChild(underChild).transform);
                    button.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
                    button.transform.localPosition = new Vector3(offSet, -85f * i - 50, 0);
                }
                else if (count > 5 && count < 8)
                {
                    var offSet = buttonType == BehaviorTypes.Actions ? -13f : 13f;

                    button = Instantiate(obj) as GameObject;
                    button.transform.SetParent(this.transform.FindChild(underChild).transform);
                    button.transform.localScale = new Vector3(1.1f, 1.1f, 1);
                    button.transform.localPosition = new Vector3(offSet, -75f * i - 40, 0);
                }
                else
                {
                    var offSet = buttonType == BehaviorTypes.Actions ? -37.5f : -12.5f;

                    button = Instantiate(obj) as GameObject;
                    button.transform.SetParent(this.transform.FindChild(underChild).transform, true);
                    button.transform.localScale = new Vector3(0.75f, 0.75f, 1);
                    button.transform.localPosition = new Vector3(offSet + 50 * (i % 2), -50f * Mathf.CeilToInt(i / 2) - 70f, 0);
                }

                gameObjects.Add(button);
            }

            if (buttonType == BehaviorTypes.Triggers)
            {
                canvasMenu.TriggerButtons = gameObjects.ToArray();
            }
            else
            {
                canvasMenu.ActionButtons = gameObjects.ToArray();
            }
        }

        internal void HideButtons(BehaviorTypes triggers)
        {
            if (triggers == BehaviorTypes.Triggers)
            {
                Hide("Triggers");
            }
            if (triggers == BehaviorTypes.Actions)
            {
                Hide("Actions");
            }
        }

        private void Hide(string underChild)
        {
            List<Transform> children = new List<Transform>();

            foreach (Transform t in this.transform.FindChild(underChild).transform)
            {
                children.Add(t);
            }

            foreach (Transform t in children)
            {
                t.gameObject.SetActive(false);
            }
        }

        private void Destroy(string underChild)
        {
            List<Transform> children = new List<Transform>();

            foreach (Transform t in this.transform.FindChild(underChild).transform)
            {
                children.Add(t);
            }

            foreach (Transform t in children)
            {
                DestroyImmediate(t.gameObject);
            }
        }
    }
}
