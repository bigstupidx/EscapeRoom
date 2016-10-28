using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class ClockInteractionSmall : Focusable
{
    public GameObject small_pointer;
    // Use this for initialization
    void Start()
    {
        small_pointer = GameObject.Find("small_pointer");

    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        small_pointer.transform.Rotate(0, 0, 30);

        if (GameVariables.smallPointerTime == 12)
        {
            GameVariables.smallPointerTime = 1;
        }
        else if (GameVariables.smallPointerTime == 1)
        {
            GameVariables.smallPointerTime = 2;
        }
        else if (GameVariables.smallPointerTime == 2)
        {
            GameVariables.smallPointerTime = 3;
        }
        else if (GameVariables.smallPointerTime == 3)
        {
            GameVariables.smallPointerTime = 4;
        }
        else if (GameVariables.smallPointerTime == 4)
        {
            GameVariables.smallPointerTime = 5;
        }
        else if (GameVariables.smallPointerTime == 5)
        {
            GameVariables.smallPointerTime = 6;
        }
        else if (GameVariables.smallPointerTime == 6)
        {
            GameVariables.smallPointerTime = 7;
        }
        else if (GameVariables.smallPointerTime == 7)
        {
            GameVariables.smallPointerTime = 8;
        }
        else if (GameVariables.smallPointerTime == 8)
        {
            GameVariables.smallPointerTime = 9;
        }
        else if (GameVariables.smallPointerTime == 9)
        {
            GameVariables.smallPointerTime = 10;
        }
        else if (GameVariables.smallPointerTime == 10)
        {
            GameVariables.smallPointerTime = 11;
        }
        else if (GameVariables.smallPointerTime == 11)
        {
            GameVariables.smallPointerTime = 12;
        }
    }
}
