using UnityEngine;

namespace Assets.Scripts.UI
{
    public class DesignPanels : MonoBehaviour
    {
        public bool IsOverlayOpen { get { return Overlay != null; } }
        public GameObject Overlay { get; set; }

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
            if (IsOverlayOpen && Input.GetKeyDown(KeyCode.Escape))
            {
                CloseActiveOverlay();
            }
        }

        public void CloseActiveOverlay()
        {
            Overlay.SetActive(false);
            Overlay = null;
        }

        private void FindAndOpenSavePanel( string overlayName )
        {
            var savePanel = gameObject.transform.FindChild(overlayName).gameObject;
            savePanel.SetActive(true);
            Overlay = savePanel;
        }        
    }
}
