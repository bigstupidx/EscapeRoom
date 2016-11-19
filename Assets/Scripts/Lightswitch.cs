using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Lightswitch : Openable {
	public Light[] onLights;
	public Light[] offLights;


	public Projector mapProjector;

	public Desklamp deskLamp;

	public override void Start()
	{
		base.Start();

		foreach (Light light in offLights) {
			light.enabled = false;
		}

		foreach (Light light in onLights) {
			light.enabled = true;
		}

		Opened += Lightswitch_Opened;
		Closed += Lightswitch_Closed;
	}

	void Lightswitch_Opened ()
	{
		foreach (Light light in offLights) {
			light.enabled = true;
		}

		foreach (Light light in onLights) {
			light.enabled = false;
		}

		if (deskLamp.isOn) {
			mapProjector.material.color = Color.white;
		}
	}

	void Lightswitch_Closed ()
	{
		foreach (Light light in offLights) {
			light.enabled = false;
		}

		foreach (Light light in onLights) {
			light.enabled = true;
		}

		mapProjector.material.color = deskLamp.projectorLightOnColor;
	}
}
