using UnityEngine;

public class EnemyMeleeAttack : MonoBehaviour, IMeleeAttackable, ISelectTarget
{
    Enemy _enemy;
    ISelectTarget _selectTarget;
    float _cooldown = 2f, _timer;
    bool _isAttacking;
    float _scopeAttack = 1f;
    LayerMask _layersTarget;
    GameObject _effectSword;

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
        _effectSword = Resources.Load<GameObject>("EffectSword");
    }

    private void FixedUpdate()
    {
        if (_timer > 0) _timer -= Time.deltaTime;
    }

    private void Update()
    {
        MeleeAttack();
    }

    public void MeleeAttack()
    {
        if (TargetObject == null) return;
        Collider2D hit = Physics2D.OverlapCircle(transform.position, _scopeAttack, _layersTarget);
        if (hit != null && hit.transform == TargetObject)
        {
            if (_isAttacking) return;
            if (_timer > 0) return;
            _isAttacking = true;
            _timer = _cooldown;
            GameObject effect = Instantiate(_effectSword, TargetObject);
            hit.GetComponent<IDamageable>().TakeDamage(Damage);
            Destroy(effect, 0.5f);
            Invoke(nameof(CompleteAttack), 0.5f);
        }
    }

    private void CompleteAttack()
    {
        _isAttacking = false;
    }
}
