using Assets.Scripts.Buttons;
using Assets.Scripts.Misc;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.World
{
    public class CreateButtons : MonoBehaviour
    {
        public GameObject obj;

        public int buttons = 5;

        public void BuildObject()
        {
            Destroy("Actions");
            Destroy("Triggers");
            Create("Triggers", BehaviorTypes.Triggers);
            Create("Actions", BehaviorTypes.Actions);
        }

        private void Create(string underChild, BehaviorTypes buttonType)
        {
            Vector3 position;

            if (buttonType == BehaviorTypes.Triggers)
            {
                position = this.transform.FindChild(underChild).transform.position;
            }
            else
            {
                position = this.transform.FindChild(underChild).transform.position;
            }

            var canvasMenu = GetComponent<CanvasMenu>();
            List<GameObject> gameObjects = new List<GameObject>();
            for (int i = 0; i < buttons; i++)
            {
                GameObject button;
                if (buttons <= 5)
                {
                    button = Instantiate(obj, position + new Vector3(0, -100f * i + 200, 0), Quaternion.Euler(Vector3.zero)) as GameObject;
                    button.transform.SetParent(this.transform.FindChild(underChild).transform, true);
                    button.transform.localScale = new Vector3(1, 1, 1);
                }
                else
                {
                    button = Instantiate(obj, position + new Vector3(-25 + 50 * (i % 2), -50f * Mathf.CeilToInt(i / 2) + 200, 0), Quaternion.Euler(Vector3.zero)) as GameObject;
                    button.transform.SetParent(this.transform.FindChild(underChild).transform, true);
                    button.transform.localScale = new Vector3(0.5f, 0.5f, 1);
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
