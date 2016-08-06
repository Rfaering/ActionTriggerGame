using Assets.Scripts.Tile;
using Assets.Scripts.Utils;
using Assets.Scripts.World.Tile;
using System;
using UnityEngine;

namespace Assets.Scripts.Actions
{
    public class Water : Action
    {
        public Water(GameObject owner) : base(owner)
        {
        }

        public override void Execute(GameObject gameObject)
        {
            gameObject.GetComponent<WaterState>().Watered = true;
        }

        public override void UpdateUI(GameObject gameobject)
        {
            gameobject.GetComponent<ImageSetter>().SetSpecialVisual(ImageSetter.SpecialTypes.Water);
            base.UpdateUI(gameobject);
        }
    }
}
