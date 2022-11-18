using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkin : MonoBehaviourSingleton<PlayerSkin>
{
    [SerializeField] private SpriteRenderer _modelRender;
    [SerializeField] private SpriteRenderer _weaponRender;
    [SerializeField] private SpriteRenderer _shieldRender;
    public List<Skin> Skins => SkinManager.Instance.Skins;

    private void Awake()
    {
        _modelRender = transform.Find("Model").GetComponent<SpriteRenderer>();
        _weaponRender = _modelRender.transform.Find("Weapon").GetComponent<SpriteRenderer>();
        _shieldRender = _modelRender.transform.Find("Shield").GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        SetSkin(Player.Instance.Type);
    }

    public void SetSkin(PlayerType type)
    {
        switch (type)
        {
            case PlayerType.Melee:
                SetSkinsMelee();
                break;
            case PlayerType.Distance:
                SetSkinsDistance();
                break;
            case PlayerType.Magic:
                SetSkinsMagic();
                break;
        }
    }

    private void SetSkinsMelee()
    {
        foreach (var skin in Skins)
        {
            if (!skin.isUsing) continue;
            _shieldRender.gameObject.SetActive(true);
            if (skin.SkinBase.SkinSlot == SkinSlot.MeleeBody)
                _modelRender.sprite = skin.SkinBase.Image;
            if (skin.SkinBase.SkinSlot == SkinSlot.MeleeWeapon)
                _weaponRender.sprite = skin.SkinBase.Image;
            if (skin.SkinBase.SkinSlot == SkinSlot.Shield)
                _shieldRender.sprite = skin.SkinBase.Image;
        }
    }

    private void SetSkinsDistance()
    {
        foreach (var skin in Skins)
        {
            if (!skin.isUsing) continue;
            _shieldRender.gameObject.SetActive(false);
            if (skin.SkinBase.SkinSlot == SkinSlot.DistanceBody)
                _modelRender.sprite = skin.SkinBase.Image;
            if (skin.SkinBase.SkinSlot == SkinSlot.DistanceWeapon)
                _weaponRender.sprite = skin.SkinBase.Image;
        }
    }

    private void SetSkinsMagic()
    {
        foreach (var skin in Skins)
        {
            if (!skin.isUsing) continue;
            _shieldRender.gameObject.SetActive(false);
            if (skin.SkinBase.SkinSlot == SkinSlot.MagicBody)
                _modelRender.sprite = skin.SkinBase.Image;
            if (skin.SkinBase.SkinSlot == SkinSlot.MagicWeapon)
                _weaponRender.sprite = skin.SkinBase.Image;
        }
    }
}
