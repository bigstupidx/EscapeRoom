using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class Chair : Searchable {
    public Axe axe;
    public GameObject InventoryPosition;
    public override void OnPointerClick(PointerEventData eventData)
    {
        if (axe.checkClick())
        {
            GoalMover mover = gameObject.GetComponent<GoalMover>();
            mover.ClearGoals();
            mover.AddGoal(InventoryPosition.transform.position, InventoryPosition.transform.rotation);
        }
        else
        {
            base.OnPointerClick(eventData);
        }
    }

    
}
