using UnityEngine;
using System.Collections;

public class ButtonManager : MonoBehaviour
{

    public GameObject copy;
    public GameObject itemint;
    private bool[] gaze = new bool[4];
    void Start()
    {
        gaze[0] = false;
        gaze[1] = false;
        gaze[2] = false;
        gaze[3] = false;
    }
    void Update()
    {
        if (gaze[0])
           transform.Rotate(Vector3.right * (Time.deltaTime * 45));
        else if (gaze[1])
           transform.Rotate(Vector3.left * (Time.deltaTime * 45));
        else if(gaze[2])
           transform.Rotate(Vector3.down * (Time.deltaTime * 45));
        else if(gaze[3])
           transform.Rotate(Vector3.up * (Time.deltaTime * 45));
    }
    public void rotateUP()
    {
        transform.Rotate(Vector3.right * (Time.deltaTime * 2));
    }

    public void setGazedOn(int index)
    {
        gaze[index] = true;
    }
    public void setGazedOff(int index)
    {
        gaze[index] = false;
    }

    public void onExitButton()
    {
        itemint.SetActive(false);
    }
}
