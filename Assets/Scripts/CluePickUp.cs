using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class CluePickUp : MonoBehaviour, IPointerClickHandler
{
    private Vector3 startingPosition;

    /*void Update()
    {
        if (GameVariables.drawerIsOpen == true)
        {
            gameObject.SetActive(true);
        } else if (GameVariables.drawerIsOpen == false)
        {
            gameObject.SetActive(false);
        }

    }*/

    public void OnPointerClick(PointerEventData eventData)
    {
        GameVariables.keyCount++;
        GameVariables.keyDisplayTime = 2;
        gameObject.SetActive(false);
    }

}
