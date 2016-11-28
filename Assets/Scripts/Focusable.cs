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

	public virtual void OnPointerEnter(PointerEventData eventData)
	{
		SelectionGlow.ClearGlowingObjects();

		foreach (Renderer renderer in GetComponentsInChildren<Renderer>(true)) {
			SelectionGlow.AddGlowingObject(renderer.gameObject);
		}
	}

	public virtual void OnPointerExit(PointerEventData eventData)
	{
		SelectionGlow.ClearGlowingObjects();
	}

	public void DisplayMessage(string text)
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

	public void RotateToFaceCamera() {
		Util.RotateToFaceCamera(transform);
	}
}
