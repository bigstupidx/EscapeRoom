using UnityEngine;
using System.Collections;

public class UnlockSafe : MonoBehaviour {

	public static string codeEntry;
	public string combination;
	public GoalMover safeTop;
	public GameObject openSafeTop;

	// Use this for initialization
	void Start () {
		codeEntry = "";
	}
	
	// Update is called once per frame
	void Update () {
		if (codeEntry.Equals (combination)) {
			Vector3 pos = openSafeTop.transform.position;
			Quaternion rot = openSafeTop.transform.rotation;
		
			safeTop.AddGoal (pos, rot, null);
		}
	}
}
