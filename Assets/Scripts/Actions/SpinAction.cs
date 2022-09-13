using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinAction : BaseAction
{
    private float totalSpinAmount;
    [SerializeField] float spinAmount = 360f;
    private void Update()
    {
        if (!isActionActive)
        {
            return;
        }

        transform.Rotate(Vector3.up, spinAmount * Time.deltaTime);
        totalSpinAmount += spinAmount * Time.deltaTime;
        if (totalSpinAmount >= 360f)
        {
            ActionComplete();
        }
    }

    public override void TakeAction(GridPosition gridPosition, actionDelegate onActionComplete)
    {
        totalSpinAmount = 0f;
        ActionStart(onActionComplete);
    }

	public override string GetActionName()
	{
        return "Spin";
	}

    public override List<GridPosition> GetValidActionGridPositionList()
    {
        GridPosition unitGridPosition = unit.UnitCurrentGridPosition;
        return new List<GridPosition>
        {
            unitGridPosition
        };
    }

    public override int GetActionPointsCost()
    {
        return 1;
    }
    public override EnemyAIAction GetEnemyAIAction(GridPosition gridPosition)
    {
        return new EnemyAIAction
        {
            gridPosition = gridPosition,
            actionValue = 0,
        };
    }
}
