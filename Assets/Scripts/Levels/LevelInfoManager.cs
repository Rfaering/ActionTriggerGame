using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelInfoManager : MonoBehaviour
{
    int groupSize = 6;

    private class LevelInfo
    {
        public string Name { get; set; }
        public string Hint { get; set; }
    }

    private LevelInfo[] Hints = new LevelInfo[]
    {
            new LevelInfo() { Name = "Level 001", Hint = "Hint1" }
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
        var maxLevel = PlayerPrefs.GetInt("Level");
        if (newLevel > maxLevel)
        {
            PlayerPrefs.SetInt("Level", newLevel);
        }
    }

    public void ShowInfoForLevel(string level)
    {
        var currentLevelInfo = Hints.FirstOrDefault(x => x.Name == level);
        if (currentLevelInfo != null)
        {
            FindObjectOfType<CanvasMenu>()
                .GetComponentInChildren<Hint>(true)
                .PlayHint(currentLevelInfo.Hint);
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
