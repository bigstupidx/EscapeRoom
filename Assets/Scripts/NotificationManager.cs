using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NotificationManager : MonoBehaviour {

	public GameObject notificationPrefab;

	public Canvas notificationCanvas;

	public Camera faceCamera;

	public void ShowNotification(string message, Bounds objectBounds) {
		// Automatically select the position as the closest point on the bounds to the camera
		ShowNotification(message, objectBounds.ClosestPoint(faceCamera.transform.position));
	}

	public void ShowNotification(string message, Vector3 position)
	{
		// Display the message
		if (notificationPrefab != null && notificationCanvas != null && faceCamera != null) {
			GameObject clone = (GameObject)Instantiate(notificationPrefab);
			Text text = clone.GetComponent<Text>();
			text.text = message;

			// Scale the text based on distance from the camera
			float dist = Vector3.Distance(faceCamera.transform.position, position);

			// Scale the text based on distance to the camera
			float scale = dist / 40.0f;

			// Set properties of clone
			clone.transform.parent = notificationCanvas.transform;
			clone.transform.localScale *= scale;
			clone.transform.position = position;
			clone.transform.LookAt(position + faceCamera.transform.rotation * Vector3.forward, faceCamera.transform.rotation * Vector3.up);
		}
	}
}
