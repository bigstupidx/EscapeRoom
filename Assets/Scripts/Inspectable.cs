using UnityEngine;
using System.Collections;

[RequireComponent(typeof(GoalMover))]
public class Inspectable : Focusable {

	private Vector3 originalPosition;
	private Quaternion originalRotation;

	public float cameraDistance = 1.0f;

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
			Vector3 cameraGoal = originalPosition - cameraPos;
			cameraGoal.Normalize();
			cameraGoal *= cameraDistance;
			cameraGoal += cameraPos;

			// Move the (invisible) inspection menu canvas to the camera position
			manager.inspectionCanvas.transform.position = cameraGoal;

			// Rotate it to face the camera
			manager.inspectionCanvas.transform.LookAt(cameraGoal + Camera.main.transform.rotation * Vector3.forward, Camera.main.transform.rotation * Vector3.up);

			// Move the object to camera goal position
			GoalMover mover = gameObject.GetComponent<GoalMover>();
			mover.ClearGoals();
			mover.AddGoal(cameraGoal, gameObject.transform.rotation);

			// Disable selection of all other objects in the scene
			CustomizedGazeInputModule inputModule = FindObjectOfType<CustomizedGazeInputModule>();
			inputModule.SelectionMask.value &= ~1;
			inputModule.SelectionMask.value &= ~(1 << 8);

			mover.MovementComplete += Mover_PickUpMovementComplete;
		}
	}

	void Mover_PickUpMovementComplete ()
	{
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

			// Re-enable selection of all other objects in the scene
			// Disable selection of all other objects in the scene
			CustomizedGazeInputModule inputModule = FindObjectOfType<CustomizedGazeInputModule>();
			inputModule.SelectionMask.value |= 1;
			inputModule.SelectionMask.value |= 1 << 8;
		}
	}

	void Mover_PutDownMovementComplete ()
	{
		// Unregister handler
		GoalMover mover = gameObject.GetComponent<GoalMover>();
		mover.MovementComplete -= Mover_PutDownMovementComplete;
	}
}
