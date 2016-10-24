using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class KeySelection : Focusable
{
	public void Start()
	{
		// Hide the key when the game starts, but leave it visible in the editor to make it easier to work with
		this.gameObject.SetActive(false);
	}

	public override void OnPointerClick(PointerEventData eventData)
	{
		// Stop recording
		RecordingManager.StopRecording();

		// Save the recording into the static variable of the MenuManager
		MenuManager.SavedRecording = RecordingManager.GetRecordingFromActiveScene();

		// Load the win scene
		SceneManager.LoadScene("WinScene");
	}
}
