using System;
using UnityEngine;

namespace Assets.Scripts.Actions
{
    public class Kill : Action
    {
        public override void Execute(GameObject gameObject)
        {
            gameObject.GetComponent<Position>().Death = true;            
        }
    }
}
