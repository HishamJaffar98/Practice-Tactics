using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GridSystem
{
	#region Variables
	private int width;
	private int height;
	private float cellSize;
	private GridObject[,] gridObjectCollection;
	#endregion

	#region Properties
	public int Width
	{
		set
		{
			width = value;
		}
		get
		{
			return width;
		}
	}

	public int Height
	{
		set
		{
			height = value;
		}
		get
		{
			return height;
		}
	}

	public float CellSize
	{
		set
		{
			cellSize = value;
		}
		get
		{
			return cellSize;
		}
	}
	#endregion

	#region Constructor
	public GridSystem(int width, int height, float cellSize )
	{
		Width = width;
		Height = height;
		CellSize = cellSize;
		gridObjectCollection = new GridObject[Width, Height];
		CreateGridObjects();
		DebugGridDraw();
	}
	#endregion

	#region Private Functions
	private void CreateGridObjects()
	{
		for (int x = 0; x < Width; x++)
		{
			for (int z = 0; z < Height; z++)
			{
				GridPosition newGridPosition = new GridPosition(x,z);
				gridObjectCollection[x,z] = new GridObject(this, newGridPosition);
			}
		}
	}

	private void DebugGridDraw()
	{
		for (int x = 0; x < Width; x++)
		{
			for (int z = 0; z < Height; z++)
			{
				GridPosition newGridPos = new GridPosition(x, z) ; 
				Debug.DrawLine(GetWorldPosition(newGridPos), GetWorldPosition(newGridPos) + Vector3.right*CellSize, Color.red, 10000);
				Debug.DrawLine(GetWorldPosition(newGridPos), GetWorldPosition(newGridPos) + Vector3.forward*CellSize, Color.red, 10000);
				if(x==Width-1)
				{
					Debug.DrawLine(GetWorldPosition(new GridPosition(x+1,z)), GetWorldPosition(new GridPosition(x + 1, z)) + Vector3.forward * CellSize, Color.red, 10000);
				}
				if(z==Height-1)
				{
					Debug.DrawLine(GetWorldPosition(new GridPosition(x, z + 1)), GetWorldPosition(new GridPosition(x, z + 1)) + Vector3.right * CellSize, Color.red, 10000);
				}

			}
		}
	}
	#endregion

	#region Public Functions
	public Vector3 GetWorldPosition(GridPosition gridPosition)
	{
		return new Vector3(gridPosition.x, 0f, gridPosition.z) * CellSize;
	}

	public GridPosition GetGridPosition (Vector3 worldPosition)
	{
		return new GridPosition(Mathf.FloorToInt(worldPosition.x / CellSize), Mathf.FloorToInt(worldPosition.z / CellSize));
	}

	public void DebugObjects(Transform debugPrefab)
	{
		for (int x = 0; x < Width; x++)
		{
			for (int z = 0; z < Height; z++)
			{
				GridPosition newGridPos = new GridPosition(x, z);
				Transform debugTransformText = GameObject.Instantiate(debugPrefab, GetWorldPosition(newGridPos), Quaternion.identity).GetChild(0);
				debugTransformText.GetComponent<TextMeshPro>().text = GetGridObject(new GridPosition(x,z)).ToString(); ;
			}
		}
	}

	public GridObject GetGridObject(GridPosition gridPosition)
	{
		return gridObjectCollection[gridPosition.x,gridPosition.z];
	}
	#endregion
}