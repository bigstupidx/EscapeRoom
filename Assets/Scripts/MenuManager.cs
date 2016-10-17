using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;

[ExecuteInEditMode]
public class MenuManager : MonoBehaviour
{

	public void PlayButtonPressed()
	{
		// Make sure that any startup recording is cleared
		RecordingManager.SavedRecording = null;

		// Load the game scene
		SceneManager.LoadScene("CSC495Demo");
	}

	public void RecordingsButtonPressed()
	{
	//	if (Application.isPlayer) {
			SceneManager.LoadScene ("RecordingSavesMenu");
//		} else if(Application.isEditor) {
//			EditorSceneManager.OpenScene ("RecordingSavesMenu");
//		}
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

	private string FileNameFromSlotNumber(int slot)
	{
		// Generate file name in the format slot#Recording.xml
		return string.Format("slot{0}Recording.xml", slot);
	}

	public void LoadRecordingFromSlotButtonPressed(int slot)
	{
		// Load the recording file from disk
		// Really we should check if the file exists and show an error message instead of just causing a file not found exception here.
		string fileName = FileNameFromSlotNumber(slot);
		RecordingManager.SavedRecording = Recording.Load(fileName);

		// Subscribe to event telling us when playback is complete
		RecordingManager.PlaybackComplete += RecordingManager_PlaybackComplete;

		// Load the game scene
		SceneManager.LoadScene("CSC495Demo");
	}

	public void SaveRecordingToSlotButtonPressed(int slot)
	{
		// Save the recording file to disk
		// Really we should be checking if the file exists and showing the user an "Are you sure?" message here.
		string fileName = FileNameFromSlotNumber(slot);
		RecordingManager.SavedRecording.Save(fileName);

		// Clear the recording file
		RecordingManager.SavedRecording = null;

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
