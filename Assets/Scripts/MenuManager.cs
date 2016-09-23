using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour {



	public void PlayButtonPressed() {
		Application.LoadLevel ("GameScene");
	}

	public void RecordingsButtonPressed() {
        Application.LoadLevel("RecordingsMenuScene");
    }

	public void SettingsButtonPressed() {
		Application.LoadLevel ("SettingsMenuScene");
	}

	public void QuitButtonPressed() {
		Application.Quit();
	}
}
