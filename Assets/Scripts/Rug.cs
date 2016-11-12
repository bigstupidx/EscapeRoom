using UnityEngine;
using System.Collections;

public class Rug : Focusable {

	protected Animation anim;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animation> ();
	}

	public override void OnPointerClick(UnityEngine.EventSystems.PointerEventData eventData)
	{
		// Play the animation
		anim.Play();

		// Change layer of this object so it is no longer selectable
		gameObject.layer = 2;
	}
}
