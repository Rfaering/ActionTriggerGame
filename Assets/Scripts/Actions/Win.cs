using System;
using UnityEngine;

namespace Assets.Scripts.Actions
{
    public class Win : Action
    {
        public override void Execute(GameObject gameObject)
        {            
            gameObject.GetComponent<Renderer>().material.color = new Color(1.0f, 0.2f, 1.0f);
        }        
    }
}
