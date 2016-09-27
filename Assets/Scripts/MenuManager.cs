using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

    public RecordingManager recordingManager;


	public void PlayButtonPressed()
    {
		SceneManager.LoadScene("CSC495Demo");
		RecordingManager.StartRecording ();
	}

	public void RecordingsButtonPressed()
    {
        SceneManager.LoadScene("RecordingsMenuScene");
    }

	public void SettingsButtonPressed()
    {
		SceneManager.LoadScene("SettingsMenuScene");
	}

	public void QuitButtonPressed()
    {
		Application.Quit();
	}

    public void MainMenuButtonPressed()
    {
		SceneManager.LoadScene("MenuScene");
    }

    public  void ReplayRecordingButtonPressed()
    {
		RecordingManager.StartupState = RecordingState.Playing;
		RecordingManager.StartupPlaybackFileName = "testRecording.xml";

		SceneManager.LoadScene("CSC495Demo");

		//recordingManager.LoadRecordingButtonPressed();
		// Subscribe to iteration complete event with code that resets the scene
		RecordingManager.IterationComplete +=  RecordingManager_IterationComplete;
		RecordingManager.PlaybackComplete +=  RecordingManager_PlaybackComplete;
    }
		
	void RecordingManager_IterationComplete ()
	{
		SceneManager.LoadScene("CSC495Demo");
	}

	void RecordingManager_PlaybackComplete ()
	{
		
		// Unsubscribe
		RecordingManager.IterationComplete -= RecordingManager_IterationComplete;
		// Unsubscribe
		RecordingManager.PlaybackComplete -= RecordingManager_PlaybackComplete;
	}
}
