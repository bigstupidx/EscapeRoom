using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InspectionManager : MonoBehaviour {

	[HideInInspector]
	public Inspectable objectBeingInspected = null;

	[HideInInspector]
	public Vector3 cameraRight;

	public Canvas inspectionCanvas;

	public RotationButton upButton;
	public RotationButton downButton;
	public RotationButton leftButton;
	public RotationButton rightButton;

	public RotationButton exitButton1;
	public RotationButton exitButton2;

	// Use this for initialization
	void Start () {
		inspectionCanvas.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		if (objectBeingInspected == null) {
			return;
		}
			
		if (exitButton1.isPressed || exitButton2.isPressed) {
			// User has looked at invisible exit button - put down the item
			objectBeingInspected.PutDown();

			// Reset buttons
			exitButton1.isPressed = false;
			exitButton2.isPressed = false;
		} else if (upButton.isPressed) {
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
