using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Television : Searchable
{
	public GameObject key;

	Television()
	{
		message = "You found the key!";
	}

	public override void OnPointerClick(UnityEngine.EventSystems.PointerEventData eventData)
	{
		base.OnPointerClick(eventData);

		key.SetActive(true);
	}
}
