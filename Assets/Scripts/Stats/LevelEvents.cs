using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.Analytics;

namespace Assets.Scripts.Stats
{
    public static class LevelEvents
    {
        private static readonly Dictionary<string, object> CompletedEvent = new Dictionary<string, object>();

        public static void SendCompletedEvent(string levelName)
        {

            CompletedEvent["Level"] = levelName;
            Analytics.CustomEvent("LevelCompleted", CompletedEvent);
        }
    }
}
