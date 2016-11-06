using UnityEngine;
using System.Collections;

public class Lockable : Openable {

	public bool isLocked = true;

	public string lockedMessage = "It's locked";

	public string unlockedMessage = "You unlocked the...";

	public override void OnPointerClick(UnityEngine.EventSystems.PointerEventData eventData)
	{
		if (isLocked) {
			// Try to unlock it
			if (!CanUnlock()) {
				DisplayMessage(lockedMessage);
			} else {
				isLocked = false;

				DisplayMessage(unlockedMessage);
			}
		}

		// If it is unlocked, invoke the base class behavior which will open/close it
		if (!isLocked) {
			base.OnPointerClick(eventData);
		}
	}

	// Override this to implement a custom unlock test.
	public virtual bool CanUnlock() {
		return false;
	}
}
