using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using System.Collections.Generic;
using System;


public class MenuManager : MonoBehaviour
{
	/// <summary>
	/// This is used to save a recording from a scene before switching to a new scene
	/// </summary>
	/// <value>The saved recording.</value>
	public static Recording SavedRecording { get; set; }

	public enum Panels
	{
		Main,
		Settings,
		Recordings,
		Saves,
		Warning,
		NoRecording,
		Win,
		Stats,
		Iterations
	}

	public TextAsset demoRecording;

	[Serializable]
	public class PanelDescription
	{
		public Panels type;
		public GameObject panel;
	}

	public PanelDescription[] panels;

	private Dictionary<Panels, GameObject> panelDictionary = new Dictionary<Panels, GameObject>();

	public Button[] loadRecordingButtons = new Button[4];

	void Start()
	{
		// Add all the panels to a dictionary (hashmap) for easy lookup
		foreach (PanelDescription d in panels) {
			panelDictionary[d.type] = d.panel;
		}

		// Recenter the GVRViewer so that the menu is always in front of the user when scene loads
		GvrViewer viewer = FindObjectOfType<GvrViewer>();
		if (viewer != null) {
			viewer.Recenter();
		}

		// Activate the initial screen using the static StartingScreen property which may have been set by another class.
		CurrentPanel = StartingPanel;

		// Reset the starting panel back to main for next time
		StartingPanel = Panels.Main;
	}

	private static Panels _startingPanel = Panels.Main;

	public static Panels StartingPanel {
		get { return _startingPanel; }
		set { _startingPanel = value; }
	}

	private Panels? _currentPanel = null;

	public Panels? CurrentPanel {
		get { return _currentPanel; }
		set {
			// Check if the value has changed
			if (value == _currentPanel) {
				// It's the same so do nothing
				return;
			}

			// Deactivate all panels (The active one will be activated later)
			foreach (GameObject panel in panelDictionary.Values) {
				panel.SetActive(false);
			}

			// Commit new value
			_currentPanel = value;

			// Activate the new current screen, if any
			if (_currentPanel != null) {
				panelDictionary[(Panels)_currentPanel].SetActive(true);
			}
		}
	}

	public void PlayButtonPressed()
	{
		// Make sure that any startup recording is cleared
		SavedRecording = null;

		// Get notified when the scene has finished loading
		SceneManager.sceneLoaded += SceneManager_sceneLoadedGameplay;

		// Set the next menu scene to load to be the saves scene
		StartingPanel = Panels.Win;

		// Load the game scene
		SceneManager.LoadScene("FinalScene");
	}

	void SceneManager_sceneLoadedGameplay(Scene arg0, LoadSceneMode arg1)
	{
		// Start recording the game scene
		RecordingManager.StartRecording();

		// Start Staticiscs Timing
		Statistics.StartTiming();

		// Unsubscribe from notification
		SceneManager.sceneLoaded -= SceneManager_sceneLoadedGameplay;
	}

	public void RecordingsButtonPressed()
	{
		CurrentPanel = Panels.Recordings;

		// Disable button if file does not exist
		for (int i = 0; i < 4; ++i) {
			loadRecordingButtons[i].interactable = File.Exists(FileNameFromSlotNumber(i + 1));
		}
	}

	public void SettingsButtonPressed()
	{
		CurrentPanel = Panels.Settings;
	}

	public void QuitButtonPressed()
	{
		Application.Quit();
	}

	public void MainMenuButtonPressed()
	{
		CurrentPanel = Panels.Main;
	}

	private string FileNameFromSlotNumber(int slot)
	{
		// Generate file name in the format slot#Recording.xml
		return string.Format("{0}{1}slot{2}Recording.xml", Application.persistentDataPath, Path.DirectorySeparatorChar, slot);
	}

	public void LoadRecordingFromSlotButtonPressed(int slot)
	{
		// Load the recording file from disk
		// Really we should check if the file exists and show an error message instead of just causing a file not 
		//found exception here.
		if (File.Exists(FileNameFromSlotNumber(slot))) {
			SavedRecording = Recording.Load(FileNameFromSlotNumber(slot));
			CurrentPanel = Panels.Iterations;
		} else {	
			CurrentPanel = Panels.NoRecording;
		}
	}

	public void SelectIterationsButtonPressed(int iterations)
	{
		CurrentPanel = Panels.Warning;
		remainingIterations = iterations;
		Invoke("StartPlayback", 1.5f);
	}

	private int remainingIterations = 1;
	private bool firstIteration = true;

	void StartPlayback()
	{
		// Get notified when the scene has finished loading
		SceneManager.sceneLoaded += SceneManager_sceneLoadedPlaybackRecording;

		// Subscribe to event telling us when playback is complete
		RecordingManager.PlaybackComplete += RecordingManager_PlaybackComplete;

		// Set the next menu scene to load to be the saves scene
		StartingPanel = Panels.Stats;

		// Note that this is the first iteration for the callback so that timing can be started
		firstIteration = true;

		// Load the game scene
		SceneManager.LoadScene("FinalScene");

		Statistics.StartTiming();
	}

	public void LoadDemoRecording()
	{
		SavedRecording = Recording.Load(demoRecording);
		CurrentPanel = Panels.Iterations;
	}

	void SceneManager_sceneLoadedPlaybackRecording(Scene arg0, LoadSceneMode arg1)
	{
		// Apply the saved recording to the scene
		RecordingManager.SetRecordingOnActiveScene(SavedRecording);

		if (firstIteration) {
			Statistics.StartTiming();
			firstIteration = false;
		}

		// Start playing back the recording
		RecordingManager.StartPlayback();
	}

	public void SaveRecordingYesButtonPressed()
	{
		CurrentPanel = Panels.Saves;
	}

	public void SaveRecordingToSlotButtonPressed(int slot)
	{
		// Save the recording file to disk
		// Really we should be checking if the file exists and showing the user an "Are you sure?" message here.
		string fileName = FileNameFromSlotNumber(slot);
		SavedRecording.Save(fileName);

		// Clear the recording file
		SavedRecording = null;

		// Return to main menu
		SceneManager.LoadScene("MenuScene");
	}

	/// <summary>
	/// This function fires when the playback of the recording is complete.  
	/// If there are more iterations to play, it will reload the scene and start playback again.
	/// Otherwise, it will unsubscribe.
	/// </summary>
	void RecordingManager_PlaybackComplete()
	{
		remainingIterations--;

		if (remainingIterations == 0) {
			// There are no iterations left, so we are done.  Clean up

			// Unsubscribe
			RecordingManager.PlaybackComplete -= RecordingManager_PlaybackComplete;
			SceneManager.sceneLoaded -= SceneManager_sceneLoadedPlaybackRecording;

			//Calculate WinScene stats
			Statistics.StopTiming();

			// Return to main menu
			SceneManager.LoadScene("MenuScene");
		} else {
			// We need to play the scene again
			SceneManager.LoadScene("FinalScene");
		}
	}
}
