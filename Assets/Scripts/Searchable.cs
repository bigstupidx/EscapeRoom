using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Searchable : Focusable
{
	public string message;

	public Searchable()
	{
		message = "";
	}

	public override void OnPointerClick(UnityEngine.EventSystems.PointerEventData eventData)
    {
        message = "Nothing Here.";
        base.OnPointerClick(eventData);
		DisplayMessage(message);
	}
}
