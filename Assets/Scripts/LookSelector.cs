// Copyright 2014 Google Inc. All rights reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Collider))]
public class LookSelector : MonoBehaviour, IGvrGazeResponder {
  private Vector3 startingPosition;
    public GameObject selObject;
    private int remaining = 3;
    private Color objColor;
    public Targets targets;
    public KeyScript key;
    public Text notifications;
  void Start() {
        startingPosition = transform.localPosition;
        objColor = selObject.GetComponent<MeshRenderer>().material.color;
        targets = new Targets();
		SetGazedAt(false);
        notifications.text = "";
  }
  void LateUpdate() {
    GvrViewer.Instance.UpdateState();
    if (GvrViewer.Instance.BackButtonPressed) {
      Application.Quit();
    }
  }

  public void countDown()
    {
        remaining--;
        GetComponent<Renderer>().material.color = remaining%2 == 1 ? Color.magenta : Color.yellow;
        if (remaining <= 0)
        {
            CancelInvoke("countDown");
            remaining = 3;
            var pointer = new PointerEventData(EventSystem.current);
            ExecuteEvents.Execute(GetComponent<Renderer>().gameObject, pointer, ExecuteEvents.pointerClickHandler);
        }
    }
  public void SearchKey()
    {
        print(targets.getObject(selObject.name));
        if(targets.getObject(selObject.name) == 5)
        {
            key.setKeyActive();
            GameVariables.keyCount++;
            notifications.text = "You found the key!";
        } else
        {

            notifications.text = "Nothing Here.";
        }
    }
  public void SetGazedAt(bool gazedAt) {
    GetComponent<Renderer>().material.color = gazedAt ? Color.green : objColor;
        if (gazedAt)
            InvokeRepeating("countDown", 1, 1);
        else
        {
            CancelInvoke("countDown");
            remaining = 3;
            notifications.text = "";
        }
    }

  public void Reset() {
    transform.localPosition = startingPosition;
  }

  public void ToggleVRMode() {
    GvrViewer.Instance.VRModeEnabled = !GvrViewer.Instance.VRModeEnabled;
  }

  public void ToggleDistortionCorrection() {
    switch(GvrViewer.Instance.DistortionCorrection) {
    case GvrViewer.DistortionCorrectionMethod.Unity:
      GvrViewer.Instance.DistortionCorrection = GvrViewer.DistortionCorrectionMethod.Native;
      break;
    case GvrViewer.DistortionCorrectionMethod.Native:
      GvrViewer.Instance.DistortionCorrection = GvrViewer.DistortionCorrectionMethod.None;
      break;
    case GvrViewer.DistortionCorrectionMethod.None:
    default:
      GvrViewer.Instance.DistortionCorrection = GvrViewer.DistortionCorrectionMethod.Unity;
      break;
    }
  }

  public void ToggleDirectRender() {
    GvrViewer.Controller.directRender = !GvrViewer.Controller.directRender;
  }

  public void TeleportRandomly() {
    Vector3 direction = Random.onUnitSphere;
    direction.y = Mathf.Clamp(direction.y, 0.5f, 1f);
    float distance = 2 * Random.value + 1.5f;
    transform.localPosition = direction * distance;
  }

  #region IGvrGazeResponder implementation

  /// Called when the user is looking on a GameObject with this script,
  /// as long as it is set to an appropriate layer (see GvrGaze).
  public void OnGazeEnter() {
    SetGazedAt(true);
  }

  /// Called when the user stops looking on the GameObject, after OnGazeEnter
  /// was already called.
  public void OnGazeExit() {
    SetGazedAt(false);
  }

  /// Called when the viewer's trigger is used, between OnGazeEnter and OnGazeExit.
  public void OnGazeTrigger() {
    TeleportRandomly();
  }

  #endregion
}
