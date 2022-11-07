using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviourSingleton<Equipment>
{
    [Header("Skin")]
    public Sprite skinMelee;
    public Sprite skinDistance;
    public Sprite skinMagic;
    [Header("Set")]
    public WeaponSO WeaponMelee;
    public ShieldSO Shield;
    public WeaponSO WeaponDistance;
    public WeaponSO WeaponMagic;
}
