using UnityEngine;
using System.Collections;

public class ButtonManager : MonoBehaviour
{

    public GameObject copy;
    public GameObject selObject;
    private Quaternion rotate;
    public GameObject itemint;
    private bool[] gaze = new bool[4];
    private bool select;
    void Start()
    {
        rotate = copy.transform.rotation;
        gaze[0] = false;
        gaze[1] = false;
        gaze[2] = false;
        gaze[3] = false;
        select = false;
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

        if (transform.position == new Vector3(98.651f, 1.257f, 0.507f))
        {
            itemint.SetActive(true);
        }
        else
        {
            itemint.SetActive(false);
        }
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

    public void PointerClick()
    {
        iTween.MoveTo(selObject, iTween.Hash("position", new Vector3(98.651f, 1.257f, 0.507f), "time", 1f));
    }

    public void onExitButton()
    {
        itemint.SetActive(false);
        copy.transform.rotation = rotate;
        iTween.MoveTo(selObject, iTween.Hash("position", new Vector3(102.85f, 1.65f, 3.48f), "time", 1f));
    }

}
