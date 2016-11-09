using UnityEngine;
using System.Collections;

public delegate void ToggleEvent();

public class Openable : Focusable
{
	public GoalMover openMover;

	public Transform openTransform;

	public event ToggleEvent Opened;

	public event ToggleEvent Closed;

	private bool _isOpen = false;

	private Vector3 closedPosition;
	private Quaternion closedRotation;
	private int originalLayer;

	public bool IsOpen {
		get { return _isOpen; }

		set {
			if (value == _isOpen) {
				// Value has not changed, do nothing
				return;
			}

			// Save new value
			_isOpen = value;

			// Clear any outstanding goals
			openMover.ClearGoals();

			// Subscribe to complection notification
			openMover.MovementComplete += OpenMover_MovementComplete;

			// Disable selection during animation
			gameObject.layer = 2;

			if (IsOpen) {
				// Move to open position
				openMover.AddGoal(openTransform.position, openTransform.rotation);

				if (Opened != null) {
					Opened();
				}
			} else {
				// Move to original (closed) position
				openMover.AddGoal(closedPosition, closedRotation);

				if (Closed != null) {
					Closed();
				}
			}
		}
	}

	void OpenMover_MovementComplete ()
	{
		// Re-enable selection
		gameObject.layer = originalLayer;

		// Unsubscribe
		openMover.MovementComplete -= OpenMover_MovementComplete;
	}

	// Use this for initialization
	public virtual void Start()
	{
		// Save the initial position and rotation so we can return to it if the object is closed again
		closedPosition = openMover.transform.position;
		closedRotation = openMover.transform.rotation;
		originalLayer = gameObject.layer;
	}
	
	// Update is called once per frame
	public virtual void Update()
	{
	
	}

	public override void OnPointerClick(UnityEngine.EventSystems.PointerEventData eventData)
	{
		// Toggle open state
		IsOpen = !IsOpen;
	}
}
