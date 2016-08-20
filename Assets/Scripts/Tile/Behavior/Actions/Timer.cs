using Assets.Scripts.Tile;
using Assets.Scripts.Utils;
using Assets.Scripts.World.Tile;
using System;
using UnityEngine;

namespace Assets.Scripts.Actions
{
    public class Timer : Action
    {
        private int Counter = COUNTER;

        private const int COUNTER = 3;

        public Timer(GameObject owner) : base(owner)
        {
        }

        public override void Execute(GameObject gameObject)
        {
            if (Counter < 3)
            {
                gameObject.GetComponent<ImageSetter>().DisableVisual("Special/Timer/" + (Counter + 1));
            } else
            {
                gameObject.GetComponent<ImageSetter>().DisableVisual("Special/Timer/Icon");
            }

            if (Counter == 0)
            {
                gameObject.GetComponent<WaterState>().Watered = true;                
            }
            else
            {
                gameObject.GetComponent<ImageSetter>().ActivateVisual("Special/Timer/" + Counter);
            }

            Counter--;
        }

        public override void UpdateUI(GameObject gameObject)
        {
            gameObject.GetComponent<ImageSetter>().SetSpecialVisual(ImageSetter.SpecialTypes.Timer);
            gameObject.GetComponent<ImageSetter>().ActivateVisual("Special/Timer/Icon");
            gameObject.GetComponent<ImageSetter>().DisableVisual("Special/Timer/3");
            gameObject.GetComponent<ImageSetter>().DisableVisual("Special/Timer/2");
            gameObject.GetComponent<ImageSetter>().DisableVisual("Special/Timer/1");
            base.UpdateUI(gameObject);
        }

        public override void Reset()
        {
            Counter = COUNTER;
            base.Reset();
        }
    }
}

