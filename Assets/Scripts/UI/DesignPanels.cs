using UnityEngine;

namespace Assets.Scripts.UI
{
    public class DesignPanels : MonoBehaviour
    {
        public bool IsPanelOpen { get { return Panel != null; } }
        public GameObject Panel { get; set; }

        public void OpenSave()
        {
            FindAndOpenSavePanel("Save Overlay");
        }

        public void OpenNew()
        {
            FindAndOpenSavePanel("New Overlay");
        }

        public void Update()
        {
            if (IsPanelOpen && Input.GetKeyDown(KeyCode.Escape))
            {
                CloseActiveOverlay();
            }
        }

        private void CloseActiveOverlay()
        {
            Panel.SetActive(false);
            Panel = null;
        }

        private void FindAndOpenSavePanel( string overlayName )
        {
            var savePanel = this.gameObject.transform.FindChild(overlayName).gameObject;
            savePanel.SetActive(true);
            Panel = savePanel;
        }        
    }
}
