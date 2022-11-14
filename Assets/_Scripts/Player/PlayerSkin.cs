using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkin : MonoBehaviourSingleton<PlayerSkin>
{
    [SerializeField] private SpriteRenderer _modelRender;
    [SerializeField] private SpriteRenderer _weaponRender;
    [SerializeField] private SpriteRenderer _shieldRender;

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
                _modelRender.sprite = SkinManager.Instance.GetSkinMeleeBody().skinSO.Image;
                _weaponRender.sprite = SkinManager.Instance.GetSkinMeleeWeapon().skinSO.Image;
                _shieldRender.gameObject.SetActive(true);
                _shieldRender.sprite = SkinManager.Instance.GetSkinShield().skinSO.Image;
                break;
            case PlayerType.Distance:
                _modelRender.sprite = SkinManager.Instance.GetSkinDistanceBody().skinSO.Image;
                _weaponRender.sprite = SkinManager.Instance.GetSkinDistanceWeapon().skinSO.Image;
                _shieldRender.gameObject.SetActive(false);
                break;
            case PlayerType.Magic:
                _modelRender.sprite = SkinManager.Instance.GetSkinMagicBody().skinSO.Image;
                _weaponRender.sprite = SkinManager.Instance.GetSkinMagicWeapon().skinSO.Image;
                _shieldRender.gameObject.SetActive(false);
                break;
        }
    }
}
