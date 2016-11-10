using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;

[RequireComponent(typeof(Collider))]
public class Focusable : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{

	// Use this for initialization
	void Start()
	{
	
	}
	
	// Update is called once per frame
	void Update()
	{
	
	}

	public virtual void OnPointerClick(PointerEventData eventData)
	{
		// Do nothing in base implementation
	}

	private Dictionary<Material, Shader> oldShaders = new Dictionary<Material, Shader>();

	private Dictionary<SpriteRenderer, Color> oldColors = new Dictionary<SpriteRenderer, Color>();

	public virtual void OnPointerEnter(PointerEventData eventData)
	{
		foreach (Renderer renderer in GetComponentsInChildren<Renderer>(true)) {
			if (renderer is SpriteRenderer) {
				SpriteRenderer spriteRenderer = (SpriteRenderer)renderer;
				// Remember the previous color
				oldColors[spriteRenderer] = spriteRenderer.color;
				spriteRenderer.color = new Color(1.0f, 0.7f, 0);
			} else if (renderer is MeshRenderer) {
				foreach (Material material in renderer.materials) {
					float mode = 0.0f;

					if (material.HasProperty("_Mode")) {
						mode = material.GetFloat("_Mode");
					}

					if (mode == 0.0f) {
						// Remember the previous shader 
						if (material.shader.name != "Outlined/Diffuse") {
							oldShaders[material] = material.shader;
							material.shader = Shader.Find("Outlined/Diffuse");
						}
					}
				}
			}
		}
	}

	public virtual void OnPointerExit(PointerEventData eventData)
	{
		foreach (Renderer renderer in GetComponentsInChildren<Renderer>(true)) {
			if (renderer is SpriteRenderer) {
				SpriteRenderer spriteRenderer = (SpriteRenderer)renderer;

				// Restore original color
				Color originalColor;

				if (oldColors.TryGetValue(spriteRenderer, out originalColor)) {
					spriteRenderer.color = originalColor;
				}
			} else if (renderer is MeshRenderer) {
				foreach (Material material in renderer.materials) {
					Shader originalShader = null;

					oldShaders.TryGetValue(material, out originalShader);
					if (originalShader != null) {
						material.shader = originalShader;
					}
				}
			}
		}
	}

	protected void DisplayMessage(string text)
	{
		// Do nothing if message is null or empty
		if (string.IsNullOrEmpty(text)) {
			return;
		}

		// Display the message using the Notification Manager
		NotificationManager manager = FindObjectOfType<NotificationManager>();

		// Get collider component
		Collider collider = GetComponent<Collider>();

		if (manager != null) {
			manager.ShowNotification(text, collider.bounds);
		}
	}
}
