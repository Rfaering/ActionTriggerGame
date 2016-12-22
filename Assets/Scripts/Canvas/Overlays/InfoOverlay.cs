using System;
using UnityEngine;
using UnityEngine.UI;

public class InfoOverlay : MonoBehaviour
{
    private OverlayManager _overlayManager;
    private System.Action _callback;

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

    public InfoOverlay SetButtonAction(System.Action callBack)
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
