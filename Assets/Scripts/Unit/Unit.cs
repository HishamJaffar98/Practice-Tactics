using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;

public class Unit : MonoBehaviour
{

	#region Structs
	private GridPosition currentGridPosition;
	#endregion

	private int actionPoints = 2;

	#region Referenced Components
	[SerializeField] UnitSelectVisual unitSelectVisual;
	private MoveAction moveAction;
	private SpinAction spinAction;
	private BaseAction[] actionArray;
	#endregion

	#region Properties
	public MoveAction MoveActionComponent
	{
		get
		{
			return moveAction;
		}
	}

	public SpinAction SpinActionComponent
	{
		get
		{
			return spinAction;
		}
	}

	public GridPosition UnitCurrentGridPosition
	{
		get
		{
			return currentGridPosition;
		}
	}

	public BaseAction[] ActionArray
	{
		get
		{
			return actionArray;
		}
	}
	#endregion

	#region Unity Cycle Fucntions

	private void OnEnable()
	{
		ActionSystem.Instance.OnSelectedUnitChanged += ToggleUnitSelectVisual;
	}
	private void Awake()
	{
		currentGridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
		moveAction = GetComponent<MoveAction>();
		spinAction = GetComponent<SpinAction>();
		actionArray = GetComponents<BaseAction>();
	}

	private void Start()
	{
		LevelGrid.Instance.AddUnitAtGridPosition(currentGridPosition, this);
	}

	void Update()
	{
		GridPosition newGridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
		if(currentGridPosition!=newGridPosition)
		{
			LevelGrid.Instance.UnitMoveGridPosition(this, currentGridPosition, newGridPosition);
			currentGridPosition = newGridPosition;
		}
	}

	private void OnDisable()
	{
		ActionSystem.Instance.OnSelectedUnitChanged -= ToggleUnitSelectVisual;
	}

	private void SpendActionPoints(int amount)
	{
		actionPoints -= amount;
	}
	#endregion

	#region Private Functions
	private void ToggleUnitSelectVisual()
	{
		if(ActionSystem.Instance.SelectedUnit==this)
		{
			unitSelectVisual.ToggleSelectedVisual(true);
		}
		else
		{
			unitSelectVisual.ToggleSelectedVisual(false);
		}
	}
	#endregion

	public bool TrySpendActionPointsToTakeAction(BaseAction baseAction)
	{
		if (CanSpendActionPointsToTakeAction(baseAction))
		{
			SpendActionPoints(baseAction.GetActionPointsCost());
			return true;
		}
		else
		{
			return false;
		}
	}

	public bool CanSpendActionPointsToTakeAction(BaseAction baseAction)
	{
		if (actionPoints >= baseAction.GetActionPointsCost())
		{
			return true;
		}
		else
		{
			return false;
		}
	}

	public int GetActionPoints()
	{
		return actionPoints;
	}
}
