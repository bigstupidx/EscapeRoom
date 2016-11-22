using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum RecordingState
{
	Inactive,
	Recording,
	Playing,
	DonePlaying
};

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

	public virtual void StartRecording(float realStartTime)
	{
		// Clear any previous recordings
		Clear();

		// Can only start recording from Inactive state
		if (State != RecordingState.Inactive) {
			throw new UnityException("State must be 'Inactive' to start a new recording.");
		}

		// Record start time of recording so that other times can be recorded relative to that
		recordingStartTime = realStartTime;

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

	public virtual void StartPlayback(float realStartTime)
	{
		// Can only start recording from Inactive state
		if (State != RecordingState.Inactive) {
			throw new UnityException("State must be 'Inactive' to start playback.");
		}

		// Record time playback starts so that we can calculate a relative time to the animation curves
		playbackStartTime = realStartTime;

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
		float time = Time.realtimeSinceStartup;;

		switch (State) {
		case RecordingState.Recording:
			return time - recordingStartTime;
		case RecordingState.Playing:
			return time - playbackStartTime;
		default:
			return 0.0f;
		}
	}

	// This must be implemented by derived class to prepare a timeline ready to save to a file
	public abstract List<Recording.Timeline> GetTimelines();

	// This must be implemented by derived class to apply saved timeline
	public abstract void SetTimelines(List<Recording.Timeline> timelines);

	// This must be implemented by derived classes to clear any stored recordings
	public abstract void Clear();

	// This must be implemented by derived classes to return the length of the recording
	public abstract float Length { get; }
}



