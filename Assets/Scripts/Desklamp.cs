using UnityEngine;
using System.Collections;

public class Desklamp : Focusable {

	public Light spotLight;
	public Projector mapProjector;
	public Lightswitch lightSwitch;

	public Color projectorLightOnColor;

	public bool isOn = false;

	public override void OnPointerClick(UnityEngine.EventSystems.PointerEventData eventData)
	{
		isOn = !isOn;

		if (isOn) {
			spotLight.enabled = true;
			if (lightSwitch.IsOpen) {
				mapProjector.material.color = Color.white;
			} else {
				mapProjector.material.color = projectorLightOnColor;
			}
			mapProjector.enabled = true;
		} else {
			spotLight.enabled = false;
			mapProjector.enabled = false;
		}
	}
}
