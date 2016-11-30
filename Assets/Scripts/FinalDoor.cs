using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class FinalDoor : Lockable {

	public Pickup [] keys;

	public Canvas doorCanvas;

	public override void Start()
	{
		base.Start();
		doorCanvas.enabled = false;
	}

	public override bool CanUnlock()
	{
        // Only let the player escape when all keys are found
        foreach (Pickup key in keys) {
			if (!key.IsFound) {
				// Show quit menu
				doorCanvas.transform.position = Util.GetPointBetweenPositionAndCamera(transform.position, 1.0f);
				Util.RotateAroundYToFaceCamera(doorCanvas.transform);
				doorCanvas.enabled = true;
				return false;
			}
		}

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

	public void HideDoorCanvas() {
		doorCanvas.enabled = false;
	}

	public void Quit() {
		// Only do something if the user is playing, not if a recording is playing
		if (RecordingManager.State == RecordingState.Recording) {
			LoadMenuScene();
		}
	}
}
