using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class KeyPadButton : Searchable
{
	public string buttonPressed;
	public GameObject KeyPad;


	public override void OnPointerClick(UnityEngine.EventSystems.PointerEventData eventData)
	{
		if (buttonPressed.Equals("del")) {
			UnlockSafe.codeEntry = "";
			message = "";
		}
		else if (buttonPressed.Equals("X")) {
			UnlockSafe.codeEntry = "";
			message = "";
			KeyPad.transform.position = UnlockSafe.hiddenPosition;
		} else {
			UnlockSafe.codeEntry += buttonPressed;
			message = UnlockSafe.codeEntry;
		}
		DisplayMessage(message);
	}
}
