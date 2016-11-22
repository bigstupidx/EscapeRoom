using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Fire : Searchable {
    public Ice ice;
    public GameObject iceCube;
    public GameObject key;
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
        if (iceCube.GetComponent<Pickup>().IsFound)
        {
            iceCube.transform.position = new Vector3(-3.64f, .79f, -4.11f);
            InvokeRepeating("countDown", 1, 1);
        }
    }

    public void countDown()
    {
        remaining--;
        // Display the message using the Notification Manager
        NotificationManager manager = FindObjectOfType<NotificationManager>();
        message = "Melting, please wait!";
        if (manager != null)
        {
            if(!manager.GetComponentInChildren<Text>())
                manager.ShowNotification(message, iceCube.transform.position);
        }
        if (remaining <= 0)
        {
            iceCube.SetActive(false);
            key.transform.position = new Vector3(-6.382f, .22f, -3.638f);
            CancelInvoke("countDown");
        }
    }
}
