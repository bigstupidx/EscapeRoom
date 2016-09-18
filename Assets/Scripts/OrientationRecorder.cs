using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class OrientationRecorder : MonoBehaviour
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
	private Vector4Curve rotations = new Vector4Curve();

	//Recording
	private bool isRecording = false;
	private float recordingStartTime = 0;
	private float lastKeyTime = 0;

	// Playback
	private bool isPlaying = false;
	private float playbackStartTime = 0;

	// Sample rate of 1/20 of a second
	private const float SampleRate = 1.0f / 20.0f;

	// Use this for initialization
	void Start()
	{
	}

	// Update is called once per frame
	void Update()
	{
		float now = Time.realtimeSinceStartup;

		if (isPlaying == false && Input.GetButtonUp("ToggleRecording")) {
			ToggleRecording();
		} else if (isRecording == false && Input.GetButtonUp("TogglePlayback")) {
			TogglePlayback();
		} else {
			if (isPlaying) {
				SampleCurves(now);
			} else if (isRecording && now - lastKeyTime > SampleRate) {
				RecordKeyFrame(now);
			}
		}
	}


	public bool IsRecording {
		get { return isRecording; }
		set
		{
			// Do nothing if value has not changed
			if (value == isRecording) {
				return; 
			}

			// Set new value of isRecording
			isRecording = value;

			float now = Time.realtimeSinceStartup;

			if (isRecording) {
				// Recording is starting
				// Record start time of recording so that other times can be recorded relative to that
				recordingStartTime = now;

				// Clear any previous recordings
				rotations.Clear();
			}

			// Record a key frame at the start and end of each recording, regardless of sample rate
			RecordKeyFrame(now);
		}
	}

	private void ToggleRecording()
	{
		IsRecording = !IsRecording;
	}

	private void RecordKeyFrame(float realTime)
	{
		// Save times relative to the begining of the recording
		float time = realTime - recordingStartTime;

		// Record orientation
		Quaternion localRotation = this.transform.localRotation;

		rotations.X.AddKey(time, localRotation.x);
		rotations.Y.AddKey(time, localRotation.y);
		rotations.Z.AddKey(time, localRotation.z);
		rotations.W.AddKey(time, localRotation.w);

		lastKeyTime = time;
	}

	private void TogglePlayback()
	{
		isPlaying = !isPlaying;

		if (isPlaying) {
			// Record time playback starts so that we can calculate a relative time to the animation curves
			playbackStartTime = Time.realtimeSinceStartup;

			// Disable controls
			FirstPersonController fpc = this.GetComponent(typeof(FirstPersonController)) as FirstPersonController;
			if (fpc != null) {
				fpc.enabled = false;
			}
		} else {
			// Reenable controls
			FirstPersonController fpc = this.GetComponent(typeof(FirstPersonController)) as FirstPersonController;
			if (fpc != null) {
				fpc.enabled = true;
			}
		}

		// Sample at begining and end of playback
		SampleCurves(Time.realtimeSinceStartup);
	}

	private void SampleCurves(float realTime)
	{
		// Get the time relative to the start of the animation playback
		float time = realTime - playbackStartTime;

		// Sample rotation from curves
		Quaternion newLocalRotation = new Quaternion();

		newLocalRotation.x = rotations.X.Evaluate(time);
		newLocalRotation.y = rotations.Y.Evaluate(time);
		newLocalRotation.z = rotations.Z.Evaluate(time);
		newLocalRotation.w = rotations.W.Evaluate(time);

		this.transform.localRotation = newLocalRotation;
	}
}
