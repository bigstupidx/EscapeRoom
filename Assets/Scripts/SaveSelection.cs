using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SaveSelection : MonoBehaviour {

    public GameObject savePanel;
    public GameObject winPanel;

    private int remaining = 2;
    void Start()
    {
        SetGazedAt(false);
        savePanel.SetActive(false);
    }
    public void saveGame(bool answer)
    {
        if (answer) {
            savePanel.SetActive(true);
            winPanel.SetActive(false);
        }
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
