using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class ClockInteraction : Focusable
{
    public GameObject big_pointer;
	// Use this for initialization
	void Start () {
        big_pointer = GameObject.Find("big_pointer");

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public override void OnPointerClick(PointerEventData eventData)
    {
        big_pointer.transform.Rotate(0, 0, 30);

        if (GameVariables.bigPointerTime == 12)
        {
            GameVariables.bigPointerTime = 1;
        }
        else if (GameVariables.bigPointerTime == 1)
        {
            GameVariables.bigPointerTime = 2;
        }
        else if (GameVariables.bigPointerTime == 2)
        {
            GameVariables.bigPointerTime = 3;
        }
        else if (GameVariables.bigPointerTime == 3)
        {
            GameVariables.bigPointerTime = 4;
        }
        else if (GameVariables.bigPointerTime == 4)
        {
            GameVariables.bigPointerTime = 5;
        }
        else if (GameVariables.bigPointerTime == 5)
        {
            GameVariables.bigPointerTime = 6;
        }
        else if (GameVariables.bigPointerTime == 6)
        {
            GameVariables.bigPointerTime = 7;
        }
        else if (GameVariables.bigPointerTime == 7)
        {
            GameVariables.bigPointerTime = 8;
        }
        else if (GameVariables.bigPointerTime == 8)
        {
            GameVariables.bigPointerTime = 9;
        }
        else if (GameVariables.bigPointerTime == 9)
        {
            GameVariables.bigPointerTime = 10;
        }
        else if (GameVariables.bigPointerTime == 10)
        {
            GameVariables.bigPointerTime = 11;
        }
        else if (GameVariables.bigPointerTime == 11)
        {
            GameVariables.bigPointerTime = 12;
        }

    }


}

