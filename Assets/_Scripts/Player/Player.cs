using UnityEngine;

public class Player : MonoBehaviourSingleton<Player>
{
    public PlayerData data = new PlayerData();
    PlayerBase _base = new PlayerBase();

    public int MaxExp => _base.Exp * data.level;

    public int MaxHealth => _base.Health * data.level;
    public int MaxMana => _base.Mana * data.level;

    public int RecoveryHp => _base.RecoveryHealth
                                + Equipment.Instance.RecoveryHp
                                + Mathf.FloorToInt(Equipment.Instance.RecoveryHealthPercent * MaxHealth / 100);

    public int RecoveryMp => _base.RecoveryMana
                                + Equipment.Instance.RecoveryMp
                                + Mathf.FloorToInt(Equipment.Instance.RecoveryManaPercent * MaxMana / 100);

    public int Attack => Equipment.Instance.Attack + data.level * 10;
    public int Armor => Defense + Equipment.Instance.Armor + data.level * 10;
    public int Speed => _base.Speed + Equipment.Instance.Speed + data.level / 2;

    public int Melee => data.melee + Equipment.Instance.Melee;
    public int Distance => data.distance + Equipment.Instance.Distance;
    public int Magic => data.magic + Equipment.Instance.Magic;
    public int Defense => data.defense + Equipment.Instance.Defense;

    [field: SerializeField]
    public PlayerType Type { get; private set; }

    public void ChangeCharacter(PlayerType type)
    {
        Type = type;
        PlayerSkin.Instance.SetSkin(Type);
    }
}

public enum PlayerType
{
    Melee,
    Distance,
    Magic
}
