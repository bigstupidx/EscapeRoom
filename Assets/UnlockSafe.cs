using UnityEngine;
using System.Collections;

public class UnlockSafe : MonoBehaviour {

	public static string codeEntry;
	string combination;

	// Use this for initialization
	void Start () {
		codeEntry = "";
	}
	
	// Update is called once per frame
	void Update () {
		if (codeEntry.Equals (combination)) {
			
		}
	}
}
