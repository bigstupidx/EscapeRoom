using UnityEngine;
using System.Collections;

public class TestBuildManager : MonoBehaviour {

	public TextAsset demoRecording;

	public int iterations = 1;

	// Use this for initialization
	void Start () {
		MenuManager.SavedRecording = Recording.Load(demoRecording);
		MenuManager.remainingIterations = iterations;
		MenuManager.StartPlayback();
	}
}
