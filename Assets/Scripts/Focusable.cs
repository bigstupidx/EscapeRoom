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

	private Dictionary<GameObject, int> oldLayers = new Dictionary<GameObject, int>();

	public virtual void OnPointerEnter(PointerEventData eventData)
	{
		foreach (Renderer renderer in GetComponentsInChildren<Renderer>(true)) {
			GameObject go = renderer.gameObject;

			if (go.layer != 8) {
				oldLayers[go] = go.layer;
				go.layer = 8;
			}
		}
	}

	public virtual void OnPointerExit(PointerEventData eventData)
	{
		foreach (Renderer renderer in GetComponentsInChildren<Renderer>(true)) {
			GameObject go = renderer.gameObject;

			int oldLayer = 0;

			oldLayers.TryGetValue(go, out oldLayer);
			go.layer = oldLayer;
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
