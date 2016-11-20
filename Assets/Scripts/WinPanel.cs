using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WinPanel : MonoBehaviour {
    public Text text;
    private float frames;
    private float AvgFrames;
    private float time;
    void Start()
    {
        
        string message = "You have Won!\n";
        message += string.Format("Total Frames: {0:0.00}\n", Statistics.TotalFrames);
        message += string.Format("Average Frames: {0:0.00}\n", Statistics.AvgFPS);
        message += string.Format("Dropped Frames: {0:0.00}\n", Statistics.DroppedFrames);
        message += string.Format("Total Time: {0:0.00}\n", Statistics.TotalTime);
        message += "Would you like to save your replay?";
        text.text = message;
    }
}
