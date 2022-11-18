using UnityEngine;

[CreateAssetMenu]
public class EnemyBase : ScriptableObject
{
    [field: SerializeField]
    public string Name { get; private set; }

    [field: SerializeField]
    public EnemyType Type { get; private set; }

    [field: SerializeField]
    public int Health { get; private set; }

    [field: SerializeField]
    public int Attack { get; private set; }

    [field: SerializeField]
    public int Armor { get; private set; }

    [field: SerializeField]
    public int Speed { get; private set; }
}

public enum EnemyType
{
    Melee,
    Distance,
    Magic
}