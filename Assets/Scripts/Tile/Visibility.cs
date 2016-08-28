using Assets.Scripts.Utils;
using UnityEngine;

namespace Assets.Scripts.Tile
{
    public class Visibility : MonoBehaviour
    {
        public bool IsVisible;
        public GameObject visibilityIcon;

        public void Start()
        {
            visibilityIcon = transform
                        .Find("Visible")
                        .gameObject;
        }

        public void Update()
        {
            UpdateInput();
            UpdateVisiblity();
        }

        private void UpdateInput()
        {
            if (GlobalProperties.IsInBuildMode() &&
                !GlobalProperties.IsOverlayPanelOpen &&
                GetComponent<Selection>().Selected &&
                Input.GetKeyDown(KeyCode.V))
            {
                IsVisible = !IsVisible;
            }

        }

        public void UpdateVisiblity()
        {
            if (visibilityIcon == null)
            {
                return;
            }

            if (IsVisible)
            {
                visibilityIcon.SetActive(false);
            }
            else
            {
                if (GlobalProperties.IsInBuildMode())
                {
                    if (!isActiveAndEnabled)
                    {
                        gameObject.SetActive(true);
                    }

                    visibilityIcon.SetActive(true);
                }
                else
                {

                    gameObject.SetActive(false);
                }
            }
        }
    }
}


