using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WarningPanel : MonoBehaviour
{
    public Text text;
   
    void Start()
    {
        string message = "It is not recommended that you watch recordings with the Google Cardboard headset on.";
        text.text = message;
    }
}

