using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class TestBase : MonoBehaviour {
    public void TriggerClick(GameObject target)
    {
        EventSystem eventSystem = Object.FindObjectOfType(typeof(EventSystem)) as EventSystem;
        
        ExecuteEvents.Execute(target, new PointerEventData(eventSystem), ExecuteEvents.pointerClickHandler);
    }

    public void TriggerCursorEnter(GameObject target)
    {
        EventSystem eventSystem = Object.FindObjectOfType(typeof(EventSystem)) as EventSystem;
        ExecuteEvents.Execute(target, new PointerEventData(eventSystem), ExecuteEvents.pointerEnterHandler);
    }

    public void TriggerCursorExit(GameObject target)
    {
        EventSystem eventSystem = Object.FindObjectOfType(typeof(EventSystem)) as EventSystem;
        ExecuteEvents.Execute(target, new PointerEventData(eventSystem), ExecuteEvents.pointerExitHandler);
    }
}
