using UnityEngine;
using System.Collections;

public class LockedByPickup : Lockable {

	public Pickup requiredToUnlock = null;

	public override bool CanUnlock()
	{
		return requiredToUnlock.IsFound;
	}
}
