using System;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

public class MenuItems
{
	[MenuItem("Tools/Add Input Recorders")]
	private static void AddInputRecorders()
	{
		int addedCount = 0;

		// Make sure any focusable objects also have input recorders attached
		foreach (Focusable focusable in SceneAsset.FindObjectsOfType<Focusable>()) {
			if (focusable.gameObject.GetComponent<InputRecorder>() == null) {
				Undo.AddComponent<InputRecorder>(focusable.gameObject);
				addedCount++;
			}
		}

		Debug.Log(String.Format("Added {0} InputRecorder(s) to the scene.", addedCount));
	}

	[MenuItem("Tools/Check Input Recorder Names")]
	private static void CheckInputRecorderNames()
	{
		HashSet<string> foundNames = new HashSet<string>();

		int errorCount = 0;

		foreach (InputRecorder recorder in SceneAsset.FindObjectsOfType<InputRecorder>()) {
			string name = recorder.gameObject.name;
			if (foundNames.Contains(name)) {
				Debug.LogError(String.Format("Detected duplicate Input Recorder name: {0}", name));
				++errorCount;
			} else {
				foundNames.Add(recorder.name);
			}
		}

		if (errorCount == 0) {
			Debug.Log("No Input Recorder name conflicts were detected.");
		}
	}
}
