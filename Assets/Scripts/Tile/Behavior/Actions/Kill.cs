using Assets.Scripts.Tile;
using Assets.Scripts.World.Tile;
using System;
using UnityEngine;

namespace Assets.Scripts.Actions
{
    public class Kill : Action
    {
        public Kill(GameObject owner) : base(owner)
        {
        }

        public override string IconName
        {
            get
            {
                return "skull";
            }
        }

        public override void Execute(GameObject gameObject)
        {
            gameObject.GetComponent<Position>().Death = true;
        }
    }
}
