using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Unit : MonoBehaviour
{
	enum MovementStates { Moving, Idle };

	#region Variables
	private MovementStates currentMovementState = MovementStates.Idle;
	private Vector3 targetPosition;
	[SerializeField] private float speed = 10f;
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
	#endregion

	#region UnityCycleFucntions
	private void OnEnable()
	{
		Floor.OnFloorClicked += SetMovementParameters;
	}
	private void OnDisable()
	{
		Floor.OnFloorClicked -= SetMovementParameters;
	}

	void Start()
    {
        
    }

    void Update()
	{
		Move();
	}
	#endregion

	#region PrivateFunctions
	private void Move()
	{
		if (currentMovementState == MovementStates.Moving)
		{
			transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed*Time.deltaTime);
			if(Vector3.Distance(transform.position, targetPosition)==0)
			{
				currentMovementState = MovementStates.Idle;
				Debug.Log("StoppedMoving");
			}
		}
	}

	private void SetMovementParameters(Vector3 targetPosition)
	{
		if(currentMovementState==MovementStates.Moving)
		{
			return;
		}

		TargetPosition = targetPosition;
		currentMovementState = MovementStates.Moving;
	}
	#endregion
}
