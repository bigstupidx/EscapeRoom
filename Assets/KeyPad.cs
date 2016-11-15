using UnityEngine;
using System.Collections;

public class KeyPad : Searchable {
	public GameObject key;

	KeyPad()
	{
		message = "";
	}

	void Start()
	{
	}

	void Update() {
		message = UnlockSafe.codeEntry;
	}

//	public override void OnPointerClick(UnityEngine.EventSystems.PointerEventData eventData)
//	{
//		base.OnPointerClick(eventData);
//		message = "You found the key!";
//		key.SetActive(true);
//	}
}

