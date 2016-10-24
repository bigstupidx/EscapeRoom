using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Searchable : Focusable
{
	public string message;

	public Text overlayText;

	public Searchable()
	{
		message = "Nothing Here.";
	}

	public override void OnPointerClick(UnityEngine.EventSystems.PointerEventData eventData)
	{
		base.OnPointerClick(eventData);

		// Display the message
		if (overlayText != null) {
			overlayText.text = message;
		}
	}
}
