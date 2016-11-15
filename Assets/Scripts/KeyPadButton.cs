using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class KeyPadButton : MonoBehaviour
{
	public string buttonPressed;

	KeyPadButton()
	{
		
	}

    void Start()
    {
        
    }

	public void workNowKThxBye() {
		UnlockSafe.codeEntry += buttonPressed;
	}

}
