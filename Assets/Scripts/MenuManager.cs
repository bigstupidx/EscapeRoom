using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour {



	public void PlayButtonPressed() {
		Application.LoadLevel ("TestScene");
	}

	void RecordingsButtonPressed() {

	}

	public void SettingsButtonPressed() {
		Application.LoadLevel ("SettingsMenuScene");
	}

	public void QuitButtonPressed() {
		Application.Quit();
	}
}
