using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Safe : Lockable
{
	public UnlockSafe keyPad;


	public override void OnPointerClick(UnityEngine.EventSystems.PointerEventData eventData)
	{
		base.OnPointerClick(eventData);
		if (isLocked) {
			Vector3 safePos = transform.position;
			Vector3 camPos = Camera.main.transform.position;
			keyPad.Show(new Vector3(safePos.x, camPos.y, safePos.z));
		}
	}
}
