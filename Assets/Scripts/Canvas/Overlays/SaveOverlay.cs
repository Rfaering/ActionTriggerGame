using UnityEngine;
using Assets.Scripts.World;
using Assets.Scripts.Canvas.Elements;
using UnityEngine.UI;

namespace Assets.Scripts.Canvas.Overlays
{
    public class SaveOverlay : MonoBehaviour
    {
        public string GetInputField()
        {
            return transform.Find("LevelName").GetComponent<InputField>().text;
        }

        public void SetInputField()
        {
            transform.Find("LevelName").GetComponent<InputField>().text = FindObjectOfType<SaveLevel>().SaveLevelName;
        }

        public void Save()
        {
            var saveLevel = GlobalGameObjects.World.Get().GetComponent<SaveLevel>();
            saveLevel.SaveLevelName = GetInputField();
            saveLevel.SaveCurrentLevel();
            GetComponentInParent<OverlayManager>().CloseActiveOverlay();
            GlobalGameObjects.DesignPanel.Get().GetComponentInChildren<LevelsDropdown>().PopulateOptions();
        }
    }
}
