using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAction : MonoBehaviour
{
    protected Unit unit;
    protected bool isActionActive;
    public delegate void actionDelegate();
    protected actionDelegate OnActionComplete;
    protected virtual void Awake()
    {
        unit = GetComponent<Unit>();
    }

    public abstract string GetActionName();
    public abstract void TakeAction(GridPosition gridPosition, actionDelegate onActionComplete);

    public virtual bool IsValidActionGridPosition(GridPosition gridPosition)
    {
        List<GridPosition> validGridPositionList = GetValidActionGridPositionList();
        return validGridPositionList.Contains(gridPosition);
    }
    public abstract List<GridPosition> GetValidActionGridPositionList();
    public virtual int GetActionPointsCost()
    {
        return 1;
    }

    protected void ActionStart(actionDelegate onActionComplete)
    {
        isActionActive = true;
        this.OnActionComplete = onActionComplete;
    }

    protected void ActionComplete()
    {
        isActionActive = false;
        OnActionComplete();
    }
}
