using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class OpenCloseClockDoor : Lockable {
	public override bool CanUnlock()
	{
		return GameVariables.bigPointerTime == 6 && GameVariables.smallPointerTime == 3;
	}
}
