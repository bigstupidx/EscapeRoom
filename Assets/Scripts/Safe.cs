using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Safe : Searchable
{
	public UnlockSafe keyPad;
	public GoalMover safeLid;


	public override void OnPointerClick(UnityEngine.EventSystems.PointerEventData eventData)
	{
		base.OnPointerClick(eventData);
		message = "You found a safe!";
		Vector3 safePos = transform.position;
		Vector3 camPos = Camera.main.transform.position;
		keyPad.Show(new Vector3(safePos.x, camPos.y, safePos.z));
		safeLid.ClearGoals();
	}
}
