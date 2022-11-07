using UnityEngine;
using System.Collections;

public class Node : IHeapItem<Node>
{
    public bool walkable;
    public Vector3 centerPos;
    public int gridX;
    public int gridY;
    public int gCost;
    public int hCost;
    public Node parent;

    public Node(bool walkable, Vector3 centerPos, int gridX, int gridY)
    {
        this.walkable = walkable;
        this.centerPos = centerPos;
        this.gridX = gridX;
        this.gridY = gridY;
    }

    public int fCost
    {
        get { return gCost + hCost; }
    }

    public int HeapIndex { get; set; }

    public int CompareTo(Node nodeToCompare)
    {
        int compare = fCost.CompareTo(nodeToCompare.fCost);
        if (compare == 0)
            compare = hCost.CompareTo(nodeToCompare.hCost);
        return -compare;
    }
}