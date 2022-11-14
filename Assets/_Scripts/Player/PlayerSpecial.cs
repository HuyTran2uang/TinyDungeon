using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpecial : MonoBehaviour
{
    float _areaMeleeDamage = 1f;
    LayerMask _layersTarget;
    GameObject _effectMeleeSpecial;
    IRangeAttack _rangeAttack;

    public int Damage { get; private set; }

    GameObject _physicBullet;
    GameObject _effectMagicSpecial;

    private void Awake()
    {
        _layersTarget = LayerMask.GetMask("Enemy");
        _physicBullet = Resources.Load<GameObject>("Arrow");
        _effectMagicSpecial = Resources.Load<GameObject>("EffectDamage");
    }

    private void Update()
    {
        switch (Player.Instance.Type)
        {
            case PlayerType.Melee:
                Damage = Mathf.FloorToInt((Player.Instance.Melee + Player.Instance.data.level * 10 + Equipment.Instance.Attack) * 1.5f);
                MeleeSpecial();
                break;
            case PlayerType.Distance:
                Damage = Mathf.FloorToInt((Player.Instance.Distance + Player.Instance.data.level * 10 + Equipment.Instance.Attack) * 1.5f);
                break;
            case PlayerType.Magic:
                Damage = Mathf.FloorToInt((Player.Instance.Magic + Player.Instance.data.level * 10 + Equipment.Instance.Attack) * 1.5f);
                break;
        }
    }

    public void MeleeSpecial()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, _areaMeleeDamage, _layersTarget);
        if (hits == null) return;
        foreach (var hit in hits)
        {
            GameObject clone = Instantiate(_effectMeleeSpecial, hit.transform);
            hit.GetComponent<IDamageable>().TakeDamage(Damage);
            Destroy(clone, 0.5f);
        }
    }

    public void DistanceSpecial()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, _rangeAttack.ScopeAttack, _layersTarget);
        if (hits == null) return;
        foreach (var hit in hits)
        {
            GameObject clone = Instantiate(_effectMeleeSpecial, hit.transform);
            hit.GetComponent<IDamageable>().TakeDamage(Damage);
            Destroy(clone, 0.5f);
        }
    }

    public void MagicSpecial()
    {

    }
}
