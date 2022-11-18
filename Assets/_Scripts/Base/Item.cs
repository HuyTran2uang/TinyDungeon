using UnityEngine;

public class Item : ScriptableObject
{
    public int Id => GetInstanceID();

    [field: SerializeField]
    public bool IsStack { get; private set; }

    [field: SerializeField]
    public string Name { get; private set; }

    [field: SerializeField]
    public int MaxStackSize { get; private set; } = 1;

    [field: SerializeField]
    [field: TextArea]
    public string Description { get; private set; }

    [field: SerializeField]
    public Sprite ItemImage { get; private set; }

    [field: SerializeField]
    public int PriceBuy { get; private set; }

    [field: SerializeField]
    public int PriceSell { get; private set; }

    public virtual void Use(Item item) { }

    public virtual void RemoveFromInventory()
    {
        //
    }
}