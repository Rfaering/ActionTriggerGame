using Assets.Scripts.Tile.Behavior;
using System;
using UnityEngine;

namespace Assets.Scripts.Tile
{
    public class Visual : MonoBehaviour
    {
        private WaterState _waterState;
        private Runner _runner;

        public bool Editable { get; set; }

        void Start()
        {
            _waterState = GetComponent<WaterState>();
            _runner = GlobalGameObjects.World.Get().GetComponent<Runner>();
        }

        void Update()
        {
            if (!_waterState.Watered)
            {
                if (Editable && !_runner.IsRunning())
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
}
