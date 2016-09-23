
using UnityEngine;
using UnityEngine.EventSystems;

public class CustomizedGazeInputModule : GazeInputModule
{
	public override void Process()
	{
		// Only call base class Process function if the script is enabled.
		// This allows us to disable generation of new input events during playback of recordings
		if (this.enabled) {
			base.Process();
		}
	}
}

