using System.Collections.Generic;
using UnityEngine;

public class SkinManager : MonoBehaviourSingleton<SkinManager>
{
    [SerializeField] List<Skin> _skins = new List<Skin>();

    public List<Skin> Skins => _skins;

    public void Buy(Skin skin)
    {
        foreach (var i in Skins)
        {
            if (i != skin) continue;
            if (Player.Instance.data.diamond < skin.SkinBase.PriceBuy) return;
            Player.Instance.data.diamond -= skin.SkinBase.PriceBuy;
            i.isOwned = true;
            MenuManager.Instance.OpenMenu("Skin");
            return;
        }
    }

    public void Use(Skin skin)
    {
        foreach (var i in Skins)
        {
            if (i == skin) i.isUsing = true;
            if (i.isOwned && i.isUsing && i.SkinBase.SkinSlot == skin.SkinBase.SkinSlot && i != skin)
            {
                i.isUsing = false;
            }
            MenuManager.Instance.OpenMenu("Skin");
        }
    }
}