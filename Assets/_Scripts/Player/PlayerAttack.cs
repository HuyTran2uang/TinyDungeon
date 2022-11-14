using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    ISelectTarget _selectTarget;

    public int Damage { get; private set; }

    public float ScopeAttack { get; private set; }
    LayerMask _layersTarget;
    [SerializeField] private WeaponCtrl _weaponCtrl;

    public Transform TargetObject => _selectTarget.TargetObject;

    private void Awake()
    {
        _selectTarget = GetComponent<ISelectTarget>();
        _layersTarget = LayerMask.GetMask("Enemy");
        _weaponCtrl = GetComponentInChildren<WeaponCtrl>();
        _effectSword = Resources.Load<GameObject>("EffectSword");
        _bulletPhysic = Resources.Load<GameObject>("Arrow");
        _bulletMagic = Resources.Load<GameObject>("FireMagic");
    }

    private void Update()
    {
        switch (Player.Instance.Type)
        {
            case PlayerType.Melee:
                ScopeAttack = 1f;
                Damage = Player.Instance.Attack + Player.Instance.Melee;
                MeleeAttack();
                break;
            case PlayerType.Distance:
                ScopeAttack = 5f;
                Damage = Player.Instance.Attack + Player.Instance.Distance;
                DistanceAttack();
                break;
            case PlayerType.Magic:
                ScopeAttack = 5f;
                Damage = Player.Instance.Attack + Player.Instance.Magic;
                MagicAttack();
                break;
        }
    }

    private void FixedUpdate()
    {
        if (_timer > 0) _timer -= Time.deltaTime;
    }

    float _cooldown = 2f, _timer = 0;

    [SerializeField] private GameObject _effectSword;
    private void MeleeAttack()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, ScopeAttack, _layersTarget);
        foreach (var hit in hits)
            if (hit != null && hit.transform == TargetObject)
            {
                if (_timer > 0) return;
                _timer = _cooldown;
                _weaponCtrl.Attack();
                GameObject effect = Instantiate(_effectSword, TargetObject);
                hit.GetComponent<IDamageable>().TakeDamage(Damage);
                Destroy(effect, 0.5f);
            }
    }

    [SerializeField] private GameObject _bulletPhysic;
    private void DistanceAttack()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, ScopeAttack, _layersTarget);
        foreach (var hit in hits)
            if (hit != null && hit.transform == TargetObject)
            {
                if (_timer > 0) return;
                _timer = _cooldown;
                _weaponCtrl.Attack();
                GameObject bulletPhysic = Instantiate(_bulletPhysic);
                Bullet bullet = bulletPhysic.GetComponent<Bullet>();
                bullet.Damage = Damage;
                bullet.Target = TargetObject;
            }
    }

    [SerializeField] private GameObject _bulletMagic;
    int _manaToUseMagicAttack = 4;
    private void MagicAttack()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, ScopeAttack, _layersTarget);
        foreach (var hit in hits)
            if (hit != null && hit.transform == TargetObject)
            {
                if (_timer > 0) return;
                _timer = _cooldown;
                _weaponCtrl.Attack();
                GameObject bulletMagic = Instantiate(_bulletMagic);
                PlayerMana.Instance.UseMana(_manaToUseMagicAttack);
                Bullet bullet = bulletMagic.GetComponent<Bullet>();
                bullet.Damage = Damage;
                bullet.Target = TargetObject;
            }
    }
}
