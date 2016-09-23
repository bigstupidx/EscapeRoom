using UnityEngine;
using System.Collections;

public class ShowPickUp : MonoBehaviour {

    Rect rect;
    Texture texture;
	// Use this for initialization
	void Start () {
        float size = Screen.width * 0.1f;
        rect = new Rect(Screen.width / 2 - size / 2, Screen.height * 0.7f, size, size);
        texture = Resources.Load("Models/Key/Models/Texture/KeyY") as Texture;
	}
	
	// Update is called once per frame
	void Update () {
	    if (GameVariables.keyDisplayTime > 0)
        {
            GameVariables.keyDisplayTime -= Time.deltaTime;
        }
	}

    void OnGUI()
    {
        if (GameVariables.keyDisplayTime > 0)
        {
            GUI.DrawTexture(rect, texture);
        }
    }
}
