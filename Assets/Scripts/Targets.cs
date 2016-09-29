using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class Targets : MonoBehaviour {
    private int microwave;
    private int toilet;
    private int trash;
    private int lamp;
    private int tv;
    private int refrigerator;
    private int drawerDoor;
    private bool keyFound;
    public Targets()
    {
        keyFound = false;
        microwave = 1;
        toilet = 2;
        trash = 3;
        lamp = 4;
        tv = 5;
        refrigerator = 6;
        drawerDoor = 7;
    }
    public bool getKeyFound(){
        return keyFound;
    }
    public void setKeyFound(bool temp){
	    keyFound = temp;
    }
    public int getObject(string name)
    {
        if (name.Equals("Microwave"))
            return getMicrowave();
        if (name.Equals("Toilet"))
            return getToilet();
        if (name.Equals("Trash"))
            return getTrash();
        if (name.Equals("Lamp"))
            return getLamp();
        if (name.Equals("TV"))
            return getTV();
        if (name.Equals("Refrigerator"))
            return getRefrigerator();
        if (name.Equals("Drawer_Big2"))
            return getDrawerDoor();
        return 0;
    }
	private int getMicrowave()
    {
        return microwave;
    }

    private int getToilet()
    {
        return toilet;
    }

    private int getTrash()
    {
        return trash;
    }

    private int getLamp()
    {
        return lamp;
    }

    private int getTV()
    {
        return tv;
    }

    private int getRefrigerator()
    {
        return refrigerator;
    }

    private int getDrawerDoor()
    {
        return drawerDoor;
    }
}
