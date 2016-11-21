using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NoRecording : MonoBehaviour
{
    public Text text;

    void Start()
    {
        string message = "No recording file found on this save slot. Please choose a save slot with a recording saved.";
        text.text = message;
    }
}

