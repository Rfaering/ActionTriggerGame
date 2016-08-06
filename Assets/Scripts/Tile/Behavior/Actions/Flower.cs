using Assets.Scripts.Tile;
using Assets.Scripts.Utils;
using Assets.Scripts.World.Tile;
using System;
using UnityEngine;

namespace Assets.Scripts.Actions
{
    public class Flower : Action
    {
        public Flower(GameObject owner) : base(owner)
        {
        }

        public bool Done { get; set; }

        public override void Execute(GameObject gameObject)
        {
            Done = true;
            _owner.GetComponent<WaterState>().Watered = true;
        }

        public override void UpdateUI(GameObject gameobject)
        {
            gameobject.GetComponent<ImageSetter>().SetSpecialVisual(ImageSetter.SpecialTypes.Flower);
            base.UpdateUI(gameobject);
        }
    }
}
