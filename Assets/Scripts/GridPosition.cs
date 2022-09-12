using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct GridPosition : IEquatable<GridPosition>
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

	public override bool Equals(object obj)
	{
		return obj is GridPosition position &&
			   x == position.x &&
			   z == position.z;
	}

	public override int GetHashCode()
	{
		return HashCode.Combine(x, z);
	}

	public bool Equals(GridPosition other)
	{
		return this == other;
	}
	#endregion

	public static bool operator !=(GridPosition firstGridPosition, GridPosition secondGridPosition)
	{
		if (firstGridPosition.x != secondGridPosition.x || firstGridPosition.z != secondGridPosition.z)
		{
			return true;
		}
		else
		{
			return false;
		}
	}

	public static bool operator ==(GridPosition firstGridPosition, GridPosition secondGridPosition)
	{
		if (firstGridPosition.x == secondGridPosition.x && firstGridPosition.z == secondGridPosition.z)
		{
			return true;
		}
		else
		{
			return false;
		}
	}
}
