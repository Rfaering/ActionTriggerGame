using UnityEngine;

public class MenuButtons : MonoBehaviour
{

    public void Start()
    {
        PlayerPrefs.SetInt("Level", 1);
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
