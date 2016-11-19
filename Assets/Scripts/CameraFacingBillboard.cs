using UnityEngine;
using System.Collections;

public class CameraFacingBillboard : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 targetPos = transform.position + Camera.main.transform.rotation * Vector3.forward;
		Vector3 targetOrientation = Camera.main.transform.rotation * Vector3.up;
		transform.LookAt(targetPos, targetOrientation);
	}
}
