using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class BeginGameButton : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        FindObjectOfType<BeginLevel>().BeginGame();
    }
}
