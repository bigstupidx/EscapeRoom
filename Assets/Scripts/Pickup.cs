using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

[RequireComponent(typeof(GoalMover))]
public class Pickup : Searchable
{
    
	public bool IsFound = false;

	public GameObject InventoryPosition = null;

	public Pickup()
	{
		message = "You found ";
	}

	public override void OnPointerClick(PointerEventData eventData)
	{
		base.OnPointerClick(eventData);

		// If already found, do nothing
		if (IsFound) {
			return;
		}

		IsFound = true;

		Vector3 start = gameObject.transform.position;

		Vector3 cameraPos = Camera.main.transform.position;

		Vector3 cameraGoal = Util.GetPointBetweenPositionAndCamera(start, 1.0f);			

		GoalMover mover = gameObject.GetComponent<GoalMover>();
		mover.ClearGoals();
		mover.AddGoal(cameraGoal, gameObject.transform.rotation);

		//Disable selection of all objects while this one is moving
		CustomizedGazeInputModule inputModule = FindObjectOfType<CustomizedGazeInputModule>();
		inputModule.DisableSelectionLayer(Layers.Default);

		mover.MovementComplete += Mover_MovementCompleteRenableSelection;

		if (InventoryPosition == null) {
			// No inventory position, just make the object dissappear after movement finishes
			mover.MovementComplete += Mover_MovementCompleteVanish;
		} else {
			// Change parent of object to inventory position
			gameObject.transform.parent = InventoryPosition.transform;

			// Move object to final position
			mover.AddGoal(InventoryPosition.transform.position, InventoryPosition.transform.rotation);
		}
	}

	void Mover_MovementCompleteRenableSelection ()
	{
		//Re-enable selection of objects
		CustomizedGazeInputModule inputModule = FindObjectOfType<CustomizedGazeInputModule>();
		inputModule.EnableSelectionLayer(Layers.Default);

		GoalMover mover = gameObject.GetComponent<GoalMover>();
		mover.MovementComplete -= Mover_MovementCompleteRenableSelection;
	}

	void Mover_MovementCompleteVanish ()
	{
		//Re-enable selection of objects
		CustomizedGazeInputModule inputModule = FindObjectOfType<CustomizedGazeInputModule>();
		inputModule.EnableSelectionLayer(Layers.Default);

		// Make the object disappear and then unregister this event
		gameObject.SetActive(false);

		GoalMover mover = gameObject.GetComponent<GoalMover>();
		mover.MovementComplete -= Mover_MovementCompleteVanish;
	}
}
