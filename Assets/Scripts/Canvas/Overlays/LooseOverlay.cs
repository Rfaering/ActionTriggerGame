using Assets.Scripts.Canvas.Elements;
using Assets.Scripts.World;
using UnityEngine;

namespace Assets.Scripts.Canvas.Overlays
{
    public class LooseOverlay : MonoBehaviour
    {
        public Sprite Image;

        private LoadLevel _loadLevels;
        private Runner _runner;
        private OverlayManager _overlayManager;

        public void Start()
        {
            _loadLevels = FindObjectOfType<LoadLevel>();
            _overlayManager = FindObjectOfType<OverlayManager>();
            _runner = FindObjectOfType<Runner>();
        }

        public void ResumeLevel()
        {
            _runner.StopRunning();
            _overlayManager.CloseActiveOverlay();
        }

        public void RestartLevel()
        {
            _loadLevels.LoadCurrentLevel();
            _overlayManager.CloseActiveOverlay();
        }
    }
}
