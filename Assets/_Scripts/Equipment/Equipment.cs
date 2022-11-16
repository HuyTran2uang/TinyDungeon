using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviourSingleton<Equipment>
{
    public List<EquipmentSO> items;

    PlayerType Type => Player.Instance.Type;

    public int Attack
    {
        get
        {
            int value = 0;
            foreach (var item in items)
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
            foreach (var item in items)
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
            foreach (var item in items)
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
            foreach (var item in items)
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
            foreach (var item in items)
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
            foreach (var item in items)
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
            foreach (var item in items)
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
            foreach (var item in items)
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
            foreach (var item in items)
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
            foreach (var item in items)
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
            foreach (var item in items)
            {
                value += item.Defense;
            }
            return value;
        }
    }

    public void Drop(EquipmentSO item)
    {
        //drop item to map 
        foreach (var i in items)
        {
            if (item.EquipmentSlot == EquipmentSlot.MeleeWeapon) return;
            if (item.EquipmentSlot == EquipmentSlot.DistanceWeapon) return;
            if (item.EquipmentSlot == EquipmentSlot.MagicWeapon) return;
            if (item.EquipmentSlot == EquipmentSlot.Shield) return;
            items.Remove(item);
        }
    }

    public void Remove(EquipmentSO item)
    {
        //remove item to inventory
        foreach (var i in items)
        {
            if (item.EquipmentSlot == EquipmentSlot.MeleeWeapon) return;
            if (item.EquipmentSlot == EquipmentSlot.DistanceWeapon) return;
            if (item.EquipmentSlot == EquipmentSlot.MagicWeapon) return;
            if (item.EquipmentSlot == EquipmentSlot.Shield) return;
            items.Remove(item);
        }
    }
}
