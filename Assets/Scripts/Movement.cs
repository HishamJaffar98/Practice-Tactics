using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Movement : MonoBehaviour
{
	enum MovementStates { Moving, Idle };

	#region Variables
	private MovementStates currentMovementState = MovementStates.Idle;
	private Vector3 targetPosition;
	[SerializeField] private float movementSpeed = 10f;
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

	#region Unity Cycle Fucntions
	private void OnEnable()
	{
		MouseInputDetector.OnGameObjectClicked += SetMovementParameters;
	}
	private void OnDisable()
	{
		MouseInputDetector.OnGameObjectClicked -= SetMovementParameters;
	}

	
	void Start()
    {

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
			transform.position = Vector3.MoveTowards(transform.position, targetPosition, movementSpeed*Time.deltaTime);
			if(Vector3.Distance(transform.position, targetPosition)==0)
			{
				currentMovementState = MovementStates.Idle;
			}
		}
	}

	private void SetMovementParameters(Vector3 targetPosition)
	{
		if(currentMovementState==MovementStates.Moving)
		{
			return;
		}
		TargetPosition = new Vector3(targetPosition.x, 0f, targetPosition.z);
		currentMovementState = MovementStates.Moving;
	}
	#endregion
}
