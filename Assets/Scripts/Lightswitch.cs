using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Lightswitch : Openable {
	public Light[] onLights;
	public Light[] offLights;

    public Texture2D onLightmap;
    public Texture2D onLightDirection;
    public Texture2D offLightmap;
    public Texture2D offLightDirection;

    private LightmapData[] onLightMapData;
    private LightmapData[] offLightMapData;

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

        onLightMapData = new LightmapData[1];
        onLightMapData[0] = new LightmapData();
        onLightMapData[0].lightmapFar = onLightmap;
        onLightMapData[0].lightmapNear = onLightDirection;

        offLightMapData = new LightmapData[1];
        offLightMapData[0] = new LightmapData();
        offLightMapData[0].lightmapFar = offLightmap;
        offLightMapData[0].lightmapNear = offLightDirection;

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

        LightmapSettings.lightmaps = offLightMapData;

		if (deskLamp.isOn) {
			mapProjector.material.color = Color.white;
		}
	}

	void Lightswitch_Closed ()
	{
		foreach (Light light in offLights) {
			light.enabled = false;
		}

        LightmapSettings.lightmaps = onLightMapData;

        foreach (Light light in onLights) {
			light.enabled = true;
		}

		mapProjector.material.color = deskLamp.projectorLightOnColor;
	}
}
