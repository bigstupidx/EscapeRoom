using System;
using UnityEngine;

public static class Util
{
	public static Vector3 GetPointBetweenPositionAndCamera(Vector3 lookAt, float cameraDistance = 1.5f)
	{
		Vector3 cameraPos = Camera.main.transform.position;
		Vector3 cameraGoal = lookAt - cameraPos;
		cameraGoal.Normalize();
		cameraGoal *= cameraDistance;
		cameraGoal += cameraPos;

		return cameraGoal;
	}

	public static void RotateToFaceCamera(Transform t)
	{
		Quaternion camRotation = Camera.main.transform.rotation;
		t.LookAt(t.position + camRotation * Vector3.forward, camRotation * Vector3.up);
	}

	public static void RotateAroundYToFaceCamera(Transform t)
	{
		t.LookAt(Camera.main.transform, Vector3.up);
		t.Rotate(0, 180, 0, Space.World);
	}
}

