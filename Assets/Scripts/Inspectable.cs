using UnityEngine;
using System.Collections;

[RequireComponent(typeof(GoalMover))]
public class Inspectable : Focusable {

	private Vector3 originalPosition;
	private Quaternion originalRotation;

	public float cameraDistance = 1.5f;

	public override void OnPointerClick(UnityEngine.EventSystems.PointerEventData eventData)
	{
		base.OnPointerClick(eventData);

		InspectionManager manager = FindObjectOfType<InspectionManager>();

		// If no object is being inspected, we can begin to inspect this one
		if (manager.objectBeingInspected == null) {
			PickUp();
		} else if (manager.objectBeingInspected == this) {
			PutDown();
		}
	}

	public void PickUp() {
		InspectionManager manager = FindObjectOfType<InspectionManager>();

		if (manager.objectBeingInspected == null) {
			// Set object being inspected
			manager.objectBeingInspected = this;

			// Remember current position and rotation of object
			originalPosition = gameObject.transform.position;
			originalRotation = gameObject.transform.rotation;

			// Remember the original "right" vector of the camera
			manager.cameraRight = Camera.main.transform.right;

			// Calculate a position in front of the camera to move the object to
			Vector3 cameraPos = Camera.main.transform.position;
			Vector3 cameraGoal = Util.GetPointBetweenPositionAndCamera(originalPosition, cameraDistance);

			// Move the (invisible) inspection menu canvas to the camera position
			manager.inspectionCanvas.transform.position = cameraGoal;

			// Rotate it to face the camera
			Util.RotateToFaceCamera(manager.inspectionCanvas.transform);

			// Move the object to camera goal position
			GoalMover mover = gameObject.GetComponent<GoalMover>();
			mover.ClearGoals();
			mover.AddGoal(cameraGoal, gameObject.transform.rotation);

			// Disable selection of object and its children while they are moving
			gameObject.layer = 2;

			foreach (Transform t in gameObject.GetComponentsInChildren<Transform>()) {
				t.gameObject.layer = 2;
			}

			// Disable selection of all other objects in the scene
			CustomizedGazeInputModule inputModule = FindObjectOfType<CustomizedGazeInputModule>();
			inputModule.SelectionMask.value &= ~1;
			inputModule.SelectionMask.value &= ~(1 << 8);

			mover.MovementComplete += Mover_PickUpMovementComplete;
		}
	}

	void Mover_PickUpMovementComplete ()
	{
		// enable selection of object and its children after they are done moving
		gameObject.layer = 3;

		foreach (Transform t in gameObject.GetComponentsInChildren<Transform>()) {
			t.gameObject.layer = 3;
		}

		// Make the inspection manager canvas visible
		InspectionManager manager = FindObjectOfType<InspectionManager>();

		// Show rotation buttons
		manager.inspectionCanvas.gameObject.SetActive(true);

		// Unregister handler
		GoalMover mover = gameObject.GetComponent<GoalMover>();
		mover.MovementComplete -= Mover_PickUpMovementComplete;
	}

	public void PutDown() {
		InspectionManager manager = FindObjectOfType<InspectionManager>();

		if (manager.objectBeingInspected == this) {
			// Clear object being inspected
			manager.objectBeingInspected = null;

			// Move item back to original position
			GoalMover mover = gameObject.GetComponent<GoalMover>();
			mover.ClearGoals();
			mover.AddGoal(originalPosition, originalRotation);

			// Remove rotation buttons
			manager.inspectionCanvas.gameObject.SetActive(false);

			mover.MovementComplete += Mover_PutDownMovementComplete;

			// Disable selection of object and its children while they are moving
			gameObject.layer = 2;

			foreach (Transform t in gameObject.GetComponentsInChildren<Transform>()) {
				t.gameObject.layer = 2;
			}

			// Re-enable selection of all other objects in the scene
			// Disable selection of all other objects in the scene
			CustomizedGazeInputModule inputModule = FindObjectOfType<CustomizedGazeInputModule>();
			inputModule.SelectionMask.value |= 1;
			inputModule.SelectionMask.value |= 1 << 8;
		}
	}

	void Mover_PutDownMovementComplete ()
	{
		// Make object and its children selectable again
		gameObject.layer = 0;

		foreach (Transform t in gameObject.GetComponentsInChildren<Transform>()) {
			t.gameObject.layer = 0;
		}

		// Unregister handler
		GoalMover mover = gameObject.GetComponent<GoalMover>();
		mover.MovementComplete -= Mover_PutDownMovementComplete;
	}
}
