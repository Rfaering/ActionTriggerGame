using Assets.Scripts.Tile;
using Assets.Scripts.Utils;
using UnityEngine;

namespace Assets.Scripts.Actions
{
    public class Bacteria : Action
    {
        public GameObject Above { get; set; }

        public bool MovingUp = true;

        public Bacteria(GameObject owner) : base(owner)
        {
        }

        public override void Execute(GameObject gameObject)
        {
            gameObject.GetComponent<WaterState>().Watered = true;
        }

        public void Move()
        {
            if (MovingUp)
            {
                if (Above.GetComponent<Position>().Up != null)
                {
                    Above = Above.GetComponent<Position>().Up;
                    SetPositionToAbove();
                }
                else
                {
                    MovingUp = false;
                    Move();
                }
            }
            else
            {
                if (Above.GetComponent<Position>().Down != null)
                {
                    Above = Above.GetComponent<Position>().Down;
                    SetPositionToAbove();
                }
                else
                {
                    MovingUp = true;
                    Move();
                }
            }

        }

        public override void Reset()
        {
            Above = _owner;
            SetPositionToAbove();
            base.Reset();
        }

        public override void UpdateUI(GameObject gameobject, bool preview = false)
        {
            Above = _owner;
            gameobject.GetComponent<ImageSetter>().SetSpecialVisual(ImageSetter.SpecialTypes.Bacteria, preview);
            base.UpdateUI(gameobject);
        }

        private void SetPositionToAbove()
        {
            if (Above != null)
            {
                var currentPosition = _owner.GetComponent<ImageSetter>().GetVisualOf(ImageSetter.SpecialTypes.Bacteria)
                    .transform.position;
                _owner.GetComponent<ImageSetter>().GetVisualOf(ImageSetter.SpecialTypes.Bacteria)
                    .transform.position = new Vector3(Above.transform.position.x, Above.transform.position.y, currentPosition.z);
            }

        }
    }
}
