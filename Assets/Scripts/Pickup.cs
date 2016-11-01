using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class Pickup : Searchable {
    
	public bool IsFound = false;

	public GameObject InventoryPosition = null;

    public Pickup() {
		message = "You found ";
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);
		IsFound = true;

		NotificationManager manager = FindObjectOfType<NotificationManager>();

		Vector3 cameraPos = manager.faceCamera.transform.position;

		Vector3 cameraGoal = new Vector3(cameraPos.x, cameraPos.y, cameraPos.z + 1);

//1		Animation anim = gameObject.AddComponent<Animation>();
//
//		AnimationClip toCameraClip = new AnimationClip();
//
//		toCameraClip.SetCurve("", typeof(Transform), "position.x", AnimationCurve.EaseInOut(0, 0, 2, 10));
//		toCameraClip.SetCurve("", typeof(Transform), "position.y", AnimationCurve.EaseInOut(0, 10, 2, 0));
//		toCameraClip.SetCurve("", typeof(Transform), "position.z", AnimationCurve.EaseInOut(0, 5, 2, 2));
//
//		anim.AddClip(toCameraClip);
//
//		AnimationClip vanishClip = new AnimationClip();
//

		if (InventoryPosition == null) {
			// No inventory position, just make the object dissappear
			this.gameObject.SetActive(false);
		} else {
			// Move the object to be a child of the inventory position object
			this.transform.parent = InventoryPosition.transform;
		}
    }
}
