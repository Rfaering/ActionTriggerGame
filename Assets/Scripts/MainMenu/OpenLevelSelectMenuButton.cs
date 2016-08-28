using UnityEngine;

public class OpenLevelSelectMenuButton : MonoBehaviour
{
    public void Execute()
    {
        FindObjectOfType<BeginLevel>()
            .GetComponentInChildren<LevelSelector>(true)
            .Show();

        FindObjectOfType<BeginLevel>()
            .GetComponentInChildren<LevelPager>(true)
            .Show();

        FindObjectOfType<BeginLevel>()
            .GetComponentInChildren<MenuButtons>(true)
            .Hide();
    }
}
