using Assets.Scripts.Canvas.Elements;
using Assets.Scripts.Canvas.Overlays;
using Assets.Scripts.Misc;
using Assets.Scripts.World;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Levels
{
    public class LevelInfoManager : MonoBehaviour
    {
        int groupSize = 6;

        private class LevelInfo
        {
            public string Name { get; set; }
            public string Description { get; set; }
        }

        private LevelInfo[] InfoLevels = new LevelInfo[]
        {
            new LevelInfo() { Name = "Level 001", Description = "Connect water to flower" }
        };

        public void ShowWinForCurrentLevel()
        {
            var currentLevel = FindObjectOfType<LoadLevel>().CurrentLevelName;
            var levelNumber = int.Parse(currentLevel.Replace("Level ", ""));

            if (levelNumber == Globals.MaxLevel)
            {
                ShowGameWon();
                return;
            }

            var groupNumber = (levelNumber / groupSize) + 1;

            StoreProgress(levelNumber + 1);

            var winnerOverlay = FindObjectOfType<OverlayManager>()
                    .OpenWinnerOverlay();

            winnerOverlay.SetNumberOfLevels(levelNumber % groupSize);

            if (levelNumber > 0 && levelNumber % groupSize == 0)
            {
                winnerOverlay.CrossFadeImage("Group " + (groupNumber - 1), "Group " + groupNumber);
            }
            else
            {
                winnerOverlay.SetImage("Group " + groupNumber);
            }
        }

        public void StoreProgress(int newLevel)
        {
            PlayerPrefs.SetInt("Level", newLevel);
        }

        public void ShowInfoForLevel(string level)
        {
            var currentLevelInfo = InfoLevels.FirstOrDefault(x => x.Name == level);
            if (currentLevelInfo != null)
            {
                FindObjectOfType<OverlayManager>()
                    .OpenInfoOverlay()
                    .SetImageAndDescription("Info/" + currentLevelInfo.Name, currentLevelInfo.Description);
            }

        }

        public void ShowGameWon()
        {
            FindObjectOfType<OverlayManager>()
                .OpenInfoOverlay()
                .SetImageAndDescription("Info/Won", "Congratulations you won the game")
                .SetButtonAction(() =>
                {
                    SceneManager.LoadScene(0);
                });
        }
    }
}
