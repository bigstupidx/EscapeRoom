using UnityEngine;
using UnityStandardAssets.ImageEffects;


[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
[AddComponentMenu("Image Effects/Blur/Blur (Optimized)")]
public class SelectionGlow : PostEffectsBase
{

	[Range(0, 2)]
	public int downsample = 2;

	public enum BlurType
	{
		StandardGauss = 0,
		SgxGauss = 1,
	}

	[Range(0.0f, 10.0f)]
	public float blurSize = 3.0f;

	[Range(1, 4)]
	public int blurIterations = 2;

	public BlurType blurType = BlurType.StandardGauss;

	public Shader solidColorShader = null;

	public Shader blurShader = null;
	public Shader addBrightStuffOneOneShader;

	public float intensity = 0.5f;

	private Camera tempCam = null;
	private Camera attachedCam = null;

	private Material blurMaterial = null;
	private Material addBrightStuffBlendOneOneMaterial = null;

	public override bool CheckResources()
	{
		CheckSupport(false);

		blurMaterial = CheckShaderAndCreateMaterial(blurShader, blurMaterial);
		addBrightStuffBlendOneOneMaterial = CheckShaderAndCreateMaterial(addBrightStuffOneOneShader, addBrightStuffBlendOneOneMaterial);

		if (!isSupported)
			ReportAutoDisable();
		return isSupported;
	}

	public void OnDisable()
	{
		if (blurMaterial)
			DestroyImmediate(blurMaterial);
	}

	public void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		if (CheckResources() == false) {
			Graphics.Blit(source, destination);
			return;
		}

		// Render solid color version of glowing objects using the same depth buffer used for the scene
		RenderTexture glowColor = RenderTexture.GetTemporary(source.width, source.height, 0, source.format);


		if (attachedCam == null || tempCam == null) {
			attachedCam = GetComponent<Camera>();
			GameObject tempCampObj = new GameObject("_GlowEffectTemp", typeof(Camera));
			tempCam = tempCampObj.GetComponent<Camera>();
			tempCam.enabled = false;
		}
			
		tempCam.CopyFrom(attachedCam);
		tempCam.SetTargetBuffers(glowColor.colorBuffer, source.depthBuffer);
		tempCam.backgroundColor = Color.black;
		tempCam.clearFlags = CameraClearFlags.Color;
		tempCam.cullingMask = 1 << 8;
		tempCam.RenderWithShader(solidColorShader, "");

		//Graphics.Blit(glowColor, destination);

		float widthMod = 1.0f / (1.0f * (1 << downsample));
	
		blurMaterial.SetVector("_Parameter", new Vector4(blurSize * widthMod, -blurSize * widthMod, 0.0f, 0.0f));
		glowColor.filterMode = FilterMode.Bilinear;

		int rtW = glowColor.width >> downsample;
		int rtH = glowColor.height >> downsample;

		// downsample
		RenderTexture rt = RenderTexture.GetTemporary(rtW, rtH, 0, glowColor.format);

		rt.filterMode = FilterMode.Bilinear;
		Graphics.Blit(glowColor, rt, blurMaterial, 0);

		var passOffs = blurType == BlurType.StandardGauss ? 0 : 2;

		for (int i = 0; i < blurIterations; i++) {
			float iterationOffs = (i * 1.0f);
			blurMaterial.SetVector("_Parameter", new Vector4(blurSize * widthMod + iterationOffs, -blurSize * widthMod - iterationOffs, 0.0f, 0.0f));

			// vertical blur
			RenderTexture rt2 = RenderTexture.GetTemporary(rtW, rtH, 0, glowColor.format);
			rt2.filterMode = FilterMode.Bilinear;
			Graphics.Blit(rt, rt2, blurMaterial, 1 + passOffs);
			RenderTexture.ReleaseTemporary(rt);
			rt = rt2;

			// horizontal blur
			rt2 = RenderTexture.GetTemporary(rtW, rtH, 0, glowColor.format);
			rt2.filterMode = FilterMode.Bilinear;
			Graphics.Blit(rt, rt2, blurMaterial, 2 + passOffs);
			RenderTexture.ReleaseTemporary(rt);
			rt = rt2;
		}

		addBrightStuffBlendOneOneMaterial.SetFloat("_Intensity", intensity);

		Graphics.Blit(source, destination);

		Graphics.Blit(rt, destination, addBrightStuffBlendOneOneMaterial);

		RenderTexture.ReleaseTemporary(rt);
		RenderTexture.ReleaseTemporary(glowColor);
	}
}