using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

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
		// Clear any previous recordings
		rotations.Clear();
		translations.Clear();

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
}

