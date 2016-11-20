using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class FinalDoor : Lockable {

	public Pickup [] keys;
    public static bool WinGame = false;

	public override bool CanUnlock()
	{
        // Only let the player escape when all keys are found
        /*foreach (Pickup key in keys) {
			if (!key.IsFound) {
				return false;
			}
		}*/

        WinGame = true;

        if (RecordingManager.State == RecordingState.Recording) {
			// Load the win scene
			SceneManager.LoadScene("MenuScene");

            
           
            // Stop recording
            RecordingManager.StopRecording();

            //Calculate WinScene stats
            Statistics.StopTiming();

            // Save the recording into the static variable of the MenuManager
            MenuManager.SavedRecording = RecordingManager.GetRecordingFromActiveScene();
        }

		return true;
	}
}
