using UnityEngine;
using System.Collections;

public class Lightswitch : Openable {
	public Light light1;
	public Light light2;

	public Projector mapProjector;

	public Desklamp deskLamp;

	public Color offColor;

	private Color originalLight1Color;
	private Color originalLight2Color;

	public override void Start()
	{
		base.Start();

		originalLight1Color = light1.color;
		originalLight2Color = light2.color;

		Opened += Lightswitch_Opened;
		Closed += Lightswitch_Closed;
	}

	void Lightswitch_Opened ()
	{
		light1.color = offColor;
		light2.color = offColor;

		if (deskLamp.isOn) {
			mapProjector.material.color = Color.white;
		}
	}

	void Lightswitch_Closed ()
	{
		light1.color = originalLight1Color;
		light2.color = originalLight2Color;

		mapProjector.material.color = deskLamp.projectorLightOnColor;
	}
}
