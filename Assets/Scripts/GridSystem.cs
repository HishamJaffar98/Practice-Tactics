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
	#endregion

	#region Public Functions
	public Vector3 GetWorldPosition(int x, int z)
	{
		return new Vector3(x, 0f, z) * CellSize;
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
				Transform debugTransform = GameObject.Instantiate(debugPrefab, GetWorldPosition(x, z), Quaternion.identity);
				debugTransform.gameObject.GetComponent<TextMeshPro>().text = "x:" + x + "|z:" + z;
			}
		}
	}
	#endregion
}
