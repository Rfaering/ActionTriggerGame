using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Serialization
{
    [Serializable()]
    public class TileData
    {
        public bool Locked;
        public TriggerData[] Triggers;
        public ActionData[] Actions;
    }
}

