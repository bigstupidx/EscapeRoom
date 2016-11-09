using UnityEngine;
using System.Collections;

public class GlobeRotator : Focusable {

	public GoalMover topHalf;
	public GoalMover bottomHalf;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override void OnPointerClick(UnityEngine.EventSystems.PointerEventData eventData)
	{
		base.OnPointerClick(eventData);

		// Rotate both half of the globe by 45 degrees
		Vector3 newRotation = topHalf.transform.rotation.eulerAngles;
		newRotation.y += 45;

		topHalf.AddGoal(topHalf.transform.position, Quaternion.Euler(newRotation));
		bottomHalf.AddGoal(bottomHalf.transform.position, Quaternion.Euler(newRotation));
	}
}
