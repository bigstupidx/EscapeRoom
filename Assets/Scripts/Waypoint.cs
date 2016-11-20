using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class Waypoint : Focusable, IPointerClickHandler
{
	public override void OnPointerClick(PointerEventData eventData)
    {
		Camera.main.transform.position = new Vector3(transform.position.x, Camera.main.transform.position.y, transform.position.z);
    }
}
