using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;

public class Unit : MonoBehaviour
{
	enum MovementStates { Moving, Idle };

	#region Variables
	[SerializeField] private float movementSpeed = 10f;
	[SerializeField] private float rotationSpeed = 10f;
	[SerializeField] private Animator unitAnimator;
	private MovementStates currentMovementState = MovementStates.Idle;
	private Vector3 targetPosition;
	private Vector3 targetDirection;
	bool isSelected = false;

	#endregion

	#region Referenced Components
	[SerializeField] UnitSelectVisual unitSelectVisual;
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
	#endregion

	#region Unity Cycle Fucntions

	void Start()
	{
		MouseInputDetector.OnFloorClicked += SetMovementParameters;
		UnitSelector.Instance.OnSelectedUnitChanged += SetUnitAsSelected;
	}
	private void OnDisable()
	{
		MouseInputDetector.OnFloorClicked -= SetMovementParameters;
		UnitSelector.Instance.OnSelectedUnitChanged -= SetUnitAsSelected;
	}

    void Update()
	{
		Move();
	}
	#endregion

	#region Private Functions
	private void Move()
	{
		if (currentMovementState == MovementStates.Moving)
		{
			transform.position = Vector3.MoveTowards(transform.position, TargetPosition, movementSpeed*Time.deltaTime);
			transform.forward = Vector3.Lerp(transform.forward, TargetDirection, rotationSpeed * Time.deltaTime);
			if(Vector3.Distance(transform.position, TargetPosition) ==0)
			{
				currentMovementState = MovementStates.Idle;
				unitAnimator.SetBool(AnimationParameterManager.isMoving, false);
			}
		}
	}

	private void SetMovementParameters(Vector3 targetPosition)
	{
		if(currentMovementState==MovementStates.Idle && IsSelected==true)
		{
			TargetPosition = new Vector3(targetPosition.x, 0f, targetPosition.z);
			TargetDirection = (TargetPosition - transform.position).normalized;
			unitAnimator.SetBool(AnimationParameterManager.isMoving, true);
			currentMovementState = MovementStates.Moving;
		}
	}
	#endregion

	#region Public Functions
	public void SetUnitAsSelected(Unit unit)
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
}
