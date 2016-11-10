using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InspectionManager : MonoBehaviour {

	public Inspectable objectBeingInspected = null;

	public Canvas inspectionCanvas;

	public RotationButton upButton;
	public RotationButton downButton;
	public RotationButton leftButton;
	public RotationButton rightButton;

	public Vector3 cameraRight;

	// Use this for initialization
	void Start () {
		inspectionCanvas.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		if (objectBeingInspected == null) {
			return;
		}

		if (upButton.isPressed) {
			objectBeingInspected.transform.Rotate(cameraRight * (Time.deltaTime * 45), Space.World);
		} else if (downButton.isPressed) {
			objectBeingInspected.transform.Rotate(cameraRight * (Time.deltaTime * -45), Space.World);
		} else if (rightButton.isPressed) {
			objectBeingInspected.transform.Rotate(Vector3.down * (Time.deltaTime * 45), Space.World);
		} else if (leftButton.isPressed) {
			objectBeingInspected.transform.Rotate(Vector3.up * (Time.deltaTime * 45), Space.World);
		}
	}
}
