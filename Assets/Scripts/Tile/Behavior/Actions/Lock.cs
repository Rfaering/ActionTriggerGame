using Assets.Scripts.Utils;
using Assets.Scripts.World.Tile;
using UnityEngine;

namespace Assets.Scripts.Tile.Behavior.Actions
{
    public class Lock : Scripts.Actions.Action
    {
        public bool Locked { get; set; }

        public Lock(GameObject owner) : base(owner)
        {
            Locked = true;
        }

        public void Unlock()
        {
            Locked = false;
            _owner.GetComponent<ImageSetter>().RemoveSpecialVisual(ImageSetter.SpecialTypes.Lock);
            if (_owner.GetComponent<Behaviors>().Active)
            {
                Execute(_owner);
            }
        }

        public override void Execute(GameObject gameObject)
        {
            if (Locked == false)
            {
                gameObject.GetComponent<WaterState>().Watered = true;
            }
        }

        public override void UpdateUI(GameObject gameObject)
        {
            gameObject.GetComponent<ImageSetter>().SetSpecialVisual(ImageSetter.SpecialTypes.Lock);
            base.UpdateUI(gameObject);
        }

        public override void Reset()
        {
            Locked = true;
            base.Reset();
        }
    }
}
