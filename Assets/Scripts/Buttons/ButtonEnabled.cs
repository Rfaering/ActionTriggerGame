using Assets.Scripts.Misc;
using Assets.Scripts.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Buttons
{
    public class ButtonEnabled : MonoBehaviour
    {
        private bool _contentEnabled;

        public bool ContentEnabled
        {
            get { return _contentEnabled; }
            set
            {
                _contentEnabled = value;
                _updateNextFrame = true;
            }
        }

        private bool _updateNextFrame = false;

        private BuildMode _buildMode;
        private Runner _runner;
        private ImageSetter _imageSetter;

        public void Start()
        {
            _updateNextFrame = true;
            _buildMode = FindObjectOfType<BuildMode>();
            _imageSetter = GetComponent<ImageSetter>();
            _runner = FindObjectOfType<Runner>();
        }

        public void Update()
        {
            if (_buildMode.RuntimeMode == BuilderMode.Running)
            {
                GetComponent<Button>().interactable = _contentEnabled && !_runner.IsRunning();
            }
            else
            {
                GetComponent<Button>().interactable = !_runner.IsRunning();
            }

            if (_updateNextFrame)
            {
                if (ContentEnabled)
                {
                    _imageSetter.SetEnabledColor();
                }
                else
                {
                    _imageSetter.SetDisabledColor();
                }
            }
        }
    }
}
