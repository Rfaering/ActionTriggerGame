using Assets.Scripts.Canvas.Elements;
using Assets.Scripts.World;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Canvas.Overlays
{
    public class InfoOverlay : MonoBehaviour
    {
        private Runner _runner;
        private OverlayManager _overlayManager;

        public void Start()
        {
            _overlayManager = FindObjectOfType<OverlayManager>();
            _runner = FindObjectOfType<Runner>();
        }

        public void SetImageAndDescription(string resourceName, string text)
        {
            transform.Find("Text").GetComponent<Text>().text = text;
            transform.Find("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>(resourceName);
        }

        public void ResumeLevel()
        {
            _runner.StopRunning();
            _overlayManager.CloseActiveOverlay();
        }
    }
}
