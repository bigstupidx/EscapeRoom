using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class MotionRecorder : MonoBehaviour
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

	private RecordingState _state = RecordingState.Inactive;

	public RecordingState State {
		get { return _state; }
		private set { _state = value; }
	}

	//Recording
	private float recordingStartTime = 0;
	private float lastKeyTime = 0;

	public void StartRecording()
	{
		// Can only start recording from Inactive state
		if (State != RecordingState.Inactive) {
			throw new UnityException("State must be 'Inactive' to start a new recording.");
		}

		// Get current time
		float now = Time.realtimeSinceStartup;

		// Record start time of recording so that other times can be recorded relative to that
		recordingStartTime = now;

		// Clear any previous recordings
		rotations.Clear();
		translations.Clear();

		// Record a key frame at the start of recording to ensure that original position is saved
		RecordKeyFrame(now);

		// Set new state value
		State = RecordingState.Recording;
	}

	public void StopRecording()
	{
		// Can only stop recording from Recording state
		if (State != RecordingState.Recording) {
			throw new UnityException("State must be 'Recording' to end a recording.");
		}

		// Get current time
		float now = Time.realtimeSinceStartup;

		// Record a key frame at the end of the recording to ensure that final position is saved, regardless of sample rate.
		RecordKeyFrame(now);

		// Set new state value
		State = RecordingState.Inactive;
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

	// Playback
	private float playbackStartTime = 0;

	public void StartPlayback()
	{
		// Can only start recording from Inactive state
		if (State != RecordingState.Inactive) {
			throw new UnityException("State must be 'Inactive' to start playback.");
		}

		// Record time playback starts so that we can calculate a relative time to the animation curves
		playbackStartTime = Time.realtimeSinceStartup;

		// Sample initial position
		SampleCurves(Time.realtimeSinceStartup);

		// Set new state value
		State = RecordingState.Playing;
	}

	public void StopPlayback()
	{
		// Can only start recording from Inactive state
		if (State != RecordingState.Playing) {
			throw new UnityException("State must be 'Playing' to stop playback.");
		}

		// Sample final position
		SampleCurves(Time.realtimeSinceStartup);

		// Set new state value
		State = RecordingState.Inactive;
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

