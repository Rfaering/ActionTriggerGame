using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class LevelsDropdown : MonoBehaviour
{
    public void Start()
    {
        PopulateOptions();
    }

    public void PopulateOptions()
    {
        string[] levels = LevelsInfo.GetLevels();
        PopulateOptions(levels);
    }

    private void PopulateOptions(string[] values)
    {
        var dropDown = gameObject.GetComponent<Dropdown>();
        dropDown.ClearOptions();
        dropDown.AddOptions(values.Select(x => new Dropdown.OptionData(x)).ToList());
    }

    private string GetSelectedLevel()
    {
        var dropDown = transform.GetComponent<Dropdown>();
        return dropDown.options[dropDown.value].text;
    }

    public void SetSelectedLevel()
    {
        var loadLevel = FindObjectOfType<LoadLevel>();
        loadLevel.CurrentLevelName = GetSelectedLevel();
        loadLevel.LoadCurrentLevel();
    }
}
