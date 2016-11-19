using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NotificationManager : MonoBehaviour {

	public GameObject notificationPrefab;

	public Canvas notificationCanvas;

	public void ShowNotification(string message, Bounds objectBounds) {
		// Automatically select the position as the closest point on the bounds to the camera
		ShowNotification(message, objectBounds.center);
	}

	public void ShowNotification(string message, Vector3 position)
	{
		// Display the message
		if (notificationPrefab != null && notificationCanvas != null && Camera.main != null) {
			GameObject clone = (GameObject)Instantiate(notificationPrefab);
			Text text = clone.GetComponent<Text>();
			text.text = message;

			// Place the text 1 meter from the camera in the direction of the object being clicked
			Vector3 cameraPos = Camera.main.transform.position;

			Vector3 cameraGoal = Util.GetPointBetweenPositionAndCamera(position);

			// Set properties of clone
			clone.transform.parent = notificationCanvas.transform;
			Util.RotateToFaceCamera(clone.transform);
			clone.transform.position = cameraGoal;
			Util.RotateToFaceCamera(clone.transform);
		}
	}
}
