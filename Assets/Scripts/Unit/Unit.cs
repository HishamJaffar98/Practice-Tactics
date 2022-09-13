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

	private const int ACTION_POINTS_MAX = 2;
	private int actionPoints = ACTION_POINTS_MAX;
	[SerializeField] private bool isEnemy;

	public static event Action OnAnyActionPointsChanged;

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

	public bool IsEnemy
	{
		get
		{
			return isEnemy;
		}
	}
	#endregion

	#region Unity Cycle Fucntions

	private void OnEnable()
	{
		ActionSystem.Instance.OnSelectedUnitChanged += ToggleUnitSelectVisual;
		TurnSystem.Instance.OnTurnChanged += TurnSystem_OnTurnChanged;
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
		TurnSystem.Instance.OnTurnChanged -= TurnSystem_OnTurnChanged;
	}

	private void SpendActionPoints(int amount)
	{
		actionPoints -= amount;
		OnAnyActionPointsChanged?.Invoke();
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
	
	private void TurnSystem_OnTurnChanged()
	{
		if ((IsEnemy && !TurnSystem.Instance.IsPlayerTurn) ||
		   (!IsEnemy && TurnSystem.Instance.IsPlayerTurn))
		{
			actionPoints = ACTION_POINTS_MAX;
			OnAnyActionPointsChanged?.Invoke();
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

	public void Damage()
	{
		Debug.Log(transform + " damaged!");
	}

	public Vector3 GetWorldPosition()
	{
		return transform.position;
	}
}
