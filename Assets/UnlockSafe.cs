using UnityEngine;
using System.Collections;

public class UnlockSafe : MonoBehaviour {

	public static string codeEntry;
	public string combination;
	public Lockable safeTop;
	public GameObject keyPad;
	public static readonly Vector3 hiddenPosition = new Vector3(1000, 1000, 1000);

	// Use this for initialization
	void Start () {
		codeEntry = "";
		Hide();
	}
	
	// Update is called once per frame
	void Update () {
		if (safeTop.isLocked && codeEntry.Equals (combination)) {
			safeTop.isLocked = false;
			safeTop.IsOpen = true;
			Hide();
			safeTop.DisplayMessage(safeTop.unlockedMessage);
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
