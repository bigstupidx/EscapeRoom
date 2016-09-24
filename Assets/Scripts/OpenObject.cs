using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class OpenObject : MonoBehaviour, IPointerClickHandler
{
    private Vector3 startingPosition;

    public void OnPointerClick(PointerEventData eventData)
    {
        
        if (GameVariables.keyCount >= 1 && !GameVariables.drawerIsOpen)
        {
            gameObject.GetComponent<Animation>().Play();
            GameVariables.drawerIsOpen = true;
        }

        
    }

}
