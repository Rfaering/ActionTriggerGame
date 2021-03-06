﻿using System;
using UnityEngine;
using UnityEngine.UI;

public class ImageSetter : MonoBehaviour
{
    public enum HoseTypes { Straight, Turn, Cross, Bridge, Center }
    public enum SpecialTypes { Water, Flower, FlowerBlue, Timer, Lock, Key, BlackHole, Bacteria }
    public enum Angle { Down = 0, Right = 90, Up = 180, Left = 270 }

    private Color disabledColor = new Color(0.5f, 0.5f, 0.5f, 1.0f);
    private Color enabledColor = new Color(1.0f, 1.0f, 1.0f, 1.0f);

    private Color previewColor = new Color(1.0f, 1.0f, 1.0f, 0.3f);

    public void SetHoseVisual(HoseTypes type, Angle angle, bool preview)
    {
        var visualGameObject = transform.Find("Hose/" + Enum.GetName(typeof(HoseTypes), type)).gameObject;
        visualGameObject.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, ((float)angle)));
        var spriteRenderers = visualGameObject.GetComponentsInChildren<Image>();
        if (preview)
        {
            foreach (var item in spriteRenderers)
            {
                item.color = previewColor;
            }
        }
        else
        {
            foreach (var item in spriteRenderers)
            {
                item.color = enabledColor;
            }
        }

        visualGameObject.SetActive(true);
    }

    public GameObject GetVisualOf(SpecialTypes type)
    {
        return transform.Find("Special/" + Enum.GetName(typeof(SpecialTypes), type)).gameObject;
    }

    public void SetSpecialVisual(SpecialTypes type, bool preview)
    {
        var visualGameObject = transform.Find("Special/" + Enum.GetName(typeof(SpecialTypes), type)).gameObject;

        var spriteRenderer = visualGameObject.GetComponent<Image>();
        if (preview)
        {
            if (spriteRenderer != null)
            {
                spriteRenderer.color = previewColor;
            }
        }
        else
        {
            if (spriteRenderer != null)
            {
                spriteRenderer.color = enabledColor;
            }
        }

        visualGameObject.SetActive(true);
    }

    public void RemoveSpecialVisual(SpecialTypes type)
    {
        var visualGameObject = transform.Find("Special/" + Enum.GetName(typeof(SpecialTypes), type)).gameObject;
        visualGameObject.SetActive(false);
    }

    public void ActivateVisual(string path)
    {
        var visualGameObject = transform.Find(path);
        if (visualGameObject == null)
        {
            return;
        }
        visualGameObject.gameObject.SetActive(true);
    }

    public void DisableVisual(string path)
    {
        var visualGameObject = transform.Find(path);
        if (visualGameObject == null)
        {
            return;
        }
        visualGameObject.gameObject.SetActive(false);
    }

    public void SetDisabledColor()
    {
        foreach (var item in Enum.GetValues(typeof(SpecialTypes)))
        {
            var visualGameObject = transform.Find("Special/" + Enum.GetName(typeof(SpecialTypes), item)).gameObject;
            if (visualGameObject != null)
            {
                foreach (var image in visualGameObject.GetComponentsInChildren<Image>())
                {
                    image.CrossFadeColor(disabledColor, 0.2f, true, true);
                }
            }
        }
        foreach (var item in Enum.GetValues(typeof(HoseTypes)))
        {
            var visualGameObject = transform.Find("Hose/" + Enum.GetName(typeof(HoseTypes), item)).gameObject;

            if (visualGameObject != null)
            {
                foreach (var image in visualGameObject.GetComponentsInChildren<Image>())
                {
                    image.CrossFadeColor(disabledColor, 0.2f, true, true);
                }
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
                foreach (var image in visualGameObject.GetComponentsInChildren<Image>())
                {
                    image.CrossFadeColor(enabledColor, 0.2f, true, true);
                }
            }
        }
        foreach (var item in Enum.GetValues(typeof(HoseTypes)))
        {
            var visualGameObject = transform.Find("Hose/" + Enum.GetName(typeof(HoseTypes), item)).gameObject;
            if (visualGameObject != null)
            {
                foreach (var image in visualGameObject.GetComponentsInChildren<Image>())
                {
                    image.CrossFadeColor(enabledColor, 0.2f, true, true);
                }
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
