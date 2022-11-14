using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Grid))]
[RequireComponent(typeof(Pathfinding))]
public class EnemyMeleeMovement : MonoBehaviour, IMeleeMoveable, ISelectTarget
{
    Enemy _enemy;

    public float MoveSpeed
    {
        get { return _enemy.Speed; }
    }

    Grid _grid;
    Pathfinding _pathfinding;
    Vector3 _originalPosition;
    Vector3 _targetPosition;
    public Transform TargetObject { get; private set; }

    float _activeArea = 6f;
    float _timeIdle = 10f;
    float _timer;
    bool _isPatience = true;
    LayerMask _layersTarget;

    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
        _grid = GetComponent<Grid>();
        _pathfinding = GetComponent<Pathfinding>();
        _layersTarget = LayerMask.GetMask("Player");

        SetGrid();
    }

    private void SetGrid()
    {
        _grid.Map = GameObject.Find("GamePlay").GetComponent<Tilemap>();
        _grid.UnWalkableMask = LayerMask.GetMask("Player", "Enemy", "UnWalkable");
        _grid.PointBottomLeft = _grid.Map.transform.GetChild(0);
    }

    private void OnEnable()
    {
        _originalPosition = transform.localPosition;
        _originalPosition = _grid.GetNodeFromPos(_originalPosition).centerPos;
        _targetPosition = _originalPosition;
    }

    private void FixedUpdate()
    {
        if (_timer > 0) _timer -= Time.deltaTime;
        MeleeMovement();
    }

    public void MeleeMovement()
    {
        if ((transform.position - _originalPosition).magnitude > _activeArea)
        {
            _isPatience = false;
            TargetObject = null;
            _targetPosition = _originalPosition;
        }
        else
        {
            if (_isPatience)
            {
                Collider2D hit = Physics2D.OverlapCircle(transform.position, _activeArea, _layersTarget);
                if (hit != null)
                    TargetObject = hit.transform;
            }
        }

        if (TargetObject != null)
        {
            _targetPosition = transform.localPosition;
            _pathfinding.FindPathToTransform(transform.position, TargetObject);
        }
        else
        {
            if (transform.position == _originalPosition)
            {
                _isPatience = true;
            }

            if (transform.position == _targetPosition)
            {
                if (_timer <= 0)
                {
                    _timer = _timeIdle;
                    float x = Random.Range(-_activeArea, _activeArea) / 2;
                    float y = Random.Range(-_activeArea, _activeArea) / 2;
                    Vector2 nextTargetPosition = new Vector2(x, y);
                    nextTargetPosition = _grid.GetNodeFromPos(nextTargetPosition).centerPos;
                    _targetPosition = nextTargetPosition;
                }
            }

            _pathfinding.FindPath(transform.position, _targetPosition);
        }

        MoveFollowPathFound();
    }

    private void MoveFollowPathFound()
    {
        if (_grid.path == null || _grid.path.Count <= 0)
        {
            MoveToCellCenter(transform.position);
            return;
        }

        if ((transform.position.x - _grid.path[0].centerPos.x) < 0)
            transform.localScale = Vector3.one;
        if ((transform.position.x - _grid.path[0].centerPos.x) > 0)
            transform.localScale = new Vector3(-1, 1, 1);

        transform.position = Vector2.MoveTowards(transform.position, _grid.path[0].centerPos, MoveSpeed * Time.deltaTime);
    }

    private void MoveToCellCenter(Vector3 pos)
    {
        Vector3Int cell = _grid.Map.WorldToCell(transform.position);
        Vector3 center = _grid.Map.GetCellCenterWorld(cell);
        transform.position = Vector2.MoveTowards(transform.position, center, MoveSpeed * Time.deltaTime);
    }
}
