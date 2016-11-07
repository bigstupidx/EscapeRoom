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
        key.transform.position = new Vector3(-2.07f, .27f, -8.43f);
        key.SetActive(false);
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (remaining <= 0)
        {
            key.SetActive(true);
            iceCube.SetActive(false);
            CancelInvoke("countDown");
        }
    }
    public override void OnPointerClick(PointerEventData eventData)
    {
        if (ice.isSelected())
        {
            iceCube.transform.position = new Vector3(-.01f, 2.146f, -8.8795f);
            InvokeRepeating("countDown", 1, 1);
        }
    }

    public void countDown()
    {
        remaining--;
        // Display the message using the Notification Manager
        NotificationManager manager = FindObjectOfType<NotificationManager>();

        if (manager != null)
        {
            manager.ShowNotification(message + remaining, iceCube.transform.position);
        }

    }
}
