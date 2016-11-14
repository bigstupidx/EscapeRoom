using UnityEngine;
using System.Collections;

// An FPS counter.
// It calculates frames/second over each updateInterval,
// so the display does not keep changing wildly.
public class Statistics : MonoBehaviour
{
    public float updateInterval = 0.5F;
    private double lastInterval;
    private int frames = 0;
    private static int TotalFrames = 0;
    private static float StartTime = 0;
    private static float FinishTime = 0;
    public static float FPS { get; private set; }
    public static float AvgFPS { get; private set; }

    void Start()
    {
        lastInterval = Time.realtimeSinceStartup;
        frames = 0;
        StartTime = Time.realtimeSinceStartup;
        print(StartTime);
    }
    void OnGUI()
    {
        GUILayout.Label(string.Format("FPS: {0:0} Avg FPS: {1:0}", FPS, AvgFPS));
    }
    void Update()
    {
        ++frames;
        ++TotalFrames;
        float timeNow = Time.realtimeSinceStartup;
        if (timeNow > lastInterval + updateInterval) {
            FPS = (float)(frames / (timeNow - lastInterval));
            frames = 0;
            lastInterval = timeNow;
        }

        AvgFPS = Time.frameCount / Time.time;
        FinishTime = Time.time;
    }

    public float getAvg()
    {
        return AvgFPS;
    }

    public float getFrames()
    {
        return TotalFrames;
    }

    public float getTotalTime()
    {
        print(Time.realtimeSinceStartup);
        return Time.realtimeSinceStartup - StartTime;
    }
}