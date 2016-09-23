using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class CluePickUp : MonoBehaviour, IPointerClickHandler
{
    private Vector3 startingPosition;

    public void OnPointerClick(PointerEventData eventData)
    {
        GameVariables.keyCount++;
        GameVariables.keyDisplayTime = 2;
        gameObject.SetActive(false);
    }

}
