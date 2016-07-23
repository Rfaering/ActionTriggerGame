﻿using Assets.Scripts.Canvas.Overlays;
using UnityEngine;
using UnityStandardAssets.ImageEffects;

namespace Assets.Scripts.Canvas.Elements
{
    public class OverlayManager : MonoBehaviour
    {
        public bool IsOverlayOpen { get { return Overlay != null; } }
        public GameObject Overlay { get; set; }

        public void OpenSaveOverlay()
        {
            FindAndOpenPanel<SaveOverlay>().SetInputField();            
        }

        public void OpenNewBoardOverlay()
        {
            FindAndOpenPanel<NewOverlay>();
        }

        public WinOverlay OpenWinnerOverlay()
        {
            return FindAndOpenPanel<WinOverlay>();
        }

        public LooseOverlay OpenLooseOverlay()
        {
            return FindAndOpenPanel<LooseOverlay>();
        }

        public void Start()
        {

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
            if (Overlay != null)
            {
                FindObjectOfType<BlurOptimized>().enabled = false;
                Overlay.SetActive(false);
                Overlay = null;
            }

        }

        private T FindAndOpenPanel<T>()
        {
            FindObjectOfType<BlurOptimized>().enabled = true;
            CloseActiveOverlay();
            var component = GetComponentInChildren<T>(true);
            var panel = (component as MonoBehaviour).gameObject;
            panel.SetActive(true);
            Overlay = panel;
            return panel.GetComponent<T>();
        }
    }
}