using UnityEngine;
using System.Collections;

public class CameraFacingBillboard : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Util.RotateAroundYToFaceCamera(transform);
	}
}
