using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    Enemy _enemy;
    EnemyBar _UIEnemyBar;

    public int MaxHealth
    {
        get { return _enemy.Health; }
    }

    [SerializeField] private int _currentHealth;
    public int CurrentHealth
    {
        get { return _currentHealth; }
        private set
        {
            _currentHealth = value;
            _UIEnemyBar.SetHealthBar(CurrentHealth, MaxHealth);
        }
    }

    public int Armor
    {
        get { return _enemy.Armor; }
    }

    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
        _UIEnemyBar = GetComponentInChildren<EnemyBar>();
    }

    private void OnEnable()
    {
        CurrentHealth = MaxHealth;
    }

    public void TakeDamage(int damage)
    {
        damage -= Armor;
        CurrentHealth -= damage;
        if (CurrentHealth <= 0)
            Die();
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
