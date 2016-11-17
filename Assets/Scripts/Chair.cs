using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class Chair : Searchable{
    public Axe axe;
    public GameObject Pos1;
    public GameObject Pos2;
    public GameObject AxeOb;
    public GameObject InventoryPosition;
    public GameObject log;
    GoalMover mover;

    void Start()
    {
        log.SetActive(false);
    }
    public override void OnPointerClick(PointerEventData eventData)
    {
        if (axe.checkClick())
        {
            mover = AxeOb.GetComponent<GoalMover>();
            mover.ClearGoals();
            mover.AddGoal(Pos1.transform.position, Pos1.transform.rotation);
            mover.AddGoal(Pos2.transform.position, Pos2.transform.rotation);

            mover.MovementComplete += Mover_FinishSelection;

        }
        else
        {
            base.OnPointerClick(eventData);
        }
    }

    void Mover_FinishSelection()
    {
        log.SetActive(true);
        gameObject.SetActive(false);
        mover.AddGoal(Pos1.transform.position, Pos1.transform.rotation);
        mover.AddGoal(InventoryPosition.transform.position, InventoryPosition.transform.rotation);
        mover.MovementComplete -= Mover_FinishSelection;
    }
}
