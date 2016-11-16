using UnityEngine;
using UnityStandardAssets.ImageEffects;
using System.Collections.Generic;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class RenderUIOverlay : PostEffectsBase
{
	private Camera tempCam = null;
	private Camera attachedCam = null;

	private Material addBrightStuffBlendOneOneMaterial = null;

	public override bool CheckResources()
	{
		CheckSupport(false);

		if (!isSupported) {
			ReportAutoDisable();
		}
		return isSupported;
	}

	public void OnDisable()
	{

	}

	public void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		if (CheckResources() == false) {
			Graphics.Blit(source, destination);
			return;
		}

		// Clear the depth buffer of the source image so that the UI will render as if there is nothing else in the scene
		RenderTexture.active = source;
		GL.Clear(true, false, Color.black);

		if (attachedCam == null || tempCam == null) {
			attachedCam = GetComponent<Camera>();
			GameObject tempCampObj = new GameObject("_UIOverlayTemp", typeof(Camera));
			tempCam = tempCampObj.GetComponent<Camera>();
			tempCam.enabled = false;
		}
			
		tempCam.CopyFrom(attachedCam);
		tempCam.SetTargetBuffers(source.colorBuffer, source.depthBuffer);
		tempCam.backgroundColor = Color.black;
		tempCam.clearFlags = CameraClearFlags.Nothing;
		tempCam.cullingMask = 1 << 5; // Only render UI layer
		tempCam.Render();

		// Blit source to result
		Graphics.Blit(source, destination);
	}
}