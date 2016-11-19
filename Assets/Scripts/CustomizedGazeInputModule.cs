
using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class CustomizedGazeInputModule : GazeInputModule
{
	public LayerMask SelectionMask = -1;

	public override void Process()
	{
		// Only call base class Process function if the script is enabled.
		// This allows us to disable generation of new input events during playback of recordings
		if (this.enabled) {
			base.Process();
		}
	}

	private List<RaycastResult> m_validRaycastCache = new List<RaycastResult>();

	protected override void CastRayFromGaze() {
		Vector2 headPose = NormalizedCartesianToSpherical(GvrViewer.Instance.HeadPose.Orientation * Vector3.forward);

		if (pointerData == null) {
			pointerData = new PointerEventData(eventSystem);
			lastHeadPose = headPose;
		}

		// Cast a ray into the scene
		pointerData.Reset();
		pointerData.position = new Vector2(0.5f * Screen.width, 0.5f * Screen.height);
		eventSystem.RaycastAll(pointerData, m_RaycastResultCache);


		// Filter the result through the Selection Layer Mask
		foreach (RaycastResult result in m_RaycastResultCache) {
			if (result.gameObject != null) {
				bool hasMouseInterface = (result.gameObject.GetComponents<IPointerClickHandler>().Length > 0 ||
				                         result.gameObject.GetComponents<IPointerEnterHandler>().Length > 0 ||
				                         result.gameObject.GetComponents<IPointerExitHandler>().Length > 0 ||
										 result.gameObject.GetComponents<EventTrigger>().Length > 0);

				if (hasMouseInterface && ((SelectionMask & 1 << result.gameObject.layer) != 0)) {
					m_validRaycastCache.Add(result);
				}
			}
		}

		pointerData.pointerCurrentRaycast = FindFirstRaycast(m_validRaycastCache);
		m_RaycastResultCache.Clear();
		m_validRaycastCache.Clear();

		pointerData.delta = headPose - lastHeadPose;
		lastHeadPose = headPose;
	}

	public void DisableSelectionLayer(Layers layer) {
		SelectionMask.value &= ~(1 << (int)layer);
	}

	public void EnableSelectionLayer(Layers layer) {
		SelectionMask.value |= (1 << (int)layer);
	}
}

