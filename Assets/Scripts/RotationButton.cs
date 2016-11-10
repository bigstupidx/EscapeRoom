using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class RotationButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

	public bool isPressed;

	public void OnPointerEnter(PointerEventData eventData)
	{
		isPressed = true;
	}
		
	public void OnPointerExit(PointerEventData eventData)
	{
		isPressed = false;
	}
}
