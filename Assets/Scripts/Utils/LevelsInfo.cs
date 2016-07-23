using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Utils
{
    public static class LevelsInfo
    {
        //public static string LevelsDirectory = Path.Combine(Application.persistentDataPath, "Levels");

        public static string[] GetLevels()
        {
            TextAsset[] txts = Resources.LoadAll<TextAsset>("Levels");
            return txts.Select(x => x.name).ToArray();
        }

        public static string GetNextLevel(string currentLevel)
        {
            var levels = GetLevels().ToList();
            var index = levels.IndexOf(currentLevel);

            if (index >= 0 && index < (levels.Count - 1))
            {
                return levels[index + 1];
            }

            return currentLevel;
        }
    }
}
