using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Searchable : Focusable
{
	public string message;

	public Searchable()
	{
		message = "Nothing Here.";
	}

	public override void OnPointerClick(UnityEngine.EventSystems.PointerEventData eventData)
	{
		base.OnPointerClick(eventData);

		DisplayMessage(message);
	}
}
