using System.Collections.Generic;
using UnityEngine.Analytics;

public static class LevelReview
{
    private static readonly Dictionary<string, object> ReviewData = new Dictionary<string, object>();

    public static void SendLevelReview(string levelName, string note)
    {
        ReviewData["Note"] = levelName + ": " + note;

        Analytics.CustomEvent("LevelComment", ReviewData);
    }
}
