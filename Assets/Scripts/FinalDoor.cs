using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class FinalDoor : Lockable {

	public Pickup [] keys;

	public override bool CanUnlock()
	{
		// Only let the player escape when all keys are found
		foreach (Pickup key in keys) {
			if (!key.IsFound) {
				return false;
			}
		}

		if (RecordingManager.State == RecordingState.Recording) {
			// Stop recording
			RecordingManager.StopRecording();

			// Save the recording into the static variable of the MenuManager
			MenuManager.SavedRecording = RecordingManager.GetRecordingFromActiveScene();

			// Load the win scene
			SceneManager.LoadScene("WinScene");
		}

		return true;
	}
}
