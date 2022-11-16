using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinManager : MonoBehaviourSingleton<SkinManager>
{
    public Skin[] skins;

    public Skin GetSkinMeleeBody()
    {
        foreach (var i in skins)
            if (i.isOwned && i.isUsing && i.skinSO.SkinSlot == SkinSlot.MeleeBody)
                return i;
        return null;
    }

    public Skin GetSkinDistanceBody()
    {
        foreach (var i in skins)
            if (i.isOwned && i.isUsing && i.skinSO.SkinSlot == SkinSlot.DistanceBody)
                return i;
        return null;
    }

    public Skin GetSkinMagicBody()
    {
        foreach (var i in skins)
            if (i.isOwned && i.isUsing && i.skinSO.SkinSlot == SkinSlot.MagicBody)
                return i;
        return null;
    }

    public Skin GetSkinMeleeWeapon()
    {
        foreach (var i in skins)
            if (i.isOwned && i.isUsing && i.skinSO.SkinSlot == SkinSlot.MeleeWeapon)
                return i;
        return null;
    }

    public Skin GetSkinDistanceWeapon()
    {
        foreach (var i in skins)
            if (i.isOwned && i.isUsing && i.skinSO.SkinSlot == SkinSlot.DistanceWeapon)
                return i;
        return null;
    }

    public Skin GetSkinMagicWeapon()
    {
        foreach (var i in skins)
            if (i.isOwned && i.isUsing && i.skinSO.SkinSlot == SkinSlot.MagicWeapon)
                return i;
        return null;
    }

    public Skin GetSkinShield()
    {
        foreach (var i in skins)
            if (i.isOwned && i.isUsing && i.skinSO.SkinSlot == SkinSlot.Shield)
                return i;
        return null;
    }
}
