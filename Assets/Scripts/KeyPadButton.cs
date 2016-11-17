using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class KeyPadButton : Searchable
{
	public string buttonPressed;

	KeyPadButton()
	{
		
	}

    void Start()
    {
        
    }

	public override void OnPointerClick(UnityEngine.EventSystems.PointerEventData eventData)
	{
		if(buttonPressed.Equals("del"))
		{
			UnlockSafe.codeEntry = "";
			message = "";
		}
		else
		{
			UnlockSafe.codeEntry += buttonPressed;
			message = UnlockSafe.codeEntry;
		}
		DisplayMessage (message);
	}

}
