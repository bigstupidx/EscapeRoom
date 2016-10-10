using UnityEngine;
using System.Collections;

public class TeleportCamera : MonoBehaviour {

    public GameObject MainCamera;
    Animation anim;
    private float animSpeed = 0.4f;

	// Use this for initialization
	void Start () {

        MainCamera = GameObject.Find("Main Camera");


	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnPointerClick()
    {
        // Debug.Log(string.Format("Object {0} was clicked.", name));
        // MainCamera.transform.position = transform.position;
        // transform.position = new Vector3(5, 1, 8);

        anim = MainCamera.GetComponent<Animation>();
        anim["CameraTeleportTest"].speed = animSpeed;
        MainCamera.GetComponent<Animation>().Play();
    }
}
