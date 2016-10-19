using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class Focusable : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
		
	public virtual void OnPointerClick(PointerEventData eventData)
	{
		// Do nothing in base implementation
	}
		
	public virtual void OnPointerEnter(PointerEventData eventData)
	{
		foreach (Renderer renderer in GetComponentsInChildren<Renderer>(true)) {
			foreach (Material material in renderer.materials) {
				material.SetColor("_Color", new Color(0.2f, 0.2f, 0, 0));
				material.SetColor("_OutlineColor", new Color(0.4f, 0.4f, 0, 0));
				material.SetFloat("_Outline", 0.006f);
				material.shader = Shader.Find("Outlined/Diffuse");
			}
		}
	}

	public virtual void OnPointerExit(PointerEventData eventData)
	{
		foreach (Renderer renderer in GetComponentsInChildren<Renderer>(true)) {
			foreach (Material material in renderer.materials) {
				material.shader = Shader.Find("Standard");
			}
		}
	}
}
