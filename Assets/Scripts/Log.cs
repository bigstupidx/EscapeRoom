using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class Log : Searchable
{
    public GameObject campfire;
    public GameObject matchbox;
    public GameObject Pos1;
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
}
