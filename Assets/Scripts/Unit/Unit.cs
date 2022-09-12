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

	#region Variables
	bool isSelected = false;
	#endregion

	#region Referenced Components
	[SerializeField] UnitSelectVisual unitSelectVisual;
	private MoveAction moveAction;
	#endregion

	#region Properties
	public bool IsSelected
	{
		set
		{
			isSelected = value;
		}
		get
		{
			return isSelected;
		}
	}

	public MoveAction MoveActionComponent
	{
		get
		{
			return moveAction;
		}
	}

	public GridPosition UnitCurrentGridPosition
	{
		get
		{
			return currentGridPosition;
		}
	}
	#endregion

	#region Unity Cycle Fucntions

	private void OnEnable()
	{
		ActionSystem.Instance.OnSelectedUnitChanged += SetUnitAsSelected;
	}
	private void Awake()
	{
		currentGridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
		moveAction = GetComponent<MoveAction>();
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
		ActionSystem.Instance.OnSelectedUnitChanged -= SetUnitAsSelected;
	}

	#endregion

	#region Private Functions
	private void SetUnitAsSelected(Unit unit)
	{
		if(unit==this)
		{
			IsSelected = true;
			unitSelectVisual.ToggleSelectedVisual(true);
		}
		else
		{
			IsSelected = false;
			unitSelectVisual.ToggleSelectedVisual(false);
		}
	}
	#endregion

	#region Public Functions
	#endregion
}
