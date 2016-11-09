using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public delegate void MovementCompleteandler();

public delegate void GoalReachedHandler(Vector3 position, Quaternion rotation, string tag);

public class GoalMover : MonoBehaviour
{
	public float maxSpeed = 3.0f;

	public float maxAcceleration = 1.0f;

	public float slowRadius = 3.0f;

	public float targetRadius = 0.1f;

	public Vector3 velocity = new Vector3(0, 0, 0);

	public Vector3 acceleration = new Vector3(0,0,0);

	public float rotationSpeed = 0.0f;

	public float angularAcceleration = 0.0f;

	public float maxAngularAcceleration = 50.0f;

	public float maxRotationSpeed = 180.0f;

	public float slowAngle = 50.0f;

	public event GoalReachedHandler GoalReached;

	public event MovementCompleteandler MovementComplete;

	private class Goal {
		public Vector3 position;
		public Quaternion rotation;
		public string tag;
	}

	private Queue<Goal> goals = new Queue<Goal>();

	public void AddGoal(Vector3 position, Quaternion rotation, string tag = null) {
		Goal goal = new Goal();
		goal.position = position;
		goal.rotation = rotation;
		goal.tag = tag;
		goals.Enqueue(goal);
	}
	
	// Update is called once per frame
	void Update()
	{
		// Get current target
		if (goals.Count == 0) {
			// No goals, do nothing
			return;
		}

		Goal target = goals.Peek();

		bool arrived = Arrive(target);
		bool alligned = Align(target);

		if (arrived && alligned) {
			// Move on to the next target
			goals.Dequeue();

			// Notify any listeners that a goal was reached
			if (GoalReached != null) {
				GoalReached(target.position, target.rotation, target.tag);
			}

			// If there are no goals left, notify any listerns that the final goal was reached
			if (goals.Count == 0 && MovementComplete != null) {
				MovementComplete();
			}
		}
	}

	private bool Arrive(Goal target) {
		// Get direction to the target
		Vector3 direction = target.position - transform.position;
		float distance = direction.magnitude;

		// Apply orientation matching

		// Check if we have arrived
		if (distance < targetRadius) {
			// Clear out acceleration and velocity
			velocity = new Vector3(0, 0, 0);
			acceleration = new Vector3(0, 0, 0);
			return true;
		}

		// If we are outside the slow radius then go max speed, otherwise calculate scaled speed
		float targetSpeed = maxSpeed;
		if (distance <= slowRadius) {
			targetSpeed = maxSpeed * distance / slowRadius;
		}

		// The target velocity combines speed and direction
		Vector3 targetVelocity = direction;
		targetVelocity.Normalize();
		targetVelocity *= targetSpeed;

		// Accelration tries to get to the target velocity
		acceleration = targetVelocity - velocity;

		// Check if the acceleration is too fast
		if (acceleration.magnitude > maxAcceleration) {
			acceleration.Normalize();
			acceleration *= maxAcceleration;
		}

		// Apply final acceleration
		velocity += acceleration * Time.deltaTime;

		// Make sure velocity isn't too fast
		if (velocity.magnitude > maxSpeed) {
			velocity.Normalize();
			velocity *= maxSpeed;
		}

		// Apply velocity to position
		transform.position += velocity * Time.deltaTime;

		return false;
	}

	private bool Align(Goal target) {
		
		// Get the naive direction to the taget
		float rotationSize = Quaternion.Angle(target.rotation, transform.rotation);

		if (rotationSize < targetRadius) {
			// Successfully aligned
			angularAcceleration = 0;
			rotationSpeed = 0;
			return true;
		}

		// If we are outside the slowAngle, then use maximum rotation
		float targetAngle;
		if (rotationSize > slowAngle) {
			targetAngle = maxAngularAcceleration;
		} else {
			targetAngle = maxRotationSpeed * rotationSize / slowAngle;
		}

		// Accelration tries to get to the target rotation
		angularAcceleration = targetAngle - rotationSpeed;

		// Check if the acceleration is too fast
		if (angularAcceleration > maxAngularAcceleration) {
			angularAcceleration =  maxAngularAcceleration;
		}

		// Apply final acceleration
		rotationSpeed += angularAcceleration * Time.deltaTime;

		// Make sure rotation speed isn't too fast
		if (rotationSpeed > maxRotationSpeed) {
			rotationSpeed = maxRotationSpeed;
		}

		// Finally apply the rotation speed to the rotation quaternion
		transform.rotation = Quaternion.RotateTowards(transform.rotation, target.rotation, rotationSpeed * Time.deltaTime);

		return false;
	}

	public void ClearGoals() {
		goals.Clear();
	}

	public bool IsMoving {
		get {
			return goals.Count != 0;
		}
	}
}