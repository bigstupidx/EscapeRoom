using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class TeleportCamera : MonoBehaviour, IPointerClickHandler
{

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

    public virtual void OnPointerClick(PointerEventData eventData)
    {
        // Debug.Log(string.Format("Object {0} was clicked.", name));
        //MainCamera.transform.position = new Vector3(-3, 1, 0);

        MainCamera.transform.position = new Vector3(transform.position.x, 1.7f, transform.position.z);
        // transform.position = new Vector3(1, 1, 8);

        //anim = MainCamera.GetComponent<Animation>();
        //anim["CameraTeleportTest"].speed = animSpeed;
        //MainCamera.GetComponent<Animation>().Play();
    }
}
