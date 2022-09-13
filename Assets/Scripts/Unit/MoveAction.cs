using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAction : BaseAction
{
	#region Variables
	[SerializeField] private float movementSpeed = 10f;
	[SerializeField] private float rotationSpeed = 10f;
	[SerializeField] private int moveRange = 4;
	private List<GridPosition> validGridPositionList;
	#endregion

	#region Structs
	private Vector3 targetPosition;
	private Vector3 targetDirection;
	#endregion

	public event Action OnStartMoving;
	public event Action OnStopMoving;

	#region Unity Cycle Functions
	private void Start()
	{
		ValidGridPositionList=GetValidActionGridPositionList();
	}

	private void Update()
	{
		if (!isActionActive)
		{
			return;
		}
		Move();
	}
	#endregion

	#region Properties
	public Vector3 TargetPosition
	{
		set
		{
			targetPosition = value;
		}
		get
		{
			return targetPosition;
		}
	}

	public Vector3 TargetDirection
	{
		set
		{
			targetDirection = value;
		}
		get
		{
			return targetDirection;
		}
	}

	public List<GridPosition> ValidGridPositionList { get => validGridPositionList; set => validGridPositionList = value; }
	#endregion

	#region Private Functions
	private void Move()
	{
		transform.position = Vector3.MoveTowards(transform.position, TargetPosition, movementSpeed * Time.deltaTime);
		transform.forward = Vector3.Lerp(transform.forward, TargetDirection, rotationSpeed * Time.deltaTime);
		if (Vector3.Distance(transform.position, TargetPosition) == 0)
		{
			OnStopMoving?.Invoke();
			ActionComplete();
			ValidGridPositionList = GetValidActionGridPositionList();
		}
	}
	#endregion

	#region Public Functions
	public override List<GridPosition> GetValidActionGridPositionList()
	{
		List<GridPosition> validActionGridPositionList = new List<GridPosition>();
		GridPosition unitGridPosition = unit.UnitCurrentGridPosition;
		for (int x = -moveRange; x <= moveRange; x++)
		{
			for (int z = -moveRange; z <= moveRange; z++)
			{
				GridPosition offsetGridPosition = new GridPosition(x, z);
				GridPosition testGridPosition = unitGridPosition + offsetGridPosition;
				if (!LevelGrid.Instance.IsValidGridPosition(testGridPosition))
				{
					continue;
				}
				if (unitGridPosition == testGridPosition)
				{
					continue;
				}
				if (LevelGrid.Instance.HasUnitOnGridPosition(testGridPosition))
				{
					continue;
				}
				validActionGridPositionList.Add(testGridPosition);
			}
		}
		return validActionGridPositionList;
	}
	public override void TakeAction(GridPosition gridPosition, actionDelegate onActionComplete)
	{
		ActionStart(onActionComplete);
		TargetPosition = ConvertTargetPositionToMiddleOfCell(LevelGrid.Instance.GetWorldPosition(gridPosition));
		TargetDirection = (TargetPosition - transform.position).normalized;
		OnStartMoving?.Invoke();
	}

	private Vector3 ConvertTargetPositionToMiddleOfCell(Vector3 targetPosition)
	{
		return new Vector3(targetPosition.x + 1f, 0, targetPosition.z + 1f);
	}

	public override string GetActionName()
	{
		return "Move";
	}

	#endregion
}
