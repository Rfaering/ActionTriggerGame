using Assets.Scripts.Tile;
using Assets.Scripts.World.Tile;
using System;
using UnityEngine;

namespace Assets.Scripts.Actions
{
    public class Paint : Action
    {
        private Color color = Color.yellow;

        public Paint(GameObject owner) : base(owner)
        {
        }

        public override void Execute(GameObject gameObject)
        {            
            _owner.transform.Find("Foreground").GetComponent<SpriteRenderer>().color = color;
        }

        public override void UpdateUI(GameObject gameObject)
        {
            gameObject.transform.Find("Action").GetComponent<SpriteRenderer>().color = color;
            base.UpdateUI();
        }

        public override void ClearUI(GameObject gameObject)
        {
            gameObject.transform.Find("Action").GetComponent<SpriteRenderer>().color = new Color(0.8f, 0.8f, 0.8f);
            base.ClearUI();
        }

        public override void ResetUI()
        {
            _owner.transform.Find("Foreground").GetComponent<SpriteRenderer>().color = new Color(0.8f, 0.8f, 0.8f);
            base.ResetUI();
        }
    }
}
