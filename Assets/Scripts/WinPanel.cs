using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WinPanel : MonoBehaviour {
    private Statistics stats = new Statistics();
    public Text text;
    private float frames;
    private float AvgFrames;
    private float time;
    void Start()
    {
        frames = stats.getFrames();
        AvgFrames = stats.getAvg();
        time = stats.getTotalTime();
        string message = "You have Won!\n";
        message += "Your Total Frames were " + frames + ".\n";
        message += "Your Avg Frames were " + AvgFrames + ".\n";
        message += "Your total time is " + time + "seconds.\n";
        message += "Now would you like to save your replay?";
        text.text = message;
    }
}
