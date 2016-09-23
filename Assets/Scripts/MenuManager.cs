using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour {

    public RecordingManager recordingManager;

	public void PlayButtonPressed()
    {
		Application.LoadLevel ("GameScene");
	}

	public void RecordingsButtonPressed()
    {
        Application.LoadLevel("RecordingsMenuScene");
    }

	public void SettingsButtonPressed()
    {
		Application.LoadLevel ("SettingsMenuScene");
	}

	public void QuitButtonPressed()
    {
		Application.Quit();
	}

    public void MainMenuButtonPressed()
    {
        Application.LoadLevel("MenuScene");
    }

    public void ReplayRecordingButtonPressed()
    {
        Application.LoadLevel("GameScene");
        recordingManager.LoadRecordingButtonPressed();
    }
}
