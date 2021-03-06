﻿using UnityEngine;
using UnityEngine.UI;

public class SaveOverlay : MonoBehaviour
{
    public string GetInputField()
    {
        return transform.Find("LevelName").GetComponent<InputField>().text;
    }

    public void SetInputField()
    {
        transform.Find("LevelName").GetComponent<InputField>().text = FindObjectOfType<SaveLevel>().SaveLevelName;
    }

    public void Save()
    {
        var saveLevel = FindObjectOfType<SaveLevel>();
        saveLevel.SaveLevelName = GetInputField();
        saveLevel.SaveCurrentLevel();
        GetComponentInParent<OverlayManager>().CloseActiveOverlay();
        FindObjectOfType<CanvasMenu>().GetComponentInChildren<LevelsDropdown>().PopulateOptions();
    }
}
