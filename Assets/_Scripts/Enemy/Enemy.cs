using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyBase _base;
    EnemyBar _UIEnemyBar;

    public string Name
    {
        get { return _base.Name; }
    }

    public EnemyType Type
    {
        get { return _base.Type; }
    }

    public int Health
    {
        get { return _base.Health; }
    }

    public int Attack
    {
        get { return _base.Attack; }
    }

    public int Armor
    {
        get { return _base.Armor; }
    }

    public float Speed
    {
        get { return _base.Speed / 100; }
    }

    private void Awake()
    {
        _UIEnemyBar = GetComponentInChildren<EnemyBar>();
    }

    private void OnEnable()
    {
        _UIEnemyBar.SetNameText(Name);
    }
}
