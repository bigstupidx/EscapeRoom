using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RecordingManager : MonoBehaviour
{
	// Use this for initialization
	void Start()
	{
		StartRecording();
	}
	
	// Update is called once per frame
	void Update()
	{
		foreach (GazeInputModule gim in modulesToDeactivate) {
			gim.DeactivateModule();
			gim.enabled = false;
		}

		modulesToDeactivate.Clear();
	}

	private static RecordingState _state = RecordingState.Inactive;

	public static RecordingState State {
		get { return _state; }
		private set { _state = value; }
	}

	public static void StartRecording()
	{
		// Can only start recording from Inactive state
		if (State != RecordingState.Inactive) {
			throw new UnityException("State must be 'Inactive' to start a new recording.");
		}

		// Find all Recorder objects in the scene and tell them to start recording
		foreach (object obj in Object.FindObjectsOfType(typeof(Recorder))) {
			Recorder r = obj as Recorder;
			if (r != null) {
				r.StartRecording();
			}
		}

		// Set new state value
		State = RecordingState.Recording;
	}

	public static void StopRecording()
	{
		// Can only stop recording from Recording state
		if (State != RecordingState.Recording) {
			throw new UnityException("State must be 'Recording' to end a recording.");
		}

		// Find all Recorder objects in the scene and tell them to stop recording
		foreach (object obj in Object.FindObjectsOfType(typeof(Recorder))) {
			Recorder r = obj as Recorder;
			if (r != null) {
				r.StopRecording();
			}
		}

		// Set new state value
		State = RecordingState.Inactive;
	}

	// Can't deactivate this module during the click event or unity will hang because it is trying to wait for the click to finish before deactivating.
	// Track it here and deactivate during next update.
	private static List<GazeInputModule> modulesToDeactivate = new List<GazeInputModule>();

	public static void StartPlayback()
	{
		// Can only start recording from Inactive state
		if (State != RecordingState.Inactive) {
			throw new UnityException("State must be 'Inactive' to start playback.");
		}

		// Find GazeInputModule objects and deactivate them
		foreach (object obj in Object.FindObjectsOfType(typeof(GazeInputModule))) {
			GazeInputModule gim = obj as GazeInputModule;
			if (gim != null) {
				modulesToDeactivate.Add(gim);
			}
		}

		// Find GvrHead objects and disable them
		foreach (object obj in Object.FindObjectsOfType(typeof(GvrHead))) {
			GvrHead head = obj as GvrHead;
			if (head != null) {
				head.enabled = false;
			}
		}

		// Find all Recorder objects in the scene and tell them to start playing
		foreach (object obj in Object.FindObjectsOfType(typeof(Recorder))) {
			Recorder r = obj as Recorder;
			if (r != null) {
				r.StartPlayback();
			}
		}

		// Set new state value
		State = RecordingState.Playing;
	}

	public static void StopPlayback()
	{
		// Can only start recording from Inactive state
		if (State != RecordingState.Playing) {
			throw new UnityException("State must be 'Playing' to stop playback.");
		}

		// Find all Recorder objects in the scene and tell them to stop playing
		foreach (object obj in Object.FindObjectsOfType(typeof(Recorder))) {
			Recorder r = obj as Recorder;
			if (r != null) {
				r.StopPlayback();
			}
		}

		// Find GvrHead objects and enable them
		foreach (object obj in Object.FindObjectsOfType(typeof(GvrHead))) {
			GvrHead head = obj as GvrHead;
			if (head != null) {
				head.enabled = true;
			}
		}

		// Find GazeInputModule objects and activate them
		foreach (object obj in Object.FindObjectsOfType(typeof(GazeInputModule))) {
			GazeInputModule gim = obj as GazeInputModule;
			if (gim != null) {
				gim.enabled = true;
				gim.ActivateModule();
			}
		}

		// Set new state value
		State = RecordingState.Inactive;
	}

	public void StartPlaybackButtonPressed()
	{
		if (State == RecordingState.Recording) {
			RecordingManager.StopRecording();
		} else if (State == RecordingState.Playing) {
			RecordingManager.StopPlayback();
		}

		RecordingManager.StartPlayback();
	}
}
