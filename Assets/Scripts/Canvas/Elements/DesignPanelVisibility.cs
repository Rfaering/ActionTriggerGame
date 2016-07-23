using UnityEngine;
using System.Collections;
using System;

public class DesignPanelVisibility : MonoBehaviour
{
    internal void SetVisibility(bool visibility)
    {
        gameObject.SetActive(visibility);
    }
}
