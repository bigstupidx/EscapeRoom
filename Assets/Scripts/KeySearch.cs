using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class KeySearch : Searchable{

    // Use this for initialization

    public GameObject key;
    KeySearch()
    {
        message = "You found the Key!";
    }
    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);

        key.transform.position = new Vector3(3.0009f, .659f, -.84f);
    }
}
