using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class Pickup : Searchable {
    public bool isFound;
    public Pickup() {
        message = "You found a";
    }
	// Use this for initialization
	void Start () {
        isFound = false;
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);
        isFound = true;
        this.gameObject.SetActive(false);
    }
}
