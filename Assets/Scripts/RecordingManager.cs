﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;

public delegate void PlaybackCompleteHandler();

public class RecordingManager : MonoBehaviour
{
	public static event PlaybackCompleteHandler IterationComplete;

	public static event PlaybackCompleteHandler PlaybackComplete;

	public static RecordingState StartupState { get; set; }

	public static string StartupPlaybackFileName { get; set; }

	// Use this for initialization
	void Start()
	{
		if (StartupState == RecordingState.Playing) {
			Recording recording = Recording.Load(StartupPlaybackFileName);
			RecordingManager.SetRecording(recording);
			StartPlayback();
		} else if (StartupState == RecordingState.Recording) {
			StartRecording();
		}
	}
	
	// Update is called once per frame
	void Update()
	{
		foreach (GazeInputModule gim in modulesToDeactivate) {
			gim.DeactivateModule();
			gim.enabled = false;
		}

		modulesToDeactivate.Clear();

		// Check if recording is done
		if (State == RecordingState.Playing) {
			if (Time.realtimeSinceStartup - playbackStartTime > playbackLength) {
				StopPlayback();
				if (remainingIterations > 0) {
					StartPlayback();
					remainingIterations--;

					// Fire an iteration complete event so that subscribed classes can react (for example, to reset the world)
					if (IterationComplete != null) {
						IterationComplete();
					}
				} else {

					// Fire a playback complete event so that subscribed classes can react (for example, to show a screen with recorded stats)
					if (PlaybackComplete != null) {
						PlaybackComplete();
					}
				}
			}
		}
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

	private static float playbackStartTime = 0.0f;
	private static float playbackLength = 0.0f;
	private static int remainingIterations = 0;

	public static void StartPlayback(int iterations = 1)
	{
		// Can only start recording from Inactive state
		if (State != RecordingState.Inactive) {
			throw new UnityException("State must be 'Inactive' to start playback.");
		}

		if (iterations < 1) {
			throw new UnityException("Iteration count must be one or more");
		}

		remainingIterations = iterations - 1;

		playbackStartTime = Time.realtimeSinceStartup;

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
		playbackLength = 0.0f;
		foreach (object obj in Object.FindObjectsOfType(typeof(Recorder))) {
			Recorder r = obj as Recorder;
			if (r != null) {
				r.StartPlayback();

				// Record the longest length value
				if (r.Length > playbackLength) {
					playbackLength = r.Length;
				}
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

	public void SaveRecordingButtonPressed()
	{
		if (State == RecordingState.Recording) {
			RecordingManager.StopRecording();
		} else if (State == RecordingState.Playing) {
			RecordingManager.StopPlayback();
		}

		Recording recording = GetRecording();
		recording.Save("testRecording.xml");
	}

	public void LoadRecordingButtonPressed()
	{
		if (State == RecordingState.Recording) {
			RecordingManager.StopRecording();
		} else if (State == RecordingState.Playing) {
			RecordingManager.StopPlayback();
		}

		// Subscribe to iteration complete event with code that resets the scene
		IterationComplete += RecordingManager_IterationComplete;

		Recording recording = Recording.Load("testRecording.xml");
		SetRecording(recording);

		RecordingManager.StartPlayback(2);
	}


	void RecordingManager_IterationComplete ()
	{
		GameObject cube = GameObject.Find("Cube");
		Teleport teleport = cube.GetComponent(typeof(Teleport)) as Teleport;
		teleport.Reset();

		// Unsubscribe
		IterationComplete -= RecordingManager_IterationComplete;
	}

	public static Recording GetRecording()
	{
		Recording recording = new Recording();

		// Find all Recorder objects in the scene and add their timelines to the recording
		foreach (object obj in Object.FindObjectsOfType(typeof(Recorder))) {
			Recorder recorder = obj as Recorder;

			if (recorder != null) {
				foreach (Recording.Timeline t in recorder.GetTimelines()) {
					recording.Timelines.Add(t);
				}
			}
		}

		return recording;
	}

	public static void SetRecording(Recording recording)
	{
		// Build dictionary of names from the recordings by type
		Dictionary<string, List<Recording.Timeline>> timelines = new Dictionary<string, List<Recording.Timeline>>();

		foreach (Recording.Timeline t in recording.Timelines) {
			List<Recording.Timeline> list = null;
			if (!timelines.TryGetValue(t.ObjectName, out list)) {
				list = new List<Recording.Timeline>();
			}

			list.Add(t);
			timelines[t.ObjectName] = list;
		}

		// Find all Recorder objects in the scene and apply a timeline if it matches the name
		foreach (object obj in Object.FindObjectsOfType(typeof(Recorder))) {
			Recorder recorder = obj as Recorder;
			if (recorder != null) {
				List<Recording.Timeline> list = null;
				if (timelines.TryGetValue(recorder.name, out list)) {
					recorder.SetTimelines(list);
				} else {
					recorder.Clear();
				}
			}
		}
	}
}
