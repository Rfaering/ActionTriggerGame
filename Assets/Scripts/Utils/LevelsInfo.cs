using System.IO;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Utils
{
    public static class LevelsInfo
    {
        public static string LevelsDirectory = Path.Combine(".", "Assets/Resources/Levels/");

        public static string[] GetLevels()
        {
#if UNITY_STANDALONE_WIN

            var files = Directory.GetFiles(LevelsDirectory, "*.txt").Select(x => Path.GetFileNameWithoutExtension(x)).ToArray();
            return files;
#endif   

            TextAsset[] txts = Resources.LoadAll<TextAsset>("Levels");
            return txts.Select(x => x.name).ToArray();
        }

        public static string GetLevel(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return null;
            }

#if UNITY_STANDALONE_WIN
            var filePath = Path.GetFullPath(Path.Combine(LevelsDirectory, name + ".txt"));
            if (!File.Exists(filePath))
            {
                return null;
            }

            return File.ReadAllText(filePath);
#endif

            var level = Resources.Load<TextAsset>(@"Levels/" + name);

            if (level == null)
            {
                return null;
            }

            return level.text;
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
