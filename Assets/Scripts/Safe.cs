using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Safe : Searchable
{
	public GameObject keyPad;

	Safe()
	{
		message = "";
	}

    void Start()
    {
        keyPad.SetActive(false);
    }

	public override void OnPointerClick(UnityEngine.EventSystems.PointerEventData eventData)
	{
		base.OnPointerClick(eventData);
        message = "You found a safe!";
		keyPad.SetActive(true);
	}
}
