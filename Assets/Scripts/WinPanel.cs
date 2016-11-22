using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Text;

public class WinPanel : MonoBehaviour {
    public Text text;
    private float frames;
    private float AvgFrames;
    private float time;
    void Start()
    {
        
		StringBuilder builder = new StringBuilder();
		builder.AppendLine(string.Format("Average FPS: {0:0.00}", Statistics.AvgFPS));
		builder.AppendLine(string.Format("Total Frames: {0}", Statistics.TotalFrames));
		builder.AppendLine(string.Format("Dropped Frames: {0}", Statistics.DroppedFrames));
		builder.Append(string.Format("Total Time: {0}:{1:00.0}", Math.Floor(Statistics.TotalTime / 60.0), Statistics.TotalTime % 60));
		text.text = builder.ToString();
    }
}
