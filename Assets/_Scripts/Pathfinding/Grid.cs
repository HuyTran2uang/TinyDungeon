using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Grid : MonoBehaviour
{
    [field: SerializeField]
    public Tilemap Map { get; set; }

    [field: SerializeField]
    public LayerMask UnWalkableMask { get; set; }

    [field: SerializeField]
    public Transform PointBottomLeft { get; set; }

    private Node[,] _grid;
    private Vector3Int _cellPosBottomLeft;

    private void Awake()
    {
        _cellPosBottomLeft = Map.WorldToCell(PointBottomLeft.position);
        SetGrid();
    }

    private void Update()
    {
        SetGrid();
    }

    private void SetGrid()
    {
        _grid = new Node[Map.size.x, Map.size.y];

        for (int x = 0; x < Map.size.x; x++)
        {
            for (int y = 0; y < Map.size.y; y++)
            {
                Vector3Int cell = Map.WorldToCell(_cellPosBottomLeft + Vector3.right * x * Map.cellSize.x + Vector3.up * y * Map.cellSize.y);
                Vector3 center = Map.GetCellCenterWorld(cell);
                Collider2D hit = Physics2D.OverlapBox(center, Map.cellSize - Map.cellSize / 10, 90 * Mathf.Deg2Rad, UnWalkableMask);
                bool walkable;
                if (hit == null)
                {
                    walkable = true;
                }
                else
                {
                    if (hit.transform == transform)
                        walkable = true;
                    else
                        walkable = !Physics2D.OverlapBox(center, Map.cellSize - Map.cellSize / 10, 90 * Mathf.Deg2Rad, UnWalkableMask);
                }
                _grid[x, y] = new Node(walkable, center, x, y);
            }
        }
    }

    public int MaxSize
    {
        get { return Map.size.x * Map.size.y; }
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
                if (xOfNeighbor >= 0 && xOfNeighbor < Map.size.x && yOfNeighbor >= 0 && yOfNeighbor < Map.size.y)
                    neighborNodes.Add(_grid[xOfNeighbor, yOfNeighbor]);
            }
        }

        return neighborNodes;
    }

    public Node GetNodeFromPos(Vector3 pos)
    {
        Vector3Int cell = Map.WorldToCell(pos);
        int x = Mathf.RoundToInt((cell.x - _cellPosBottomLeft.x) / Map.cellSize.x);
        int y = Mathf.RoundToInt((cell.y - _cellPosBottomLeft.y) / Map.cellSize.y);
        return _grid[x, y];
    }

    public List<Node> path;
    // [SerializeField] private bool onlyDisplayPathGizmos;
    // void OnDrawGizmos()
    // {
    //     if (onlyDisplayPathGizmos)
    //     {
    //         if (path != null)
    //         {
    //             foreach (Node n in path)
    //             {
    //                 if (n == path[0])
    //                     Gizmos.color = Color.gray;
    //                 else
    //                     Gizmos.color = Color.green;

    //                 Gizmos.DrawCube(n.centerPos, Map.cellSize - Map.cellSize / 10);
    //             }
    //         }
    //     }

    //     if (_grid != null && !onlyDisplayPathGizmos)
    //     {
    //         foreach (Node n in _grid)
    //         {
    //             Gizmos.color = n.walkable ? Color.white : Color.red;
    //             Gizmos.DrawCube(n.centerPos, Map.cellSize - Map.cellSize / 10);
    //         }
    //     }
    // }
}
