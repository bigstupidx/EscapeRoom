using UnityEngine;
using System.Collections;

public class ButtonManager : MonoBehaviour {

    public GameObject copy;
    public void rotateUP()
    {
        copy.transform.rotation.Set(copy.transform.rotation.x, 90, copy.transform.rotation.z, copy.transform.rotation.w);
    }
}
