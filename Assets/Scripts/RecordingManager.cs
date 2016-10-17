using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;

public delegate void PlaybackCompleteHandler();

public class RecordingManager : MonoBehaviour
{
	/// <summary>
	/// This event will be triggered when a recording that is being played back finishes playing.
	/// </summary>
	public static event PlaybackCompleteHandler PlaybackComplete;

	/// <summary>
	/// Storage for RecordingState property.
	/// </summary>
	private static RecordingState _state = RecordingState.Inactive;

	/// <summary>
	/// Tracks the current state of the RecordingManager.  It must transition from Inactive > Playing > Inactive or from Inactive > Recording > Inactive
	/// </summary>
	public static RecordingState State {
		get { return _state; }
		private set { _state = value; }
	}

	/// <summary>
	/// Called once per frame.  It is used to update the state of the recording during playback and trigger the PlaybackComplete event when playback is done.
	/// </summary>
	void Update()
	{
		// This loop is here because I decided to delay deactivation of GazeInputModules until the next frame.  It seemed to be causing problems to
		// deactivate them when StartPlayback was called.
		foreach (GazeInputModule gim in modulesToDeactivate) {
			gim.DeactivateModule();
			gim.enabled = false;
		}

		// Clear the list since we have now deactivated all of them
		modulesToDeactivate.Clear();

		// Now the main task of this function - Check if playback is done
		if (State == RecordingState.Playing) {
			if (Time.realtimeSinceStartup - playbackStartTime > playbackLength) {
				StopPlayback();

				// Fire a playback complete event so that subscribed classes can react (for example, to show a screen with recorded stats)
				if (PlaybackComplete != null) {
					PlaybackComplete();
				}
			}
		}
	}

	/// <summary>
	/// Starts recording on the current scene.  It iterates through all Recorder scripts attached to objects in the scene and tells them
	/// to start recording, so it won't do anything unless there are Recorder scripts in the scene.
	///
	/// May only be called when RecordingManager is in Inactive state. (You can't start recording if you are playing)
	/// </summary>
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

	/// <summary>
	/// Stops recording on the current scene.  It iterates through all Recorder scripts attached to objects in the scene and tells them
	/// to stop recording, so it won't do anything unless there are Recorder scripts in the scene.
	///
	/// May only be called when RecordingManager is in Recording state. (It can't stop recording if you didn't start or is playing)
	/// </summary>
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
		
	/// <summary>
	/// Starts playback on the current scene.  It iterates through all Recorder scripts attached to objects in the scene and tells them
	/// to start playing, so it won't do anything unless there are Recorder scripts in the scene.  When playback is done, the PlaybackComplete
	/// event will be fired.  If a recording hasn't been loaded into any recorder in the scene, this will happen immediately.
	/// 
	/// May only be called when RecordingManager is in Inactive state.  (It can't start playing if it's already playing or recording)
	/// </summary>
	public static void StartPlayback()
	{
		// Can only start playback from Inactive state
		if (State != RecordingState.Inactive) {
			throw new UnityException("State must be 'Inactive' to start playback.");
		}

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

	/// <summary>
	/// Stops playback on the current scene.  It iterates through all Recorder scripts attached to objects in the scene and tells them
	/// to stop playing, so it won't do anything unless there are Recorder scripts in the scene.  The PlaybackComplete event will not
	/// fire if this is called before playback is complete.
	/// 
	/// May be called when RecordingManager is in Inactive or Playing state. (It can't stop playing if it's recording).  Calling when Inactive does nothing, but is harmless.
	/// </summary>
	public static void StopPlayback()
	{
		// Calling this when the state is "inactive" is harmless
		if (State == RecordingState.Inactive) {
			return;
		}

		// Otherwise, state should be "Playing" in order to stop playback.
		if (State != RecordingState.Playing) {
			throw new UnityException("State must be 'Playing' or 'Inactive' to stop playback.");
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

	/// <summary>
	/// Creates a Recording object from the current scene which can be saved to a file.  It does this by iterating through all
	/// the recorders in the scene and asking them to create savable timelines, which it adds to the list of timelines in the
	/// recording object.
	/// </summary>
	/// <returns>The recording from active scene.</returns>
	public static Recording GetRecordingFromActiveScene()
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

	/// <summary>
	/// Copies the data from a Recording object that may have been loaded form a file or a different scene into the recorders attached to
	/// objects in the current scene.  The recording can then be played back by calling StartPlayback.  Timelines in the recording are
	/// copied to recorders attached to objects by name, so the names in the scene must be unique and match what they were at the time
	/// of recording.  Mismatched names are ignored.
	/// </summary>
	/// <param name="recording">Recording.</param>
	public static void SetRecordingOnActiveScene(Recording recording)
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

	// Can't deactivate this module during the click event or unity will hang because it is trying to wait for the click to finish before deactivating.
	// Track it here and deactivate during next update.
	private static List<GazeInputModule> modulesToDeactivate = new List<GazeInputModule>();

	/// <summary>
	/// The time when playback was started, in seconds.
	/// </summary>
	private static float playbackStartTime = 0.0f;

	/// <summary>
	/// The length of the currently playing recording, in seconds.  This is the length of the longest invididual recording in any recorder in the scene.
	/// </summary>
	private static float playbackLength = 0.0f;
}
