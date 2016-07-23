using System;

namespace Assets.Scripts.Serialization
{
    [Serializable()]
    public class TileData
    {
        public bool Visible;
        public TriggerData[] Triggers;
        public ActionData[] Actions;
    }
}


