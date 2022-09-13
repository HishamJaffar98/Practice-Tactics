using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelGrid : MonoBehaviour
{
    public static LevelGrid Instance { get; private set; }

    public event Action OnAnyUnitMovedGridPosition;

    private GridSystem<GridObject> newGridSystem;
    [SerializeField] private Transform gridDebugObjectPrefab;
    [SerializeField] private int width;
    [SerializeField] private int height;
    [SerializeField] private float cellSize;


    public Transform GridDebugObject
	{
		get
		{
            return gridDebugObjectPrefab;
		}
	}
    private void Awake()
	{
        Debug.Log("Creating Grid System....");
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            Instance = this;
        }
        newGridSystem = new GridSystem<GridObject>(width, height, cellSize,
             (GridSystem<GridObject> g, GridPosition gridPosition) => new GridObject(g, gridPosition));
    }

    private void Start()
    {
        Pathfinding.Instance.Setup(width, height, cellSize);
    }

	public void AddUnitAtGridPosition(GridPosition positionOnGrid, Unit unit)
	{
        GridObject newGridObject = newGridSystem.GetGridObject(positionOnGrid);
        newGridObject.AddUnit(unit);
    }

    public List<Unit> GetUnitListAtGridPosition(GridPosition positionOnGrid)
	{
        GridObject newGridObject = newGridSystem.GetGridObject(positionOnGrid);
        return newGridObject.GetUnitList();
	}

    public void RemoveUnitAtGridPosition(GridPosition positionOnGrid, Unit unit)
	{
        GridObject newGridObject = newGridSystem.GetGridObject(positionOnGrid);
        newGridObject.RemoveUnit(unit);
    }
    
    public void UnitMoveGridPosition(Unit unit, GridPosition fromGridPosition, GridPosition toGridPosition)
	{
        RemoveUnitAtGridPosition(fromGridPosition,unit);
        AddUnitAtGridPosition(toGridPosition,unit);
        OnAnyUnitMovedGridPosition?.Invoke();
    }
  
    public bool HasUnitOnGridPosition(GridPosition gridPosition)
	{
        GridObject newGridObject = newGridSystem.GetGridObject(gridPosition);
        return newGridObject.HasUnitList();
    }

    public Unit GetUnitAtGridPosition(GridPosition gridPosition)
    {
        GridObject gridObject = newGridSystem.GetGridObject(gridPosition);
        return gridObject.GetUnit();
    }

    public GridPosition GetGridPosition(Vector3 worldPosition) => newGridSystem.GetGridPosition(worldPosition);
    public Vector3 GetWorldPosition(GridPosition gridPosition) => newGridSystem.GetWorldPosition(gridPosition);
    public bool IsValidGridPosition(GridPosition gridPosition) => newGridSystem.IsValidGridPosition(gridPosition);
    public int GetWidth() => newGridSystem.Width;
    public int GetHeight() => newGridSystem.Height;

}
