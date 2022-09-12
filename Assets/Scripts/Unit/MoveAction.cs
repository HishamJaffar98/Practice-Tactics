using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAction : MonoBehaviour
{
	#region Variables
	[SerializeField] private float movementSpeed = 10f;
	[SerializeField] private float rotationSpeed = 10f;
	[SerializeField] private int moveRange = 4;
	private List<GridPosition> validGridPositionList;
	#endregion

	#region Structs
	enum MovementStates { Moving, Idle };
	private MovementStates currentMovementState = MovementStates.Idle;
	private Vector3 targetPosition;
	private Vector3 targetDirection;
	#endregion 

	#region Cached Components
	[SerializeField] private Animator unitAnimator;
	private Unit unit;
	#endregion

	#region Unity Cycle Functions
	private void Awake()
	{
		unit = GetComponent<Unit>();
	}

	private void Start()
	{
		ValidGridPositionList=GetValidActionGridPositionList();
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

	#region Unity Cycle Fucntions
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
			transform.position = Vector3.MoveTowards(transform.position, TargetPosition, movementSpeed * Time.deltaTime);
			transform.forward = Vector3.Lerp(transform.forward, TargetDirection, rotationSpeed * Time.deltaTime);
			if (Vector3.Distance(transform.position, TargetPosition) == 0)
			{
				currentMovementState = MovementStates.Idle;
				unitAnimator.SetBool(AnimationParameterManager.isMoving, false);
				ValidGridPositionList = GetValidActionGridPositionList();
			}
		}
	}

	private bool isTargetPositionInValidActionGridPositionList(Vector3 targetPosition)
	{
		GridPosition targetGridPosition = LevelGrid.Instance.GetGridPosition(targetPosition);
		if (ValidGridPositionList.Contains(targetGridPosition))
		{
			return true;
		}
		return false;
	}
	#endregion

	#region Public Functions
	public void SetMovementParameters(Vector3 targetPosition)
	{
		if (isTargetPositionInValidActionGridPositionList(targetPosition))
		{
			if (currentMovementState == MovementStates.Idle)
			{
				TargetPosition = new Vector3(targetPosition.x, 0f, targetPosition.z);
				TargetDirection = (TargetPosition - transform.position).normalized;
				unitAnimator.SetBool(AnimationParameterManager.isMoving, true);
				currentMovementState = MovementStates.Moving;
			}
		}
	}

	public List<GridPosition> GetValidActionGridPositionList()
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
	#endregion
}
