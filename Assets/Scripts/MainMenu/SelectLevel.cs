using UnityEngine;

public class SelectLevel : MonoBehaviour
{
    public int LevelNumber = 1;

    public void BeginLevel()
    {
        FindObjectOfType<BeginLevel>().BeginGame(LevelNumber);
    }

    internal void SetLevelNumber(int levelNumber)
    {
        LevelNumber = levelNumber;
    }
}
