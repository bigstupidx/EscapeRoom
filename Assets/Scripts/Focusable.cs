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

	public virtual void OnPointerEnter(PointerEventData eventData)
	{
		foreach (Renderer renderer in GetComponentsInChildren<Renderer>(true)) {
			foreach (Material material in renderer.materials) {
				float mode = 0.0f;

				if (material.HasProperty("_Mode")) {
					mode = material.GetFloat("_Mode");
				}
					
				if (mode == 0.0f) {
					// Remember the previous shader
					if (material.shader.name != "Outlined/Diffuse") {
						oldShaders[material] = material.shader;
					}

					material.shader = Shader.Find("Outlined/Diffuse");
				}
			}
		}
	}

	public virtual void OnPointerExit(PointerEventData eventData)
	{
		foreach (Renderer renderer in GetComponentsInChildren<Renderer>(true)) {
			foreach (Material material in renderer.materials) {
				Shader original = null;

				oldShaders.TryGetValue(material, out original);

				if (original == null || original.name == "Sprites/Default") {
					original = Shader.Find("Standard");
				}

				material.shader = original;
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
