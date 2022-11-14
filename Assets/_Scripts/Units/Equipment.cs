using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviourSingleton<Equipment>
{
    public List<EquipmentSO> Items = new List<EquipmentSO>();

    PlayerType Type => Player.Instance.Type;

    public int Attack
    {
        get
        {
            int value = 0;
            foreach (var item in Items)
            {
                if (Type == PlayerType.Melee)
                {
                    if (item.EquipmentSlot == EquipmentSlot.DistanceWeapon) continue;
                    if (item.EquipmentSlot == EquipmentSlot.MagicWeapon) continue;
                    value += item.Attack;
                }
                if (Type == PlayerType.Distance)
                {
                    if (item.EquipmentSlot == EquipmentSlot.MeleeWeapon) continue;
                    if (item.EquipmentSlot == EquipmentSlot.MagicWeapon) continue;
                    value += item.Attack;
                }
                if (Type == PlayerType.Magic)
                {
                    if (item.EquipmentSlot == EquipmentSlot.MeleeWeapon) continue;
                    if (item.EquipmentSlot == EquipmentSlot.DistanceWeapon) continue;
                    value += item.Attack;
                }
            }
            return value;
        }
    }
    public int Armor
    {
        get
        {
            int value = 0;
            foreach (var item in Items)
            {
                if (Type != PlayerType.Melee)
                    if (item.EquipmentSlot == EquipmentSlot.Shield) continue;
                value += item.Armor;
            }
            return value;
        }
    }
    public int Speed
    {
        get
        {
            int value = 0;
            foreach (var item in Items)
            {
                value += item.Speed;
            }
            return value;
        }
    }
    public int RecoveryHp
    {
        get
        {
            int value = 0;
            foreach (var item in Items)
            {
                value += item.RecoveryHp;
            }
            return value;
        }
    }
    public int RecoveryMp
    {
        get
        {
            int value = 0;
            foreach (var item in Items)
            {
                value += item.RecoveryMp;
            }
            return value;
        }
    }
    public float RecoveryHealthPercent
    {
        get
        {
            float value = 0;
            foreach (var item in Items)
            {
                value += item.RecoveryHealthPercent;
            }
            return value;
        }
    }
    public float RecoveryManaPercent
    {
        get
        {
            float value = 0;
            foreach (var item in Items)
            {
                value += item.RecoveryManaPercent;
            }
            return value;
        }
    }
    public int Melee
    {
        get
        {
            int value = 0;
            foreach (var item in Items)
            {
                value += item.Melee;
            }
            return value;
        }
    }
    public int Distance
    {
        get
        {
            int value = 0;
            foreach (var item in Items)
            {
                value += item.Distance;
            }
            return value;
        }
    }
    public int Magic
    {
        get
        {
            int value = 0;
            foreach (var item in Items)
            {
                value += item.Magic;
            }
            return value;
        }
    }
    public int Defense
    {
        get
        {
            int value = 0;
            foreach (var item in Items)
            {
                value += item.Defense;
            }
            return value;
        }
    }

    public void Wear(EquipmentSO equipment)
    {
        foreach (var item in Items)
        {
            if (equipment.LevelRequired > Player.Instance.data.level) return;

            if (item.EquipmentSlot == equipment.EquipmentSlot)
            {
                //push to inventory
                Items.Remove(item);
            }
            Items.Add(equipment);
        }
    }

    public void Remove(EquipmentSO equipment)
    {
        foreach (var item in Items)
        {
            if (equipment.EquipmentSlot == EquipmentSlot.MeleeWeapon) return;
            if (equipment.EquipmentSlot == EquipmentSlot.DistanceWeapon) return;
            if (equipment.EquipmentSlot == EquipmentSlot.MagicWeapon) return;
            if (equipment.EquipmentSlot == EquipmentSlot.Shield) return;
            Items.Remove(equipment);
        }
    }
}
