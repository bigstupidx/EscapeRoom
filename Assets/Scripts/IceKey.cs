using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class IceKey : Pickup
{

	public GameObject wax;

	public override void OnPointerClick(PointerEventData eventData)
	{
		if (wax.activeSelf) {
			DisplayMessage("The key appears to be stuck!");
		} else {
			base.OnPointerClick(eventData);
		}
	}
}
