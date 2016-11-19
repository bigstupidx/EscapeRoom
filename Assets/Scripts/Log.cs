using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class Log : Searchable {
    public GameObject campfire;
    public GameObject matchbox;
    public Log()
    {
        message = "The log lights on fire!";
    }

	void Start () {
        campfire.SetActive(false);
	}

    public override void OnPointerClick(PointerEventData eventData)
    {
        if (!campfire.activeSelf)
        {
            if (matchbox.GetComponent<Pickup>().IsFound) {
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
}
