using Assets.Scripts.Tile;
using Assets.Scripts.Tile.Behavior;
using Assets.Scripts.Utils;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Actions
{
    public class Well : Action
    {
        public Well(GameObject owner) : base(owner)
        {
        }

        public override void Execute(GameObject gameObject)
        {
            foreach (var item in GameObject.FindObjectsOfType<SelectedBehavior>().Where(x => x.IsNameSelected("Well")))
            {
                item.GetComponent<WaterState>().Watered = true;
            }

        }

        public override void UpdateUI(GameObject gameobject)
        {
            gameobject.GetComponent<ImageSetter>().SetSpecialVisual(ImageSetter.SpecialTypes.Well);
            base.UpdateUI(gameobject);
        }
    }
}
