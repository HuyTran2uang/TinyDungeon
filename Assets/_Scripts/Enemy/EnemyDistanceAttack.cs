using UnityEngine;

public class EnemyDistanceAttack : MonoBehaviour, IDistanceAttackable, IRangeAttack
{
    Enemy _enemy;
    ISelectTarget _selectTarget;
    float _cooldown = 2f, _timer;
    bool _isAttacking;
    public float ScopeAttack { get; private set; } = 4f;
    LayerMask _layersTarget;
    public GameObject Bullet { get; private set; }

    public int Damage
    {
        get { return _enemy.Attack; }
    }

    public Transform TargetObject
    {
        get { return _selectTarget.TargetObject; }
    }

    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
        _selectTarget = GetComponent<ISelectTarget>();
        _layersTarget = LayerMask.GetMask("Player");
        Bullet = Resources.Load<GameObject>("Arrow");
    }

    private void FixedUpdate()
    {
        if (_timer > 0) _timer = Time.deltaTime;
    }

    private void Update()
    {
        DistanceAttack();
    }

    public void DistanceAttack()
    {
        if (TargetObject == null) return;
        Collider2D hit = Physics2D.OverlapCircle(transform.position, ScopeAttack, _layersTarget);
        if (hit != null && hit.transform == TargetObject)
        {
            if (_isAttacking) return;
            if (_timer > 0) return;
            _isAttacking = true;
            _timer = _cooldown;
            GameObject clone = Instantiate(Bullet);
            Bullet bullet = clone.GetComponent<Bullet>();
            bullet.Target = TargetObject;
            bullet.Damage = Damage;
            Invoke(nameof(CompleteAttack), 0.5f);
        }
    }

    private void CompleteAttack()
    {
        _isAttacking = false;
    }
}
