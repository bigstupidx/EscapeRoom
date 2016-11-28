using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class KeyPadButton : Searchable
{
	public string buttonPressed;
	public UnlockSafe KeyPad;


	public override void OnPointerClick(UnityEngine.EventSystems.PointerEventData eventData)
	{
		if (buttonPressed.Equals("del")) {
			UnlockSafe.codeEntry = "";
			message = "";
			KeyPad.Hide();
		} else {
			UnlockSafe.codeEntry += buttonPressed;
			message = UnlockSafe.codeEntry;
		}
		DisplayMessage(message);
	}
}
