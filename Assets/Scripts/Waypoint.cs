using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class Waypoint : Focusable, IPointerClickHandler
{
	public override void OnPointerClick(PointerEventData eventData)
    {
		Vector3 newPos = new Vector3(transform.position.x, Camera.main.transform.position.y, transform.position.z);

		// Check motion mode
		if (MenuManager.InstantCameraMotion) {
			// Instant
			Camera.main.transform.position = newPos;
		} else {
			// Gradual
			GoalMover mover = Camera.main.GetComponent<GoalMover>();
			mover.ClearGoals();
			mover.AddGoal(newPos, Camera.main.transform.rotation);
		}
    }
}
