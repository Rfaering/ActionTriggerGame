using Assets.Scripts.Canvas.Elements;
using Assets.Scripts.Utils;
using Assets.Scripts.World;
using UnityEngine;
using UnityEngine.UI;

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

        public void SetNumberOfLevels(int numbers)
        {
            int counter = 1;
            foreach (var image in transform.Find("Progress").GetComponentsInChildren<Image>())
            {
                if (counter == numbers)
                {
                    image.color = Color.white;
                    image.CrossFadeColor(Color.green, 1.5f, true, false);
                }
                else
                {
                    image.color = counter < numbers ? Color.green : Color.white;
                }

                counter++;
            }
        }

        public void SetImage(string image)
        {
            transform.Find("To").gameObject.SetActive(false);

            var fromImage = transform.Find("From").GetComponent<Image>();
            fromImage.sprite = Resources.Load<Sprite>(@"Background\" + image);
            fromImage.color = Color.white;
        }

        public void CrossFadeImage(string from, string to)
        {
            transform.Find("To").gameObject.SetActive(true);

            var fromImage = transform.Find("From").GetComponent<Image>();
            var toImage = transform.Find("To").GetComponent<Image>();

            fromImage.sprite = Resources.Load<Sprite>(@"Background\" + from);
            fromImage.color = Color.white;

            toImage.sprite = Resources.Load<Sprite>(@"Background\" + to);
            toImage.color = Color.white;

            fromImage.CrossFadeAlpha(0, 1.5f, true);
        }

        public void NextLevel()
        {
            _overlayManager.CloseActiveOverlay();
            _loadLevels.CurrentLevelName = LevelsInfo.GetNextLevel(_loadLevels.CurrentLevelName);
            _loadLevels.LoadCurrentLevel();
        }

        public void RestartLevel()
        {
            _loadLevels.LoadCurrentLevel();
            _overlayManager.CloseActiveOverlay();
        }
    }
}

