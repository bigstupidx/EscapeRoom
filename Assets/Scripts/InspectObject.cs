using UnityEngine;
using System.Collections;

public class InspectObject : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnPointerClick()//(PointerEventData eventData)
    {
        if (GameVariables.inspectingObject == false)
        {
            gameObject.GetComponent<Animation>().Play();
        }
 


    }
}
