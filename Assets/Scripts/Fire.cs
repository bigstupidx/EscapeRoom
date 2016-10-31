using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Fire : Focusable {
    public Ice ice;
    public GameObject iceCube;
    public GameObject key;
    private int remaining;
    public Text message;
    private GameObject visibleKey;
	// Use this for initialization
	void Start () {
        remaining = 5;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public override void OnPointerClick(PointerEventData eventData)
    {
        if (ice.isSelected())
        {
            iceCube.transform.position = new Vector3(-.01f, 2.146f, -8.8795f);
            InvokeRepeating("countDown", 1, 1);
        }
    }

    public void countDown()
    {
        remaining--;
        GetComponent<Renderer>().material.color = remaining % 2 == 1 ? Color.magenta : Color.yellow;
        if (remaining <= 0)
        {
            CancelInvoke("countDown");
            visibleKey = key;
            visibleKey.transform.position = new Vector3(-.350092f, -.24f, 0.1804f);
        }
    }
}
