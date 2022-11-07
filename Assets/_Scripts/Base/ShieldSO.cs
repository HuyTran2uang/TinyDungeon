using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ShieldSO : ItemSO
{
    [field: SerializeField]
    public int LevelRequired { get; set; }

    [field: SerializeField]
    public int Armor { get; set; }

    [field: SerializeField]
    public int Defense { get; set; }
}
