using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Searchable : Focusable
{
	public string message;

//	public GameObject notificationPrefab;
//
//	public Canvas notificationCanvas;

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

		// Display the message
//		if (notificationPrefab != null && notificationCanvas != null) {
//			GameObject clone = (GameObject)Instantiate(notificationPrefab);
//			Text text = clone.GetComponent<Text>();
//			text.text = message;
//
//			clone.transform.parent = notificationCanvas.transform;
//			clone.transform.position = gameObject.transform.position;
//			clone.transform.rotation = gameObject.transform.rotation;
//			clone.transform.LookAt(FindObjectOfType<Camera>().transform, new Vector3(0, 1, 0));
//			clone.transform.Rotate(new Vector3(0, 180, 0));
//		}
	}
}
