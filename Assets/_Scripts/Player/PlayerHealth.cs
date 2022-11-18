using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviourSingleton<PlayerHealth>, IDamageable, IObserverLevelUp
{
    UIDamageable _damageable;

    private void Awake() => _damageable = GetComponentInChildren<UIDamageable>();

    public int MaxHealth => Player.Instance.MaxHealth;

    public int CurrentHealth
    {
        get { return Player.Instance.data.currentHealth; }
        private set
        {
            Player.Instance.data.currentHealth = value;
            PlayerBar.Instance.SetHealthBar(CurrentHealth, MaxHealth);
        }
    }

    public int RecoveryHp => Player.Instance.RecoveryHp;

    public int Armor => Player.Instance.Armor;

    float _timer, _timeRH = 5f;

    private void Update() => RecoveryHealth();

    private void RecoveryHealth()
    {
        if (CurrentHealth < MaxHealth)
        {
            if (_timer > 0) return;
            _timer = _timeRH;
            if (CurrentHealth + RecoveryHp > MaxHealth)
                CurrentHealth = MaxHealth;
            else
                CurrentHealth += RecoveryHp;
        }
    }

    public void RecoveryHealth(int health)
    {
        if (CurrentHealth + health > MaxHealth)
        {
            health = MaxHealth - CurrentHealth;
            CurrentHealth += health;
        }
        else
            CurrentHealth += health;
    }

    private void FixedUpdate()
    {
        if (_timer > 0) _timer -= Time.deltaTime;
    }

    public void TakeDamage(int damage)
    {
        damage -= Armor;
        if (damage <= 0)
            damage = 0;
        CurrentHealth -= damage;
        _damageable.ShowDamageable(damage);
        if (CurrentHealth <= 0)
            Die();
    }

    private void Die()
    {
        gameObject.SetActive(false);
    }

    public void OnNotifyLevelUp() => CurrentHealth = MaxHealth;
}
