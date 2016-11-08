using UnityEngine;
using System.Collections;

[RequireComponent(typeof(GoalMover))]
public class GlobeRotator : Focusable {

	public GoalMover otherHalf;

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
		GoalMover thisHalf = GetComponent<GoalMover>();

		Vector3 newRotation = transform.rotation.eulerAngles;
		newRotation.y += 45;

		thisHalf.AddGoal(transform.position, Quaternion.Euler(newRotation));
		otherHalf.AddGoal(otherHalf.transform.position, Quaternion.Euler(newRotation));
	}
}
