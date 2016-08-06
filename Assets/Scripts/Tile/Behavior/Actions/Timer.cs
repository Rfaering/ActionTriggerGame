using Assets.Scripts.Tile;
using Assets.Scripts.World.Tile;
using System;
using UnityEngine;

namespace Assets.Scripts.Actions
{
    public class Timer : Action
    {
        private int Counter = 3;

        public Timer(GameObject owner) : base(owner)
        {
        }

        public override void Execute(GameObject gameObject)
        {
            _owner.transform                
                .Find("Action/Full")
                .GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Icons/" + (Counter > 0 ? Counter.ToString() : "skull"));

            if (Counter == 0)
            {
                gameObject.GetComponent<WaterState>().Watered = true;
            }

            Counter--;
        }

        public override void ResetUI()
        {
            Counter = 3;
            base.ResetUI();
        }
    }
}
