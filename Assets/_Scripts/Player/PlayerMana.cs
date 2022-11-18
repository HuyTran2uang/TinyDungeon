using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMana : MonoBehaviourSingleton<PlayerMana>, IObserverLevelUp
{
    public int MaxMana => Player.Instance.MaxMana;

    public int CurrentMana
    {
        get { return Player.Instance.data.currentMana; }
        private set
        {
            Player.Instance.data.currentMana = value;
            PlayerBar.Instance.SetManaBar(CurrentMana, MaxMana);
        }
    }

    public int RecoveryMp => Player.Instance.RecoveryMp;

    public void UseMana(int mana)
    {
        if (mana > CurrentMana) return;
        else CurrentMana -= mana;
    }

    float _timer, _timeRM = 5f;

    private void Update() => RecoveryMana();

    private void FixedUpdate()
    {
        if (_timer > 0) _timer -= Time.deltaTime;
    }

    private void RecoveryMana()
    {
        if (CurrentMana < MaxMana)
        {
            if (_timer > 0) return;
            _timer = _timeRM;
            if (CurrentMana + RecoveryMp > MaxMana)
                CurrentMana = MaxMana;
            else
                CurrentMana += RecoveryMp;
        }
    }

    public void RecoveryMana(int mana)
    {
        if (CurrentMana + mana > MaxMana)
        {
            mana = MaxMana - CurrentMana;
            CurrentMana += mana;
        }
        else
            CurrentMana += mana;
    }

    public void OnNotifyLevelUp() => CurrentMana = MaxMana;
}
