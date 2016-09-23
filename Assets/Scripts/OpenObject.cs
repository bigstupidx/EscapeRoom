using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class OpenObject : MonoBehaviour, IPointerClickHandler
{
    private Vector3 startingPosition;

    public void OnPointerClick(PointerEventData eventData)
    {
        
        gameObject.SetActive(false);
    }

}
