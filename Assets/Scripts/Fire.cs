using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Fire : Searchable {
    public GameObject iceCube;
    public GameObject key;
    public GameObject block;
    public GameObject campfire;
    private int remaining;
	// Use this for initialization
	void Start () {
        remaining = 5;
        message = "Melting in ";
    }
	
	// Update is called once per frame
	void Update ()
    {

    }
    public override void OnPointerClick(PointerEventData eventData)
    {
        if (campfire.activeSelf)
        {
            if (block.GetComponent<Pickup>().IsFound)
            {
                block.transform.position = new Vector3(4.65f, .685f, -1.39f);
                InvokeRepeating("countDown", 1, 1);
            }
        }
    }

    public void countDown()
    {
        remaining--;
        // Display the message
		DisplayMessage("Melting, please wait!");
        
        if (remaining <= 0)
        {
            iceCube.SetActive(false);
            key.transform.position = new Vector3(3.6619f, .0773f, -1.543446f);
            CancelInvoke("countDown");
        }
    }
}
