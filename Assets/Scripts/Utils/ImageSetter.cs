using Assets.Scripts.Misc;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Utils
{
    public class ImageSetter : MonoBehaviour
    {
        public enum HoseTypes { Straight, Turn, Cross }
        public enum SpecialTypes { Water, Flower }
        public enum Angle { Down = 0, Right = 90, Up = 180, Left = 270 }

        public void SetHoseVisual(HoseTypes type, Angle angle)
        {
            var visualGameObject = transform.Find("Hose/" + Enum.GetName(typeof(HoseTypes), type)).gameObject;
            visualGameObject.transform.localRotation = Quaternion.Euler(new Vector3(90, ((float)angle), 0));
            visualGameObject.SetActive(true);
        }

        public void SetSpecialVisual(SpecialTypes type)
        {
            var visualGameObject = transform.Find("Special/" + Enum.GetName(typeof(SpecialTypes), type)).gameObject;
            visualGameObject.SetActive(true);
        }

        public void SetDisabledColor()
        {
            foreach (var item in Enum.GetValues(typeof(SpecialTypes)))
            {
                var visualGameObject = transform.Find("Special/" + Enum.GetName(typeof(SpecialTypes), item)).gameObject;
                if (visualGameObject != null)
                {
                    visualGameObject.GetComponent<Image>().CrossFadeColor(new Color(1.0f, 1.0f, 1.0f, 0.3f), 0.2f, true, true);
                }
            }
            foreach (var item in Enum.GetValues(typeof(HoseTypes)))
            {
                var visualGameObject = transform.Find("Hose/" + Enum.GetName(typeof(HoseTypes), item)).gameObject;

                if (visualGameObject != null)
                {
                    visualGameObject.GetComponent<Image>().CrossFadeColor(new Color(1.0f, 1.0f, 1.0f, 0.3f), 0.2f, true, true);
                }
            }
        }

        public void SetEnabledColor()
        {
            foreach (var item in Enum.GetValues(typeof(SpecialTypes)))
            {
                var visualGameObject = transform.Find("Special/" + Enum.GetName(typeof(SpecialTypes), item)).gameObject;
                if (visualGameObject != null)
                {
                    visualGameObject.GetComponent<Image>().CrossFadeColor(new Color(1.0f, 1.0f, 1.0f, 1.0f), 0.2f, true, true);
                }
            }
            foreach (var item in Enum.GetValues(typeof(HoseTypes)))
            {
                var visualGameObject = transform.Find("Hose/" + Enum.GetName(typeof(HoseTypes), item)).gameObject;
                if (visualGameObject != null)
                {
                    visualGameObject.GetComponent<Image>().CrossFadeColor(new Color(1.0f, 1.0f, 1.0f, 1.0f), 0.2f, true, true);
                }                
            }
        }

        public void ClearImages(BehaviorTypes behaviorType)
        {
            if (behaviorType == BehaviorTypes.Actions)
            {
                foreach (var item in Enum.GetValues(typeof(SpecialTypes)))
                {
                    var visualGameObject = transform.Find("Special/" + Enum.GetName(typeof(SpecialTypes), item)).gameObject;
                    visualGameObject.SetActive(false);
                }
            }
            if (behaviorType == BehaviorTypes.Triggers)
            {
                foreach (var item in Enum.GetValues(typeof(HoseTypes)))
                {
                    var visualGameObject = transform.Find("Hose/" + Enum.GetName(typeof(HoseTypes), item)).gameObject;
                    visualGameObject.SetActive(false);
                }
            }
        }
    }
}


