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
			// Load the win scene
			SceneManager.LoadScene("WinScene");

            // Stop recording
            RecordingManager.StopRecording();

            //Calculate WinScene stats
            Statistics.Stop();

            // Save the recording into the static variable of the MenuManager
            MenuManager.SavedRecording = RecordingManager.GetRecordingFromActiveScene();
        }

		return true;
	}
}
