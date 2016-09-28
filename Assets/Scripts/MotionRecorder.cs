using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class MotionRecorder : Recorder
{
	// Structure to make it easier to refer to the 3 components of an animation curve
	private class Vector3Curve
	{
		public AnimationCurve X = new AnimationCurve();

		public AnimationCurve Y = new AnimationCurve();

		public AnimationCurve Z = new AnimationCurve();

		// Reset all animation curves to new empty ones
		public virtual void Clear()
		{
			X = new AnimationCurve();
			Y = new AnimationCurve();
			Z = new AnimationCurve();
		}
	}

	private class Vector4Curve : Vector3Curve
	{
		public AnimationCurve W = new AnimationCurve();

		public override void Clear()
		{
			base.Clear();
			W = new AnimationCurve();
		}
	}

	// Animation curves to store recordings in
	private Vector3Curve translations = new Vector3Curve();
	private Vector4Curve rotations = new Vector4Curve();

	// Sample rate of 1/20 of a second
	private float samplesPerSecond = 20.0f;

	public float SamplesPerSecond {
		get { return samplesPerSecond; }
		set { samplesPerSecond = value; }
	}

	// Use this for initialization
	void Start()
	{
	}

	// Update is called once per frame
	void Update()
	{
		float now = Time.realtimeSinceStartup;

		switch (State) {
		case RecordingState.Playing:
			SampleCurves(now);
			break;
		case RecordingState.Recording:
			if (now - lastKeyTime > 1.0f / SamplesPerSecond) {
				RecordKeyFrame(now);
			}
			break;
		}
	}

	public override void StartRecording()
	{
		base.StartRecording();

		// Record a key frame at the start of recording to ensure that original position is saved
		RecordKeyFrame(recordingStartTime);
	}

	public override void StopRecording()
	{
		base.StopRecording();

		// Get current time
		float now = Time.realtimeSinceStartup;
	}

	public override void StartPlayback()
	{
		base.StartPlayback();

		// Sample initial position
		SampleCurves(Time.realtimeSinceStartup);
	}

	public override void StopPlayback()
	{
		base.StopPlayback();

		// Sample final position
		SampleCurves(Time.realtimeSinceStartup);
	}

	private void RecordKeyFrame(float realTime)
	{
		// Save times relative to the begining of the recording
		float time = realTime - recordingStartTime;

		// Record position
		Vector3 localPosition = this.transform.localPosition;

		translations.X.AddKey(time, localPosition.x);
		translations.Y.AddKey(time, localPosition.y);
		translations.Z.AddKey(time, localPosition.z);

		// Record orientation
		Quaternion localRotation = this.transform.localRotation;

		rotations.X.AddKey(time, localRotation.x);
		rotations.Y.AddKey(time, localRotation.y);
		rotations.Z.AddKey(time, localRotation.z);
		rotations.W.AddKey(time, localRotation.w);

		lastKeyTime = time;
	}

	private void SampleCurves(float realTime)
	{
		// Get the time relative to the start of the animation playback
		float time = realTime - playbackStartTime;

		// Sample position from curves
		Vector3 newLocalPosition = new Vector3();

		newLocalPosition.x = translations.X.Evaluate(time);
		newLocalPosition.y = translations.Y.Evaluate(time);
		newLocalPosition.z = translations.Z.Evaluate(time);

		this.transform.localPosition = newLocalPosition;

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
		Recording.PositionTimeline posTimeline = new Recording.PositionTimeline();
		Recording.RotationTimeline rotTimeline = new Recording.RotationTimeline();

		posTimeline.ObjectName = this.name;
		rotTimeline.ObjectName = this.name;

		int count = translations.X.keys.Length;

		if (translations.Y.keys.Length != count || translations.Z.keys.Length != count) {
			throw new UnityException("Unexpected mismatch in X, Y, and Z position key frames");
		}

		for (int i = 0; i < count; ++i) {
			Recording.PositionKey key = new Recording.PositionKey();
			key.Time = translations.X.keys[i].time;
			key.X = translations.X.keys[i].value;
			key.Y = translations.Y.keys[i].value;
			key.Z = translations.Z.keys[i].value;
			posTimeline.Positions.Add(key);
		}
			
		result.Add(posTimeline);

		count = rotations.X.keys.Length;

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
			Recording.PositionTimeline posTimeline = t as Recording.PositionTimeline;

			if (posTimeline != null) {
				foreach (Recording.PositionKey key in posTimeline.Positions) {
					translations.X.AddKey(key.Time, key.X);
					translations.Y.AddKey(key.Time, key.Y);
					translations.Z.AddKey(key.Time, key.Z);
				}
			}

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
		translations.Clear();
	}

	public override float Length {
		get {
			float translationLength = 0;
			float rotationLength = 0;

			if (translations.X.length > 0) {
				translationLength = translations.X.keys[translations.X.length - 1].time;
			}

			if (rotations.X.length > 0) {
				rotationLength = rotations.X.keys[rotations.X.length - 1].time;
			}

			return Math.Max(rotationLength, translationLength);
		}
	}
}

