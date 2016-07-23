using Assets.Scripts.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Triggers
{
    public class Left : Next
    {
        public Left(GameObject owner) : base(owner)
        {
        }

        public override Direction TriggerDirection
        {
            get
            {
                return Direction.Left;
            }
        }

        public override void UpdateUI(GameObject gameobject)
        {
            gameobject.GetComponent<ImageSetter>().SetImage("Trigger/Left", Resources.Load<Sprite>("Icons/Left"));
            base.UpdateUI(gameobject);
        }
    }
}
