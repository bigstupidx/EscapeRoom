using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Television : Searchable
{
	public GameObject key;

	Television()
	{
		message = "";
	}

    void Start()
    {
        key.SetActive(false);
    }

	public override void OnPointerClick(UnityEngine.EventSystems.PointerEventData eventData)
	{
		base.OnPointerClick(eventData);
        message = "You found the key!";
        key.SetActive(true);
	}
}
