using Assets.Scripts.Utils;
using Assets.Scripts.World;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Canvas.Elements
{
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
            var loadLevel = GlobalGameObjects.World.Get().GetComponent<LoadLevel>();
            loadLevel.CurrentLevelName = GetSelectedLevel();
            loadLevel.LoadCurrentLevel();
        }
    }
}
