using Assets.Scripts.Misc;
using Assets.Scripts.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Buttons
{
    public class ButtonEnabled : MonoBehaviour
    {
        public bool ContentEnabled;
        private bool _contentEnabled;

        private BuildMode _buildMode;
        private ImageSetter _imageSetter;

        public void Start()
        {            
            _contentEnabled = true;
            _buildMode = FindObjectOfType<BuildMode>();
            _imageSetter = GetComponent<ImageSetter>();
        }

        public void Update()
        {
            if (_buildMode.RuntimeMode == BuilderMode.Running)
            {
                GetComponent<Button>().interactable = _contentEnabled;
            }
            else
            {
                GetComponent<Button>().interactable = true;
            }
            if (ContentEnabled != _contentEnabled)
            {
                _contentEnabled = ContentEnabled;

                if (_contentEnabled)
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
