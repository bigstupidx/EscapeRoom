using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class Ice : Focusable
{

    public GameObject iceCube;
    private bool isClicked;
    // Use this for initialization
    void Start()
    {
        isClicked = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool isSelected()
    {
        return isClicked;
    }
    public override void OnPointerClick(PointerEventData eventData)
    {
        if (GetComponent<Renderer>().name == iceCube.name)
        {
            isClicked = true;
        }
    }
}
