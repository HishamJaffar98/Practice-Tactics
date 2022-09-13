using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode
{
    private GridPosition nodeGridPosition;
    private int gCost;
    private int hCost;
    private int fCost;
    private bool isWalkable = true;
    private PathNode cameFromPathNode;
    public bool IsWalkable { get => isWalkable; set => this.isWalkable = value; }

    public int GCost { get => gCost; set => this.gCost = value; }

    public int HCost { get => hCost; set => this.hCost = value; }

    public int FCost => fCost;
    public GridPosition NodeGridPosition => nodeGridPosition; 
    public PathNode CameFromPathNode { get => cameFromPathNode; set => cameFromPathNode = value; }

    public PathNode(GridPosition gridPosition)
    {
        this.nodeGridPosition = gridPosition;
    }

    public override string ToString()
    {
        return nodeGridPosition.ToString();
    }
  
    public void CalculateFCost()
    {
        fCost = gCost + hCost;
    }

    public void ResetCameFromPathNode()
    {
        cameFromPathNode = null;
    }

	
}
