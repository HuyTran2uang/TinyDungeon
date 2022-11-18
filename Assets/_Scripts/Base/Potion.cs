using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : Item
{
    [field: SerializeField]
    public PotionType Type { get; private set; }
    [field: SerializeField]
    public int HP { get; private set; }
    [field: SerializeField]
    public int MP { get; private set; }

    public override void Use(Item item)
    {
        if (Type == PotionType.HpPotion)
            PlayerHealth.Instance.RecoveryHealth(HP);
        if (Type == PotionType.MpPotion)
            PlayerMana.Instance.RecoveryMana(MP);
    }
}

public enum PotionType
{
    HpPotion,
    MpPotion
}
