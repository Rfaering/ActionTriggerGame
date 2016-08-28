using UnityEngine;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    private int maxLevel = 1;

    public void Start()
    {
        maxLevel = PlayerPrefs.GetInt("Level", 1);
        SetPage(0, 6);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void SetPage(int pageNumber, int pageSize)
    {
        var levelNumber = pageNumber * pageSize + 1;
        foreach (var item in GetComponentsInChildren<SelectLevel>())
        {
            item.SetLevelNumber(levelNumber);
            item.GetComponentInChildren<Text>().text = string.Format("Level {0:00}", levelNumber);
            item.GetComponent<Button>().interactable = levelNumber <= maxLevel;

            levelNumber++;
        }
    }
}
