using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridObject
{
	#region Variables
	private GridSystem gridSystem;
	private GridPosition gridPosition;
	private List<Unit> unitList;
	#endregion

	#region Properties
	public GridPosition GridPosition
	{
		set
		{
			gridPosition = value;
		}
		get
		{
			return gridPosition;
		}
	}
	#endregion

	#region Constructor
	public GridObject(GridSystem gridSystem, GridPosition gridPosition)
	{
		this.gridSystem = gridSystem;
		this.gridPosition = gridPosition;
		unitList = new List<Unit>();
	}

	public void AddUnit(Unit unit)
	{
		unitList.Add(unit);
	}

	public void RemoveUnit(Unit unit)
	{
		unitList.Remove(unit);
	}

	public List<Unit> GetUnitList()
	{
		return unitList;
	}

	public bool HasUnitList()
	{
		if(unitList.Count>0)
		{
			return true;
		}
		return false;
	}
	#endregion

	public override string ToString()
	{
		string unitString = " ";
		foreach(Unit unit in unitList)
		{
			unitString += unit + "\n";
		}
		return GridPosition.ToString() +"\n" + unitString;
	}
}
