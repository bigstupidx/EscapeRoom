using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class OpenCloseClockDoor : Searchable {

    public bool DoorOpen = false;
    public GameObject ClockKeyTable;
	
    OpenCloseClockDoor()
    {
        if (GameVariables.bigPointerTime == 3 && GameVariables.smallPointerTime == 6)
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

        

        if (DoorOpen == false && GameVariables.bigPointerTime == 3 && GameVariables.smallPointerTime == 6)
        {
            GetComponent<Animation>().Play("DoorOpen");
            DoorOpen = true;
        }
        else if (DoorOpen == true)
        {

            GetComponent<Animation>().Play("DoorClose");
            DoorOpen = false;
        }
        else if (DoorOpen == false && GameVariables.bigPointerTime != 3 || GameVariables.smallPointerTime != 6)
        {
            if (manager != null)
            {
                manager.ShowNotification(message, gameObject.transform.position);
            }
        }
    }
}
