﻿using UnityEngine;
using UnityEngine.UI;

public class ProgressButton : MonoBehaviour
{
    public void SetProgress(bool inProgress)
    {
        this.GetComponent<Image>().color = !inProgress ? new Color(0.5f, 1.0f, 0.5f) : new Color(1.0f, 0.5f, 0.5f);
        this.transform.Find("Icon").gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Icons/" + (!inProgress ? "Play" : "Stop"));
    }
}
