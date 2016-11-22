using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class RotationRecorder : MotionRecorder
{
	// Animation curves to store recordings in
	private Vector4Curve rotations = new Vector4Curve();

	protected override void RecordKeyFrame(float time)
	{
		// Record orientation
		Quaternion localRotation = this.transform.localRotation;

		rotations.X.AddKey(time, localRotation.x);
		rotations.Y.AddKey(time, localRotation.y);
		rotations.Z.AddKey(time, localRotation.z);
		rotations.W.AddKey(time, localRotation.w);

		lastKeyTime = time;
	}

	protected override void SampleCurves(float time)
	{
		// Sample rotation from curves
		Quaternion newLocalRotation = new Quaternion();

		newLocalRotation.x = rotations.X.Evaluate(time);
		newLocalRotation.y = rotations.Y.Evaluate(time);
		newLocalRotation.z = rotations.Z.Evaluate(time);
		newLocalRotation.w = rotations.W.Evaluate(time);

		this.transform.localRotation = newLocalRotation;
	}

	public override List<Recording.Timeline> GetTimelines()
	{
		List<Recording.Timeline> result = new List<Recording.Timeline>();
		Recording.RotationTimeline rotTimeline = new Recording.RotationTimeline();

		rotTimeline.ObjectName = this.name;

		int count = rotations.X.keys.Length;

		if (rotations.Y.keys.Length != count || rotations.Z.keys.Length != count || rotations.W.keys.Length != count) {
			throw new UnityException("Unexpected mismatch in X, Y, Z, and W rotation key frames");
		}

		for (int i = 0; i < count; ++i) {
			Recording.RotationKey key = new Recording.RotationKey();
			key.Time = rotations.X.keys[i].time;
			key.X = rotations.X.keys[i].value;
			key.Y = rotations.Y.keys[i].value;
			key.Z = rotations.Z.keys[i].value;
			key.W = rotations.W.keys[i].value;
			rotTimeline.Rotations.Add(key);
		}

		result.Add(rotTimeline);

		return result;
	}

	public override void SetTimelines(List<Recording.Timeline> timelines)
	{
		Clear();

		foreach (Recording.Timeline t in timelines) {
			Recording.RotationTimeline rotTimeline = t as Recording.RotationTimeline;

			if (rotTimeline != null) {
				foreach (Recording.RotationKey key in rotTimeline.Rotations) {
					rotations.X.AddKey(key.Time, key.X);
					rotations.Y.AddKey(key.Time, key.Y);
					rotations.Z.AddKey(key.Time, key.Z);
					rotations.W.AddKey(key.Time, key.W);
				}
			}
		}
	}

	public override void Clear()
	{
		rotations.Clear();
	}

	public override float Length {
		get {
			float rotationLength = 0;

			if (rotations.X.length > 0) {
				rotationLength = rotations.X.keys[rotations.X.length - 1].time;
			}

			return rotationLength;
		}
	}
}

