using UnityEngine;
using UnityEngine.UI;

public class LevelPager : MonoBehaviour
{
    public int CurrentPage = 0;

    public const int Min = 0;
    public const int Max = 7;

    public const int PageSize = 6;

    private Button Forward;
    private Button Back;

    public void Start()
    {
        Forward = transform.Find("Forward").GetComponent<Button>();
        Back = transform.Find("Back").GetComponent<Button>();
        UpdateButtons();
    }

    public void GoForward()
    {
        CurrentPage++;
        FindObjectOfType<LevelSelector>().SetPage(CurrentPage, PageSize);
        FindObjectOfType<GroupImage>().SetGroup(CurrentPage + 1);
        UpdateButtons();
    }

    public void GoBackward()
    {
        CurrentPage--;
        FindObjectOfType<LevelSelector>().SetPage(CurrentPage, PageSize);
        FindObjectOfType<GroupImage>().SetGroup(CurrentPage + 1);
        UpdateButtons();
    }

    public void UpdateButtons()
    {
        if (CurrentPage == Min)
        {
            Back.interactable = false;
        }
        else
        {
            Back.interactable = true;
        }

        if (CurrentPage == Max)
        {
            Forward.interactable = false;
        }
        else
        {
            Forward.interactable = true;
        }
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
