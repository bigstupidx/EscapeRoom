using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class GlobeCountry : Focusable {

	public GlobeRotator globeScript;

	private static string[] clickHistory = new string[3];

	public override void OnPointerClick(UnityEngine.EventSystems.PointerEventData eventData)
	{
		// If the globe is already opened, do nothing
		if (globeScript.IsOpen) {
			// Disable it so it can't be highlighted
			this.gameObject.layer = 2;

			return;
		}

		// Shift the first two values in the history
		clickHistory[0] = clickHistory[1];
		clickHistory[1] = clickHistory[2];

		// Add the currently clicked country to the history
		clickHistory[2] = this.name;

		// Check victory condition - countries were clicked in the correct order
		if (clickHistory[0] == "Algeria" && clickHistory[1] == "India" && clickHistory[2] == "Greenland") {
			// Open the globe
			DisplayMessage(String.Format("As you press on {0}, you hear a click.", this.name));

			globeScript.IsOpen = true;
		} else {
			DisplayMessage(String.Format("You press on {0}.", this.name));
		}
	}

	public override void OnPointerEnter(UnityEngine.EventSystems.PointerEventData eventData)
	{
		// Disable if globe is opened
		if (globeScript.IsOpen) {
			this.gameObject.layer = 2;
		}

		base.OnPointerEnter(eventData);
	}
}
