using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridObject
{
	#region Variables
	private GridSystem gridSystem;
	private GridPosition gridPosition;
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
	}
	#endregion

	public override string ToString()
	{
		return GridPosition.ToString();
	}
}
