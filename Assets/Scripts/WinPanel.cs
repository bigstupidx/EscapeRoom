using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WinPanel : MonoBehaviour {
    private Statistics stats = new Statistics();
    public Text text;
    private float frames;
    private float AvgFrames;
    void Start()
    {
        frames = stats.getFrames();
        AvgFrames = stats.getAvg();
        string message = "You have Won!\n\n";
        message += "Your Total Frames were " + frames + ".\n";
        message += "Your Avg Frames were " + AvgFrames + ".\n";
        message += "Now would you like to save your replay?";
        text.text = message;
    }
}
