﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(GoalMover))]
public class Inspectable : Focusable {

	private Vector3 originalPosition;
	private Quaternion originalRotation;

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
			Vector3 cameraGoal = Util.GetPointBetweenPositionAndCamera(originalPosition, 1.0f);

			// Move the object to camera goal position
			GoalMover mover = gameObject.GetComponent<GoalMover>();
			mover.ClearGoals();
			mover.AddGoal(cameraGoal, gameObject.transform.rotation);

			// Disable selection of object and its children while they are moving
			gameObject.layer = (int)Layers.IgnoreRaycast;

			foreach (Transform t in gameObject.GetComponentsInChildren<Transform>()) {
				t.gameObject.layer = (int)Layers.IgnoreRaycast;
			}

			// Disable selection of all other objects in the scene
			CustomizedGazeInputModule inputModule = FindObjectOfType<CustomizedGazeInputModule>();
			inputModule.DisableSelectionLayer(Layers.Default);

			mover.MovementComplete += Mover_PickUpMovementComplete;
		}
	}

	void Mover_PickUpMovementComplete ()
	{
		// enable selection of object and its children after they are done moving
		gameObject.layer = (int)Layers.InspectedObject;

		foreach (Transform t in gameObject.GetComponentsInChildren<Transform>()) {
			t.gameObject.layer = (int)Layers.InspectedObject;
		}

		// Show rotation buttons
		InspectionManager manager = FindObjectOfType<InspectionManager>();
		manager.ShowCanvas(gameObject.transform.position);

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
			manager.HideCanvas();

			mover.MovementComplete += Mover_PutDownMovementComplete;

			// Disable selection of object and its children while they are moving
			gameObject.layer = 2;

			foreach (Transform t in gameObject.GetComponentsInChildren<Transform>()) {
				t.gameObject.layer = 2;
			}

			// Re-enable selection of all other objects in the scene
			// Disable selection of all other objects in the scene
			CustomizedGazeInputModule inputModule = FindObjectOfType<CustomizedGazeInputModule>();
			inputModule.EnableSelectionLayer(Layers.Default);
		}
	}

	void Mover_PutDownMovementComplete ()
	{
		// Make object and its children selectable again
		gameObject.layer = (int)Layers.Default;

		foreach (Transform t in gameObject.GetComponentsInChildren<Transform>()) {
			t.gameObject.layer = (int)Layers.Default;
		}

		// Unregister handler
		GoalMover mover = gameObject.GetComponent<GoalMover>();
		mover.MovementComplete -= Mover_PutDownMovementComplete;
	}
}
