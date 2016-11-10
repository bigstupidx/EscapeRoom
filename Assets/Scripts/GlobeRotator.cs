using UnityEngine;
using System.Collections;

public class GlobeRotator : Openable {

	public GoalMover topHalf;
	public GoalMover bottomHalf;

	public override void OnPointerEnter(UnityEngine.EventSystems.PointerEventData eventData)
	{
		// Disable if globe is opened
		if (IsOpen) {
			this.gameObject.layer = 2;
		}

		base.OnPointerEnter(eventData);
	}

	public override void OnPointerClick(UnityEngine.EventSystems.PointerEventData eventData)
	{
		// Don't call base class implementation which would open the globe when clicked

		// Rotate both half of the globe by 45 degrees
		Vector3 newRotation = topHalf.transform.rotation.eulerAngles;
		newRotation.y += 45;

		topHalf.ClearGoals();
		bottomHalf.ClearGoals();
		topHalf.AddGoal(topHalf.transform.position, Quaternion.Euler(newRotation));
		bottomHalf.AddGoal(bottomHalf.transform.position, Quaternion.Euler(newRotation));
	}
}
