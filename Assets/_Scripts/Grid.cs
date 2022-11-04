using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Grid : MonoBehaviour
{
    [SerializeField] public Tilemap map;
    [SerializeField] private LayerMask unWalkableMask;
    private Node[,] grid;
    [SerializeField] private Transform pointBottomLeft;
    private Vector3Int cellPosBottomLeft;

    private void Awake()
    {
        cellPosBottomLeft = map.WorldToCell(pointBottomLeft.position);
        SetGrid();
    }

    private void Update()
    {
        SetGrid();
    }

    private void SetGrid()
    {
        grid = new Node[map.size.x, map.size.y];

        for (int x = 0; x < map.size.x; x++)
        {
            for (int y = 0; y < map.size.y; y++)
            {
                Vector3Int cell = map.WorldToCell(cellPosBottomLeft + Vector3.right * x * map.cellSize.x + Vector3.up * y * map.cellSize.y);
                Vector3 center = map.GetCellCenterWorld(cell);
                bool walkable = !Physics2D.OverlapBox(center, map.cellSize - map.cellSize / 10, 90 * Mathf.Deg2Rad, unWalkableMask);
                grid[x, y] = new Node(walkable, center, x, y);
            }
        }
    }

    public int MaxSize
    {
        get { return map.size.x * map.size.y; }
    }

    public List<Node> GetNeighborNodes(Node node)
    {
        List<Node> neighborNodes = new List<Node>();

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0) continue;
                if (x != 0 && y != 0) continue;

                int xOfNeighbor = node.gridX + x;
                int yOfNeighbor = node.gridY + y;

                // 0 <= gridOfNeighbor < MaxSize
                if (xOfNeighbor >= 0 && xOfNeighbor < map.size.x && yOfNeighbor >= 0 && yOfNeighbor < map.size.y)
                    neighborNodes.Add(grid[xOfNeighbor, yOfNeighbor]);
            }
        }

        return neighborNodes;
    }

    public Node GetNodeFromPos(Vector3 pos)
    {
        Vector3Int cell = map.WorldToCell(pos);
        int x = Mathf.RoundToInt((cell.x - cellPosBottomLeft.x) / map.cellSize.x);
        int y = Mathf.RoundToInt((cell.y - cellPosBottomLeft.y) / map.cellSize.y);
        return grid[x, y];
    }

    public List<Node> path;
    [SerializeField] private bool onlyDisplayPathGizmos;
    void OnDrawGizmos()
    {
        if (onlyDisplayPathGizmos)
        {
            if (path != null)
            {
                foreach (Node n in path)
                {
                    if (n == path[0])
                        Gizmos.color = Color.gray;
                    else
                        Gizmos.color = Color.green;

                    Gizmos.DrawCube(n.centerPos, map.cellSize - map.cellSize / 10);
                }
            }
        }
        else
        {
            if (grid != null && onlyDisplayPathGizmos)
            {
                foreach (Node n in grid)
                {
                    Gizmos.color = n.walkable ? Color.white : Color.red;
                    Gizmos.DrawWireCube(n.centerPos, map.cellSize - map.cellSize / 10);
                }
            }
        }
    }
}
