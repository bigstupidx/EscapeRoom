using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class IceKey : Pickup {

    public GameObject wax;
    public override void OnPointerClick(PointerEventData eventData)
    {
        if (wax.activeSelf)
        {
            // Display the message using the Notification Manager
            NotificationManager manager = FindObjectOfType<NotificationManager>();
            message = "The key appears to be stuck!";
            if (manager != null)
            {
                if (!manager.GetComponentInChildren<Text>())
                    manager.ShowNotification(message, gameObject.transform.position);
            }
        }
        else
        {
            base.OnPointerClick(eventData);
        }
    }
}
