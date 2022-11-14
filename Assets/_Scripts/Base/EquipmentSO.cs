using UnityEngine;

[CreateAssetMenu]
public class EquipmentSO : ItemSO
{
    [field: SerializeField]
    public EquipmentSlot EquipmentSlot { get; private set; }

    [field: SerializeField]
    public int LevelRequired { get; private set; } = 1;

    [field: SerializeField]
    public int Melee { get; private set; }

    [field: SerializeField]
    public int Distance { get; private set; }

    [field: SerializeField]
    public int Magic { get; private set; }

    [field: SerializeField]
    public int Defense { get; private set; }

    [field: SerializeField]
    public int Attack { get; private set; }

    [field: SerializeField]
    public int Armor { get; private set; }

    [field: SerializeField]
    public int Speed { get; private set; }

    [field: SerializeField]
    public int RecoveryHp { get; private set; }

    [field: SerializeField]
    public int RecoveryMp { get; private set; }

    [field: SerializeField]
    public float RecoveryHealthPercent { get; private set; }

    [field: SerializeField]
    public float RecoveryManaPercent { get; private set; }
}

public enum EquipmentSlot
{
    MeleeWeapon = 0,
    DistanceWeapon = 1,
    MagicWeapon = 2,
    Shield = 3,
    Helmet = 4,
    Armor = 5,
    Shoes = 6,
    Gloves = 7,
    Necklace = 8,
    Ring = 9
}
