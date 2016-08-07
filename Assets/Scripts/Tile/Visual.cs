using Assets.Scripts.Tile.Behavior;
using System;
using UnityEngine;

namespace Assets.Scripts.Tile
{
    public class Visual : MonoBehaviour
    {        
        private Runner _runner;

        public bool Editable { get; set; }

        void Start()
        {            
            _runner = GlobalGameObjects.World.Get().GetComponent<Runner>();
        }

        void Update()
        {
            if (_runner.IsRunning())
            {
                transform.Find("Background").GetComponent<SpriteRenderer>().color = new Color(1.0f, 0.50f, 0.0f);
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
