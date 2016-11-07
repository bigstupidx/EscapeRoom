using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class OpenCloseClockDoor : Searchable {

    public bool DoorOpen = false;
    public GameObject ClockKeyTable;
	
    OpenCloseClockDoor()
    {
        if (GameVariables.bigPointerTime == 6 && GameVariables.smallPointerTime == 3)
        {
            message = "You found a key!";
        }
        else
        {
            message = "It's locked.";
        }
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        NotificationManager manager = FindObjectOfType<NotificationManager>();
		Collider collider = GetComponent<Collider>();

        if (DoorOpen == false && GameVariables.bigPointerTime == 6 && GameVariables.smallPointerTime == 3)
        {
            GetComponent<Animation>().Play("DoorOpen");
            DoorOpen = true;
        }
        else if (DoorOpen == true)
        {

            GetComponent<Animation>().Play("DoorClose");
            DoorOpen = false;
        }
        else if (DoorOpen == false && GameVariables.bigPointerTime != 6 || GameVariables.smallPointerTime != 3)
        {
            if (manager != null)
            {
				manager.ShowNotification(message, collider.bounds);
            }
        }
    }
}
