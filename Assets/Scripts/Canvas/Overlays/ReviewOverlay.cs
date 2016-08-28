using Assets.Scripts.Canvas.Elements;
using Assets.Scripts.Stats;
using Assets.Scripts.World;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Canvas.Overlays
{
    public class ReviewOverlay : MonoBehaviour
    {
        private OverlayManager _overlayManager;

        public void Start()
        {
            _overlayManager = GetComponentInParent<OverlayManager>();
        }

        public void Send()
        {
            var levelName = FindObjectOfType<LoadLevel>().CurrentLevelName;

            var note = transform.Find("InputField").GetComponent<InputField>().text;

            LevelReview.SendLevelReview(levelName, note);

            GoBackInternal().ReviewSent();
        }

        public void GoBack()
        {
            GoBackInternal();
        }

        private WinOverlay GoBackInternal()
        {
            transform.Find("InputField").GetComponent<InputField>().text = string.Empty;

            _overlayManager.CloseActiveOverlay();
            return _overlayManager.OpenWinnerOverlay();
        }
    }
}

