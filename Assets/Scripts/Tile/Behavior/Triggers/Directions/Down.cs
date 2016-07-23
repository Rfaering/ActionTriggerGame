using Assets.Scripts.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Triggers
{
    public class Down : Next
    {
        public Down(GameObject owner) : base(owner)
        {
        }

        public override Direction TriggerDirection
        {
            get
            {
                return Direction.Down;
            }
        }

        public override void UpdateUI(GameObject gameobject)
        {
            gameobject.GetComponent<ImageSetter>().SetImage("Trigger/Down", Resources.Load<Sprite>("Icons/Down"));
            base.UpdateUI(gameobject);
        }
    }
}
