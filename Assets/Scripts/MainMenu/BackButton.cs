using UnityEngine;

public class BackButton : MonoBehaviour
{
    public void Execute()
    {
        FindObjectOfType<BeginLevel>()
            .GetComponentInChildren<LevelSelector>(true)
            .Hide();

        FindObjectOfType<BeginLevel>()
            .GetComponentInChildren<MenuButtons>(true)
            .Show();

        FindObjectOfType<BeginLevel>()
            .GetComponentInChildren<LevelPager>(true)
            .Hide();
    }
}
