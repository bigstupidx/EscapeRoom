using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class Axe : Pickup {
    // Use this for initialization

    public override void OnPointerClick(PointerEventData eventData)
    {
        if (!IsFound)
        {
            base.OnPointerClick(eventData);
        }
    }

    public bool checkClick()
    {
        return IsFound;
    }
}
