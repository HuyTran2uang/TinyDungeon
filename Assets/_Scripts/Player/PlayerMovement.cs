using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Grid))]
[RequireComponent(typeof(Pathfinding))]
public class PlayerMovement : MonoBehaviour, ISelectTarget
{
    Grid grid;
    Pathfinding pathfinding;
    public float MoveSpeed => Player.Instance.Speed / 100;
    float scopeRangeAttack = 4;
    Vector2 targetPosition;
    public Transform TargetObject { get; private set; }
    LayerMask layerTargets;
    GameObject targetPositionPrefabs;
    GameObject targetEnemyPrefabs;
    bool isFollowing;

    private void Awake()
    {
        grid = GetComponent<Grid>();
        pathfinding = GetComponent<Pathfinding>();

        targetPositionPrefabs = Resources.Load<GameObject>("TargetPosition");
        targetEnemyPrefabs = Resources.Load<GameObject>("TargetEnemy");
        SetGrid();
    }

    private void SetGrid()
    {
        grid.Map = GameObject.Find("GamePlay").GetComponent<Tilemap>();
        grid.UnWalkableMask = LayerMask.GetMask("Enemy", "UnWalkable");
        grid.PointBottomLeft = grid.Map.transform.GetChild(0);
    }

    private void Start()
    {
        targetPosition = grid.GetNodeFromPos(transform.position).centerPos;
    }

    private void FixedUpdate()
    {
        switch (Player.Instance.Type)
        {
            case PlayerType.Melee:
                MeleeMovement();
                break;
            case PlayerType.Distance:
                RangeMovement();
                break;
            case PlayerType.Magic:
                RangeMovement();
                break;
        }
    }

    private void MeleeMovement()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                DestroyAllTargetEnemyPrefab();
                DestroyAllTargetPositionPrefab();

                Collider2D hit = Physics2D.OverlapCircle(Camera.main.ScreenToWorldPoint(Input.mousePosition), 0, layerTargets);
                if (hit == null)
                {
                    TargetObject = null;
                    targetPosition = grid.GetNodeFromPos(Camera.main.ScreenToWorldPoint(Input.mousePosition)).centerPos;
                    Instantiate(targetPositionPrefabs, targetPosition, Quaternion.identity);
                }
                if (hit != null)
                {
                    TargetObject = hit.transform;
                    Instantiate(targetEnemyPrefabs, TargetObject);
                }
            }
        }

        if (TargetObject != null)
        {
            targetPosition = TargetObject.position;
            pathfinding.FindPathToTransform(transform.position, TargetObject);
        }

        if (TargetObject == null)
        {
            DestroyAllTargetEnemyPrefab();
            pathfinding.FindPath(transform.position, targetPosition);
        }

        if ((Vector2)transform.position == targetPosition)
        {
            DestroyAllTargetPositionPrefab();
        }

        MoveFollowPathFound();
    }

    private void RangeMovement()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                DestroyAllTargetPositionPrefab();

                Collider2D hit = Physics2D.OverlapCircle(Camera.main.ScreenToWorldPoint(Input.mousePosition), 0, layerTargets);
                if (hit == null)
                {
                    targetPosition = grid.GetNodeFromPos(Camera.main.ScreenToWorldPoint(Input.mousePosition)).centerPos;
                    Instantiate(targetPositionPrefabs, targetPosition, Quaternion.identity);
                    if (TargetObject == null) return;
                    isFollowing = false;
                    grid.path.Clear();
                }
                if (hit != null)
                {
                    DestroyAllTargetEnemyPrefab();
                    isFollowing = true;
                    TargetObject = hit.transform;
                    Instantiate(targetEnemyPrefabs, TargetObject);
                }
            }
        }

        if (TargetObject != null)
        {
            if (isFollowing)
            {
                targetPosition = grid.GetNodeFromPos(transform.position).centerPos;
                pathfinding.FindPathToTransform(transform.position, TargetObject);
            }
            else
                pathfinding.FindPath(transform.position, targetPosition);
        }

        if (TargetObject == null)
        {
            DestroyAllTargetEnemyPrefab();
            pathfinding.FindPath(transform.position, targetPosition);
        }

        if ((Vector2)transform.position == targetPosition)
        {
            DestroyAllTargetPositionPrefab();
        }


        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, scopeRangeAttack, layerTargets);
        foreach (var hit in hits)
            if (hit != null)
                if (hit.transform == TargetObject && isFollowing)
                    MoveToCellCenter(transform.position);
        MoveFollowPathFound();
    }

    private void MoveFollowPathFound()
    {
        if (grid.path == null || grid.path.Count <= 0)
        {
            MoveToCellCenter(transform.position);
            return;
        }

        if ((transform.position.x - grid.path[0].centerPos.x) < 0)
            transform.localScale = Vector3.one;
        if ((transform.position.x - grid.path[0].centerPos.x) > 0)
            transform.localScale = new Vector3(-1, 1, 1);

        transform.position = Vector2.MoveTowards(transform.position, grid.path[0].centerPos, MoveSpeed * Time.deltaTime);
    }

    private void MoveToCellCenter(Vector3 pos)
    {
        Vector3Int cell = grid.Map.WorldToCell(transform.position);
        Vector3 center = grid.Map.GetCellCenterWorld(cell);
        transform.position = Vector2.MoveTowards(transform.position, center, MoveSpeed * Time.deltaTime);
    }

    private void DestroyAllTargetPositionPrefab()
    {
        var objsTargetPosition = GameObject.FindGameObjectsWithTag("TargetPosition");
        foreach (var i in objsTargetPosition)
            Destroy(i.gameObject);
    }

    private void DestroyAllTargetEnemyPrefab()
    {
        var objsTargetEnemy = GameObject.FindGameObjectsWithTag("TargetEnemy");
        foreach (var i in objsTargetEnemy)
            Destroy(i.gameObject);
    }
}
