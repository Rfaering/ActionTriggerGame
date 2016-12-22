using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BeginLevel : MonoBehaviour
{
    private int CurrentLevel = 1;

    public void Start()
    {
        CurrentLevel = PlayerPrefs.GetInt("Level", 1);
        CurrentLevel = 1;

        transform.Find("MainMenu/Play")
            .GetComponentInChildren<Text>()
            .text = CurrentLevel == 1 ? "Play" : "Continue";
    }

    public void BeginGame(int level)
    {
        Globals.BuildMode = BuilderMode.Running;
        Globals.InitialLevel = level;
        SceneManager.LoadScene("Game");
    }
    
    public void BeginGame()
    {
        BeginGame(CurrentLevel);
    }
}
