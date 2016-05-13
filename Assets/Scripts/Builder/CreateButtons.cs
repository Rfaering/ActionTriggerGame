using Assets.Scripts.Misc;
using Assets.Scripts.UI;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Builder
{
    public class CreateButtons : MonoBehaviour
    {
        public GameObject obj;

        public int buttons = 4;        

        public void BuildObject()
        {
            DestroyChildrens("Actions");
            DestroyChildrens("Triggers");
            
            CreateBoard("Triggers", BehaviorTypes.Triggers );
            CreateBoard("Actions", BehaviorTypes.Actions );
        }

        private void CreateBoard( string underChild, BehaviorTypes buttonType )
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
                var button = Instantiate(obj, position + new Vector3(0, -1.75f * i  + 3.5f, 0), Quaternion.Euler(Vector3.zero)) as GameObject;

                button.transform.SetParent(this.transform.FindChild(underChild).transform, true);
                button.transform.localScale = new Vector3(1, 1, 1);

                gameObjects.Add(button);
            }

            if ( buttonType == BehaviorTypes.Triggers )
            {
                canvasMenu.TriggerButtons = gameObjects.ToArray();
            } else
            {
                canvasMenu.ActionButtons = gameObjects.ToArray();
            }            
        }

        private void DestroyChildrens( string underChild )
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
