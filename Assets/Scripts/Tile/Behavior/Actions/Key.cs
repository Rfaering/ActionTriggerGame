using Assets.Scripts.Misc;
using Assets.Scripts.Utils;
using UnityEngine;

namespace Assets.Scripts.Tile.Behavior.Actions
{
    public class Key : Scripts.Actions.Action
    {
        public Key(GameObject owner) : base(owner)
        {
        }

        public override void Execute(GameObject gameObject)
        {
            gameObject.GetComponent<WaterState>().Watered = true;
            gameObject.GetComponent<ImageSetter>().RemoveSpecialVisual(ImageSetter.SpecialTypes.Key);
            foreach (var selectedBehavior in GameObject.FindObjectsOfType<SelectedBehavior>())
            {
                if (selectedBehavior.IsNameSelected("Lock"))
                {
                    (selectedBehavior.SelectedAction as Lock).Unlock();
                }
            }
        }

        public override void UpdateUI(GameObject gameObject)
        {
            gameObject.GetComponent<ImageSetter>().SetSpecialVisual(ImageSetter.SpecialTypes.Key);
            base.UpdateUI(gameObject);
        }
    }
}
