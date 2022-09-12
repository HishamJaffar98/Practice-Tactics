using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelGrid : MonoBehaviour
{
    public static LevelGrid Instance { get; private set; }

    private GridSystem newGridSystem;
    [SerializeField] private Transform gridDebugObjectPrefab;
    
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

        newGridSystem = new GridSystem(10, 10, 2f);
    }

	private void Start()
	{
        newGridSystem.CreateDebugObjects(gridDebugObjectPrefab);
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
    }
    public GridPosition GetGridPosition(Vector3 worldPosition) => newGridSystem.GetGridPosition(worldPosition);
}
