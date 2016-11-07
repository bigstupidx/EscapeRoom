using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class TestBase {
    public void Click(GameObject target)
    {
        EventSystem eventSystem = Object.FindObjectOfType(typeof(EventSystem)) as EventSystem;
        
        ExecuteEvents.Execute(target, new PointerEventData(eventSystem), ExecuteEvents.pointerClickHandler);
    }

    public void Enter(GameObject target)
    {
        EventSystem eventSystem = Object.FindObjectOfType(typeof(EventSystem)) as EventSystem;
        ExecuteEvents.Execute(target, new PointerEventData(eventSystem), ExecuteEvents.pointerEnterHandler);
    }

    public void Exit(GameObject target)
    {
        EventSystem eventSystem = Object.FindObjectOfType(typeof(EventSystem)) as EventSystem;
        ExecuteEvents.Execute(target, new PointerEventData(eventSystem), ExecuteEvents.pointerExitHandler);
    }
}
