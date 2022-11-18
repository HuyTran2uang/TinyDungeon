using UnityEngine;

[CreateAssetMenu]
public class SkinBase : ScriptableObject
{
    public int Id => GetInstanceID();

    [field: SerializeField]
    public string Name { get; private set; }

    [field: SerializeField]
    public SkinSlot SkinSlot { get; private set; }

    [field: SerializeField]
    public Sprite Image { get; private set; }

    [field: SerializeField]
    public int PriceBuy { get; private set; }
}

public enum SkinSlot
{
    MeleeBody,
    DistanceBody,
    MagicBody,
    MeleeWeapon,
    DistanceWeapon,
    MagicWeapon,
    Shield
}