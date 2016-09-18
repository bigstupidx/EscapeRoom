using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public enum RecordingState
{
	Inactive,
	Recording,
	Playing}
;

public abstract class Recorder : MonoBehaviour
{
	private RecordingState _state = RecordingState.Inactive;

	public RecordingState State {
		get { return _state; }
		private set { _state = value; }
	}

	//Recording
	protected float recordingStartTime = 0;
	protected float lastKeyTime = 0;

	public virtual void StartRecording()
	{
		// Can only start recording from Inactive state
		if (State != RecordingState.Inactive) {
			throw new UnityException("State must be 'Inactive' to start a new recording.");
		}

		// Get current time
		float now = Time.realtimeSinceStartup;

		// Record start time of recording so that other times can be recorded relative to that
		recordingStartTime = now;

		// Set new state value
		State = RecordingState.Recording;
	}

	public virtual void StopRecording()
	{
		// Can only stop recording from Recording state
		if (State != RecordingState.Recording) {
			throw new UnityException("State must be 'Recording' to end a recording.");
		}

		// Set new state value
		State = RecordingState.Inactive;
	}

	// Playback
	protected float playbackStartTime = 0;

	public virtual void StartPlayback()
	{
		// Can only start recording from Inactive state
		if (State != RecordingState.Inactive) {
			throw new UnityException("State must be 'Inactive' to start playback.");
		}

		// Record time playback starts so that we can calculate a relative time to the animation curves
		playbackStartTime = Time.realtimeSinceStartup;

		// Set new state value
		State = RecordingState.Playing;
	}

	public virtual void StopPlayback()
	{
		// Can only start recording from Inactive state
		if (State != RecordingState.Playing) {
			throw new UnityException("State must be 'Playing' to stop playback.");
		}

		// Set new state value
		State = RecordingState.Inactive;
	}

	// Returns time relative to start of playbacak or recording, depending on the state that the recorder is in.
	// If recorder is inactive, then 0 is returned.
	public float GetRelativeTime()
	{
		switch (State) {
		case RecordingState.Recording:
			return Time.realtimeSinceStartup - recordingStartTime;
		case RecordingState.Playing:
			return Time.realtimeSinceStartup - playbackStartTime;
		default:
			return 0.0f;
		}
	}
}



