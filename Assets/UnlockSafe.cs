using UnityEngine;
using System.Collections;

public class UnlockSafe : MonoBehaviour {

	public static string codeEntry;
	public string combination;
	public GoalMover safeTop;
	public GameObject openSafeTop;
	public GameObject keyPad;
	public Searchable safe;
	public static readonly Vector3 hiddenPosition = new Vector3(1000, 1000, 1000);

	// Use this for initialization
	void Start () {
		codeEntry = "";
		Hide();
	}
	
	// Update is called once per frame
	void Update () {
		if (codeEntry.Equals (combination)) {
			Vector3 pos = openSafeTop.transform.position;
			Quaternion rot = openSafeTop.transform.rotation;
		
			safeTop.AddGoal (pos, rot, null);
			Hide();
			safe.message = "Safe Unlocked!";
		}
	}

	public void Hide()
	{
		keyPad.transform.position = hiddenPosition;
	}

	public void Show(Vector3 position)
	{
		keyPad.transform.position = position;
		Util.RotateAroundYToFaceCamera(keyPad.transform);
	}
}
