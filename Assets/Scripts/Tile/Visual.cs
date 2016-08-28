using Assets.Scripts.Buttons;
using Assets.Scripts.Misc;
using UnityEngine;

namespace Assets.Scripts.Tile
{
    public class Visual : MonoBehaviour
    {        
        private Runner _runner;

        public bool Editable { get; set; }

        void Start()
        {            
            _runner = FindObjectOfType<Runner>();
        }

        void Update()
        {
            if (_runner.IsRunning())
            {                
                transform.Find("Background").GetComponent<SpriteRenderer>().color = new Color(0.30f, 0.2f, 0.0f);
                return;
            }

            if( Globals.InputMode == InputMode.DragAndDrop && DraggableButton.itemBeingDragged == null)
            {
                transform.Find("Background").GetComponent<SpriteRenderer>().color = new Color(0.30f, 0.2f, 0.0f);
                return;
            }

            if (Editable)
            {
                transform.Find("Background").GetComponent<SpriteRenderer>().color = new Color(0.0f, 0.4f, 0.0f);
            }
            else
            {
                transform.Find("Background").GetComponent<SpriteRenderer>().color = Color.red;
            }

        }
    }
}
