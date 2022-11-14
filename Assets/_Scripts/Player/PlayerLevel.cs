using UnityEngine;

public class PlayerLevel : MonoBehaviour
{
    IObserverLevelUp[] _observers;
    UIShortInfo _shortInfo;

    private void Awake()
    {
        _observers = GetComponents<IObserverLevelUp>();
        _shortInfo = GetComponentInChildren<UIShortInfo>();
    }

    public int Level
    {
        get { return Player.Instance.data.level; }
        private set
        {
            Player.Instance.data.level = value;
            _shortInfo.SetLevelText(Level);
        }
    }

    public int MaxExp => Player.Instance.MaxExp;

    public int CurrentExp
    {
        get { return Player.Instance.data.currentExp; }
        private set { Player.Instance.data.currentExp = value; }
    }

    private void Start() => _shortInfo.SetLevelText(Level);

    public void ReceiveExp(int exp)
    {
        if (CurrentExp + exp > MaxExp)
        {
            LevelUp();
            ReceiveExp(CurrentExp + exp - MaxExp);
            return;
        }
        if (CurrentExp + exp == MaxExp)
        {
            LevelUp();
            return;
        }
        if (CurrentExp + exp == MaxExp)
        {
            CurrentExp += exp;
            return;
        }
    }

    private void LevelUp()
    {
        Level++;
        CurrentExp = 0;
        foreach (var observer in _observers)
            observer.OnNotifyLevelUp();
    }
}
