using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class FinalDoor : Lockable {

	public Pickup [] keys;

	public override bool CanUnlock()
	{
        // Only let the player escape when all keys are found
        /*foreach (Pickup key in keys) {
			if (!key.IsFound) {
				return false;
			}
		}*/

		if (RecordingManager.State == RecordingState.Recording) {
			Invoke("LoadMenuScene", 0.5f);
		}

		return true;
	}

	private void LoadMenuScene()
	{
		// Load the win scene
		SceneManager.LoadScene("MenuScene");
		// Stop recording
		RecordingManager.StopRecording();
		Statistics.StopTiming();
		// Save the recording into the static variable of the MenuManager
		MenuManager.SavedRecording = RecordingManager.GetRecordingFromActiveScene();
	}
}
