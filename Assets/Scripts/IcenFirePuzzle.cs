using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class IcenFirePuzzle : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{

    public GameObject iceCube;
    public GameObject toilet;
    private bool isClicked;
    // Use this for initialization
    void Start()
    {
        isClicked = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

 
    public void OnPointerEnter(PointerEventData eventData)
    {
        foreach (Renderer renderer in GetComponentsInChildren<Renderer>(true))
        {
            foreach (Material material in renderer.materials)
            {
                material.SetColor("_Color", new Color(0.2f, 0.2f, 0, 0));
                material.SetColor("_OutlineColor", new Color(0.4f, 0.4f, 0, 0));
                material.SetFloat("_Outline", 0.006f);
                material.shader = Shader.Find("Outlined/Diffuse");
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        foreach (Renderer renderer in GetComponentsInChildren<Renderer>(true))
        {
            foreach (Material material in renderer.materials)
            {
                material.shader = Shader.Find("Standard");
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        onClick();
    }

    public void onClick()
    {
        if (GetComponent<Renderer>().name == iceCube.name)
        {
            isClicked = true;
            print("worked");
        }
        else if (isClicked)
        {
            print(GetComponent<Renderer>());
            if (GetComponent<Renderer>().name == toilet.name)
            {
                print(GetComponent<Renderer>());
                iceCube.transform.position = new Vector3(-.01f, 2.146f, -8.8795f);
            }
            else
            {
                print("This doesn't seem like the right place to place the ice.");
            }
        }
    }
}
