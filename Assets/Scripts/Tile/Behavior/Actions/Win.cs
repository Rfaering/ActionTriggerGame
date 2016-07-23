using Assets.Scripts.Tile;
using Assets.Scripts.World.Tile;
using System;
using UnityEngine;

namespace Assets.Scripts.Actions
{
    public class Win : Action
    {
        public Win(GameObject owner) : base(owner)
        {
        }

        public bool Done { get; set; }

        public override string IconName
        {
            get
            {
                return "FinishFlag";
            }
        }

        public override void Execute(GameObject gameObject)
        {
            Done = true;
            _owner.GetComponent<Position>().Death = true;
        }
    }
}
