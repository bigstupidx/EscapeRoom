using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Searchable : Focusable
{
	public string message;

	public float Scale = 1.0f;

	public Searchable()
	{
		message = "Nothing Here.";
	}

	public override void OnPointerClick(UnityEngine.EventSystems.PointerEventData eventData)
	{
		base.OnPointerClick(eventData);

		// Display the message using the Notification Manager
		NotificationManager manager = FindObjectOfType<NotificationManager>();

		if (manager != null) {
			manager.ShowNotification(message, gameObject.transform.position);
		}

	}
}
