using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class WeaponSO : ItemSO
{
    [field: SerializeField]
    public int LevelRequired { get; set; }

    [field: SerializeField]
    public int Attack { get; set; }

    [field: SerializeField]
    public int Melee { get; set; }

    [field: SerializeField]
    public int Distance { get; set; }

    [field: SerializeField]
    public int Magic { get; set; }
}