using Assets.Scripts.Canvas.Elements;
using Assets.Scripts.Utils;
using Assets.Scripts.World;
using UnityEngine;

namespace Assets.Scripts.Canvas.Overlays
{
    public class WinOverlay : MonoBehaviour
    {                
        private LoadLevel _loadLevels;
        private OverlayManager _overlayManager;

        public void Start()
        {
            _loadLevels = GlobalGameObjects.World.Get().GetComponent<LoadLevel>();
            _overlayManager = GetComponentInParent<OverlayManager>();
        }

        public void NextLevel()
        {
            _loadLevels.CurrentLevelName = LevelsInfo.GetNextLevel(_loadLevels.CurrentLevelName);
            _loadLevels.LoadCurrentLevel();
            _overlayManager.CloseActiveOverlay();
        }

        public void RestartLevel()
        {
            _loadLevels.LoadCurrentLevel();
            _overlayManager.CloseActiveOverlay();
        }
    }
}
