using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class KeySelection : MonoBehaviour {

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
            var pointer = new PointerEventData(EventSystem.current);
            ExecuteEvents.Execute(GetComponent<Renderer>().gameObject, pointer, ExecuteEvents.pointerClickHandler);
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

    public void winGame()
    {
        SceneManager.LoadScene("WinScene");
    }
}
