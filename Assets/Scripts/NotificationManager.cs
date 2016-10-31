using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NotificationManager : MonoBehaviour {

	public GameObject notificationPrefab;

	public Canvas notificationCanvas;

	public Camera faceCamera;

	public void ShowNotification(string message, Vector3 position)
	{
		// Display the message
		if (notificationPrefab != null && notificationCanvas != null && faceCamera != null) {
			GameObject clone = (GameObject)Instantiate(notificationPrefab);
			Text text = clone.GetComponent<Text>();
			text.text = message;

			clone.transform.parent = notificationCanvas.transform;
			clone.transform.position = position;
			clone.transform.LookAt(position + faceCamera.transform.rotation * Vector3.forward, faceCamera.transform.rotation * Vector3.up);
		}
	}
}
