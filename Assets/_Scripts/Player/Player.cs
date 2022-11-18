using UnityEngine;

public class Player : MonoBehaviourSingleton<Player>
{
    public PlayerData data = new PlayerData();
    PlayerBase _base = new PlayerBase();

    public int MaxExp => _base.Exp * data.level;

    public int MaxHealth => _base.Health * data.level;

    public int MaxMana => _base.Mana * data.level;

    public int RecoveryHp => _base.RecoveryHealth
                                + EquipmentManager.Instance.RecoveryHp
                                + Mathf.FloorToInt(EquipmentManager.Instance.RecoveryHealthPercent * MaxHealth / 100);

    public int RecoveryMp => _base.RecoveryMana
                                + EquipmentManager.Instance.RecoveryMp
                                + Mathf.FloorToInt(EquipmentManager.Instance.RecoveryManaPercent * MaxMana / 100);

    public int Attack => EquipmentManager.Instance.Attack + data.level * 10;
    public int Armor => Defense + EquipmentManager.Instance.Armor + data.level * 10;
    public int Speed => _base.Speed + EquipmentManager.Instance.Speed + data.level / 2;

    public int Melee => _base.Melee + data.melee + EquipmentManager.Instance.Melee;
    public int Distance => _base.Distance + data.distance + EquipmentManager.Instance.Distance;
    public int Magic => _base.Magic + data.magic + EquipmentManager.Instance.Magic;
    public int Defense => _base.Defense + data.defense + EquipmentManager.Instance.Defense;

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
