using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Grid))]
[RequireComponent(typeof(Pathfinding))]
public class EnemyRangeMovement : MonoBehaviour, IRangeMoveable, ISelectTarget
{
    Enemy _enemy;
    IRangeAttack _rangeAttack;

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
    public float ScopeAttack
    {
        get { return _rangeAttack.ScopeAttack; }
    }
    LayerMask _layersTarget;

    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
        _rangeAttack = GetComponent<IRangeAttack>();
        _grid = GetComponent<Grid>();
        _pathfinding = GetComponent<Pathfinding>();

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
        RangeMovement();
    }

    public void RangeMovement()
    {
        if ((transform.position - _originalPosition).magnitude > _activeArea)
        {
            _isPatience = false;
            TargetObject = null;
            _targetPosition = _originalPosition;
        }
        else
        {
            if (!_isPatience) return;
            Collider2D hit = Physics2D.OverlapCircle(transform.position, _activeArea, _layersTarget);
            if (hit != null)
                TargetObject = hit.transform;
        }

        if (transform.position == _targetPosition)
        {
            if (_timer > 0) return;
            _timer = _timeIdle;
            float x = Random.Range(-_activeArea, _activeArea);
            float y = Random.Range(-_activeArea, _activeArea);
            Vector2 nextTargetPosition = new Vector2(x, y);
            _targetPosition = nextTargetPosition;
        }

        if (TargetObject != null)
        {
            _pathfinding.FindPathToTransform(transform.position, TargetObject);
        }
        else
        {
            _pathfinding.FindPath(transform.position, _targetPosition);
        }

        if ((transform.position - _targetPosition).magnitude > ScopeAttack)
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
