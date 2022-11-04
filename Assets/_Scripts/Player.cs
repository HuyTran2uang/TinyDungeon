using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviourSingleton<Player>
{
    [SerializeField] private float _speed;
    [SerializeField] private PlayerType _type;
    public float Speed
    {
        get { return _speed; }
    }
    public PlayerType Type
    {
        get { return _type; }
    }

    Grid grid;
    Pathfinding pathfinding;

    private void Awake()
    {
        grid = GetComponent<Grid>();
        pathfinding = GetComponent<Pathfinding>();
    }

    private void Start()
    {
        transform.localPosition = grid.GetNodeFromPos(transform.localPosition).centerPos;
    }

    private void Update()
    {

    }

    private void FixedUpdate()
    {
        switch (Type)
        {
            case PlayerType.Melee:
                MeleeMovement();
                break;
            case PlayerType.Distance:
                break;
            case PlayerType.Magic:
                break;
        }
    }

    //movement
    private Vector2 targetPosition;
    private Transform targetTransform;
    [SerializeField] private LayerMask layerTargets;
    [SerializeField] private GameObject targetPositionPrefabs;
    [SerializeField] private GameObject targetEnemyPrefabs;

    private void MeleeMovement()
    {
        if (Input.GetMouseButtonDown(0))
        {
            DestroyAllTargetEnemyPrefab();
            DestroyAllTargetPositionPrefab();

            Collider2D hit = Physics2D.OverlapCircle(Camera.main.ScreenToWorldPoint(Input.mousePosition), 0, layerTargets);
            if (hit == null)
            {
                targetTransform = null;
                targetPosition = grid.GetNodeFromPos(Camera.main.ScreenToWorldPoint(Input.mousePosition)).centerPos;
                Instantiate(targetPositionPrefabs, targetPosition, Quaternion.identity);
            }
            if (hit != null)
            {
                targetTransform = hit.transform;
                Instantiate(targetEnemyPrefabs, targetTransform);
            }
        }

        if (targetTransform != null)
        {
            pathfinding.FindPath(transform.position, targetTransform.position);
        }

        if (targetTransform == null)
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

    void MoveFollowPathFound()
    {
        if (grid.path == null || grid.path.Count <= 0)
        {
            Vector3Int cell = grid.map.WorldToCell(transform.position);
            Vector3 center = grid.map.GetCellCenterWorld(cell);
            transform.position = Vector2.MoveTowards(transform.position, center, Speed * Time.deltaTime);
            return;
        }
        transform.position = Vector2.MoveTowards(transform.position, grid.path[0].centerPos, Speed * Time.deltaTime);
    }

    IEnumerator Moving(Vector3 nextPos)
    {
        yield return transform.position = Vector2.MoveTowards(transform.position, nextPos, Speed * Time.deltaTime);
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 0.7f);
    }

    //attack
    private void MeleeAttack()
    {

    }
}

public enum PlayerType
{
    Melee,
    Distance,
    Magic
}
