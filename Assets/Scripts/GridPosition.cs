using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct GridPosition
{
	#region Variables
	public int x;
	public int z;
	#endregion

	#region Constructor
	public GridPosition(int x, int z)
	{
		this.x = x;
		this.z = z;
	}
	#endregion

	#region Public Functions
	public override string ToString()
	{
		return "x:" + x + "|z:" + z;
	}
	#endregion
}
