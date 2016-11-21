using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MenuManager : MonoBehaviour
{
	/// <summary>
	/// This is used to save a recording from a scene before switching to a new scene
	/// </summary>
	/// <value>The saved recording.</value>
	public static Recording SavedRecording { get; set; }

	public GameObject settingsPanel;
	public GameObject mainPanel;
	public GameObject recordingsPanel;
	public GameObject savesPanel;
    public GameObject warningPanel;

	public TextAsset demoRecording;

	void Start()
	{
		recordingsPanel.SetActive(false);
		settingsPanel.SetActive(false);
		savesPanel.SetActive(false);
        warningPanel.SetActive(false);

		if (FinalDoor.WinGame == true) {
			savesPanel.SetActive(true);
			mainPanel.SetActive(false);
		}
		FinalDoor.WinGame = false;
        
	}

	public void PlayButtonPressed()
	{
		// Make sure that any startup recording is cleared
		SavedRecording = null;

		// Get notified when the scene has finished loading
		SceneManager.sceneLoaded += SceneManager_sceneLoadedGameplay;

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
		mainPanel.SetActive(false);
		recordingsPanel.SetActive(true);
	}

	public void SettingsButtonPressed()
	{
		mainPanel.SetActive(false);
		settingsPanel.SetActive(true);
	}

	public void QuitButtonPressed()
	{
		Application.Quit();
	}

	public void MainMenuButtonPressed()
	{
		SceneManager.LoadScene("MenuScene");
		recordingsPanel.SetActive(false);
		settingsPanel.SetActive(false);
		savesPanel.SetActive(false);
        warningPanel.SetActive(false);
		mainPanel.SetActive(true);
	}

	private string FileNameFromSlotNumber(int slot)
	{
		// Generate file name in the format slot#Recording.xml
		return string.Format("slot{0}Recording.xml", slot);
	}

	public void LoadRecordingFromSlotButtonPressed(int slot)
	{
        recordingsPanel.SetActive(false);
        warningPanel.SetActive(true);
		// Load the recording file from disk
		// Really we should check if the file exists and show an error message instead of just causing a file not found exception here.
		string fileName = FileNameFromSlotNumber(slot);
		SavedRecording = Recording.Load(fileName);

		Invoke("StartPlayback", 3);
	}

	void StartPlayback()
	{
		// Get notified when the scene has finished loading
		SceneManager.sceneLoaded += SceneManager_sceneLoadedPlaybackRecording;
		// Subscribe to event telling us when playback is complete
		RecordingManager.PlaybackComplete += RecordingManager_PlaybackComplete;
		// Load the game scene
		SceneManager.LoadScene("FinalScene");
	}

	public void LoadDemoRecording()
	{
        recordingsPanel.SetActive(false);
        warningPanel.SetActive(true);

        SavedRecording = Recording.Load(demoRecording);

        Invoke("StartPlayback", 3);
    }

	void SceneManager_sceneLoadedPlaybackRecording(Scene arg0, LoadSceneMode arg1)
	{
		// Apply the saved recording to the scene
		RecordingManager.SetRecordingOnActiveScene(SavedRecording);

		// Start playing back the recording
		RecordingManager.StartPlayback();

		// Unsubscribe from notification
		SceneManager.sceneLoaded -= SceneManager_sceneLoadedPlaybackRecording;
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
		// For now we are assuming one iteration, so we are done.  

		// Unsubscribe
		RecordingManager.PlaybackComplete -= RecordingManager_PlaybackComplete;

		// Return to main menu
		SceneManager.LoadScene("MenuScene");
	}
}
