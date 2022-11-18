using UnityEngine;

[System.Serializable]
public class Skin
{
    [field: SerializeField]
    public SkinBase SkinBase { get; private set; }
    public bool isOwned;
    public bool isUsing;

    public void Buy()
    {
        SkinManager.Instance.Buy(this);
    }

    public void Use()
    {
        SkinManager.Instance.Use(this);
    }
}