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
			//clone.transform.LookAt(faceCamera.transform, new Vector3(0, 1, 0));
			//clone.transform.Rotate(new Vector3(0, 180, 0));
			clone.transform.LookAt(position + faceCamera.transform.rotation * Vector3.forward, faceCamera.transform.rotation * Vector3.up);
		}
	}
}
