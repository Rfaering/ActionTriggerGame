using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Misc
{
    public abstract class BehaviorBase : MonoBehaviour
    {
        public Sprite Icon;        
        public abstract BehaviorTypes BehaviorType { get; }

        private bool _active;
        public bool Active
        {
            get { return _active; }
            set
            {
                _active = value;
                UpdateUI();
            }
        }

        private bool _availible;
        public bool Available
        {
            get { return _availible; }
            set { _availible = value; }
        }


        private void UpdateUI()
        {
            if ( _active )
            {
                this.transform
                .Find("Foreground")
                .Find(BehaviorType == BehaviorTypes.Triggers ? "Trigger" : "Action")
                .GetComponent<SpriteRenderer>().sprite = Icon;
            }
        }
    }
}
