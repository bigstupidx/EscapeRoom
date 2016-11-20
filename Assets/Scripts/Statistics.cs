using UnityEngine;
using System.Collections;

// An FPS counter.
// It calculates frames/second over each updateInterval,
// so the display does not keep changing wildly.
public class Statistics : MonoBehaviour
{
	public static int TotalFrames { get; private set; }
	public static float StartTime { get; private set; }
    public static float FPS { get; private set; }
    public static float AvgFPS { get; private set; }
	public static float TotalTime { get; private set; }
	public static float DroppedFrames { get; private set; }

	public static void StartTiming() {
		TotalFrames = 0;
		AvgFPS = 0;
		StartTime = Time.realtimeSinceStartup;

		isTiming = true;
	}

	private static bool isTiming = false;
   
	void Update()
    {
		if (isTiming) {
			++TotalFrames;
		}
    }

    public static void StopTiming()
    {
		isTiming = false;

        TotalTime = Time.realtimeSinceStartup - StartTime;
        AvgFPS = TotalFrames / TotalTime;
        DroppedFrames = TotalTime * 60 - TotalFrames;
    }

}