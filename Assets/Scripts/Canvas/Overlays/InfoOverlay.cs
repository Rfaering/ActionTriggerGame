using Assets.Scripts.Canvas.Elements;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Canvas.Overlays
{
    public class InfoOverlay : MonoBehaviour
    {
        private OverlayManager _overlayManager;
        private Action _callback;

        public void Start()
        {
            _callback = () => { };
            _overlayManager = FindObjectOfType<OverlayManager>();
        }

        public InfoOverlay SetImageAndDescription(string resourceName, string text)
        {
            transform.Find("Text").GetComponent<Text>().text = text;
            transform.Find("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>(resourceName);
            return this;
        }

        public InfoOverlay SetButtonAction(Action callBack)
        {
            _callback = callBack;
            return this;
        }

        public void ResumeLevel()
        {
            _callback();
            _overlayManager.CloseActiveOverlay();
        }
    }
}
