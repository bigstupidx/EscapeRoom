using UnityEngine;
using System.Collections;
using System;

public class VRInteractiveItem : MonoBehaviour {

    public event Action OnOver;             // Called when the gaze moves over this object
    public event Action OnOut;              // Called when the gaze leaves this object
    public event Action OnClick;            // Called when click input is detected whilst the gaze is over this object.
    public event Action OnDoubleClick;      // Called when double click input is detected whilst the gaze is over this object.
    public event Action OnUp;               // Called when Fire1 is released whilst the gaze is over this object.
    public event Action OnDown;             // Called when Fire1 is pressed whilst the gaze is over this object.
}
