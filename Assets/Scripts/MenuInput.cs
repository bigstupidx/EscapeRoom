using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuInput : MonoBehaviour {

    private int remaining = 2;
    void Start()
    {
        SetGazedAt(false);
    }

    public void countDown()
    {
        remaining--;
        if (remaining <= 0)
        {
            CancelInvoke("countDown");
            remaining = 2;
            GetComponent<Button>().onClick.Invoke();
        }
    }

    public void SetGazedAt(bool gazedAt)
    {
        if (gazedAt)
            InvokeRepeating("countDown", 1, 1);
        else
        {
            CancelInvoke("countDown");
            remaining = 2;
            print("reset");
        }
    }
}
