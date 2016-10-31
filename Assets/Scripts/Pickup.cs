using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class Pickup : Searchable {
    
	public bool IsFound = false;

	public GameObject InventoryPosition = null;

    public Pickup() {
        message = "You found ";
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);
		IsFound = true;

		if (InventoryPosition == null) {
			// No inventory position, just make the object dissappear
			this.gameObject.SetActive(false);
		} else {
			// Move the object to be a child of the inventory position object
			this.transform.parent = InventoryPosition.transform;
		}
    }
}
