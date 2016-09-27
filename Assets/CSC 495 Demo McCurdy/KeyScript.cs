using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class KeyScript : MonoBehaviour {
    
    public GameObject key;
    public void Start()
    {
        key.SetActive(false);
    }

    public void setKeyActive()
    {
            key.SetActive(true);
    }


}
