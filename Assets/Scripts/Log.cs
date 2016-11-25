using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Log : Searchable
{
    public GameObject campfire;
    public GameObject matchbox;
    public GameObject Pos1;
    public GameObject iceCube;
    public GameObject key;
    public GameObject block;
    private int remaining = 5;
    private bool complete = false;
    public bool IsFound = false;
    public Log()
    {
        message = "The log lights on fire!";
    }

    void Start()
    {
        campfire.SetActive(false);
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        if (IsFound)
        {
            if (!campfire.activeSelf)
            {
                if (matchbox.GetComponent<Pickup>().IsFound)
                {
                    base.OnPointerClick(eventData);
                    campfire.SetActive(true);
                    foreach (ParticleSystem ps in campfire.GetComponentsInChildren<ParticleSystem>())
                    {
                        ps.Play();
                    }
                }
                else
                {
                    message = "How did this come from a chair???";
                    base.OnPointerClick(eventData);
                }
            }
            else if (block.GetComponent<Pickup>().IsFound)
            {
                if (block.activeSelf && !complete)
                {
                    block.transform.position = new Vector3(4.65f, .685f, -1.39f);
                    InvokeRepeating("countDown", 1, 1);
                }
            }
            else
            {
                message = "That fire looks a little too hot to touch";
                base.OnPointerClick(eventData);
            }
        }
        else
        {
            IsFound = true;
            message = "You add the log to the oven.";
            base.OnPointerClick(eventData);
            GoalMover mover = GetComponent<GoalMover>();
            mover.ClearGoals();
            mover.AddGoal(Pos1.transform.position, Pos1.transform.rotation);
        }
    }

    public void countDown()
    {
        complete = true;
        remaining--;
        // Display the message using the Notification Manager
        NotificationManager manager = FindObjectOfType<NotificationManager>();
        message = "Melting, please wait!";
        if (manager != null)
        {
            if (!manager.GetComponentInChildren<Text>())
                manager.ShowNotification(message, iceCube.transform.position);
        }
        if (remaining <= 0)
        {
            iceCube.SetActive(false);
            key.transform.position = new Vector3(3.6619f, .0773f, -1.543446f);
            CancelInvoke("countDown");
        }
    }
}
