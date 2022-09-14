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

	private const int ACTION_POINTS_MAX = 4;
	private int actionPoints = ACTION_POINTS_MAX;
	[SerializeField] private bool isEnemy;

	public static Action OnAnyActionPointsChanged;
	public static EventHandler OnAnyUnitSpawned;
	public static EventHandler OnAnyUnitDead;

	#region Referenced Components
	[SerializeField] UnitSelectVisual unitSelectVisual;
	private HealthSystem healthSystem;
	private BaseAction[] actionArray;
	#endregion

	#region Properties
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
		healthSystem.OnDead += HealthSystem_OnDead;
	}
	private void Awake()
	{
		currentGridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
		healthSystem = GetComponent<HealthSystem>();
		actionArray = GetComponents<BaseAction>();
	}

	private void Start()
	{
		OnAnyUnitSpawned?.Invoke(this, EventArgs.Empty);
		LevelGrid.Instance.AddUnitAtGridPosition(currentGridPosition, this);
	}

	void Update()
	{
		GridPosition newGridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
		if(currentGridPosition!=newGridPosition)
		{
			GridPosition oldGridPosition = currentGridPosition;
			currentGridPosition = newGridPosition;
			LevelGrid.Instance.UnitMoveGridPosition(this, oldGridPosition, newGridPosition);
		}
	}

	private void OnDisable()
	{
		ActionSystem.Instance.OnSelectedUnitChanged -= ToggleUnitSelectVisual;
		TurnSystem.Instance.OnTurnChanged -= TurnSystem_OnTurnChanged;
		healthSystem.OnDead -= HealthSystem_OnDead;
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

	public T GetAction<T>() where T : BaseAction
	{
		foreach (BaseAction baseAction in ActionArray)
		{
			if (baseAction is T)
			{
				return (T)baseAction;
			}
		}
		return null;
	}

public int GetActionPoints()
	{
		return actionPoints;
	}

	public void Damage(int damageAmount)
	{
		healthSystem.Damage(damageAmount);
	}
	private void HealthSystem_OnDead()
	{
		LevelGrid.Instance.RemoveUnitAtGridPosition(currentGridPosition, this);
		Destroy(gameObject);
		OnAnyUnitDead?.Invoke(this, EventArgs.Empty);
	}

	public Vector3 GetWorldPosition()
	{
		return transform.position;
	}

	public float GetHealthNormalized()
	{
		return healthSystem.GetHealthNormalized();
	}

}
