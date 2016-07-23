using Assets.Scripts.Buttons;
using Assets.Scripts.Misc;
using Assets.Scripts.Tile;
using Assets.Scripts.Utils;
using Assets.Scripts.World;
using UnityEngine;

namespace Assets.Scripts
{
    public class BuildMode : MonoBehaviour
    {
        private BuilderMode _runtimeMode;
        public BuilderMode RuntimeMode
        {
            get { return _runtimeMode; }
            set
            {
                _runtimeMode = value;
                SetNewRuntimeMode(value);
            }
        }

        void Update()
        {
            if (!GlobalProperties.IsOverlayPanelOpen())
            {
                if (Input.GetKeyDown(KeyCode.S))
                {
                    RuntimeMode = RuntimeMode == BuilderMode.DesignMode ? BuilderMode.Running : BuilderMode.DesignMode;
                }
            }
        }

        private void SetNewRuntimeMode(BuilderMode value)
        {
            var createButtons = FindObjectOfType<CreateButtons>();
            createButtons.buttons = value == BuilderMode.DesignMode ? 16 : 5;
            createButtons.BuildObject();
            GlobalGameObjects.DesignPanel.Get().GetComponent<DesignPanelVisibility>().SetVisibility(value == BuilderMode.DesignMode);

            foreach (var visibility in GlobalGameObjects.World.Get().GetComponentsInChildren<Visibility>(true))
            {
                visibility.UpdateVisiblity();
            }

            UnSelectAll();
            GetComponent<Runner>().StopRunning();

            var level = FindObjectOfType<LoadLevel>();
            level.LoadCurrentLevel();
        }

        private void UnSelectAll()
        {
            Selection[] selections = GetComponentsInChildren<Selection>();
            foreach (var item in selections)
            {
                item.Selected = false;
            }

            GameObject.Find("Canvas")
                .GetComponent<CanvasMenu>()
                .DisableAllButtons();
        }
    }
}
