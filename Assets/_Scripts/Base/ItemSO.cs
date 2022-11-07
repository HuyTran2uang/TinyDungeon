using UnityEngine;

public class ItemSO : ScriptableObject
{
    [field: SerializeField]
    public bool IsStack { get; set; }

    [field: SerializeField]
    public string Name { get; set; }

    [field: SerializeField]
    public int MaxStackSize { get; set; } = 1;

    [field: SerializeField]
    [field: TextArea]
    public string Description { get; set; }

    [field: SerializeField]
    public Sprite ItemImage { get; set; }
}