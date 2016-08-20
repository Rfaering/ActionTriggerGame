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
        public BuilderMode RuntimeMode
        {
            get { return Globals.BuildMode; }
            set
            {
                Globals.BuildMode = value;
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
