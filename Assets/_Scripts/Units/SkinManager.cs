using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinManager : MonoBehaviourSingleton<SkinManager>
{
    [SerializeField] List<Skin> _skins;
    [SerializeField] private GameObject _skinPrefabs;

    private void Awake() => _skinPrefabs = Resources.Load<GameObject>("UI/SkinItem");

    private void Start() => SetListSkins();

    private void SetListSkins()
    {
        foreach (var i in _skins)
        {
            GameObject skinPrefab = Instantiate(_skinPrefabs, transform);
            Text notify = skinPrefab.transform.Find("Notify").GetComponent<Text>();
            Image image = skinPrefab.transform.Find("Image").GetComponent<Image>();
            Button btnBuy = skinPrefab.transform.Find("ButtonBuy").GetComponent<Button>();
            Button btnUse = skinPrefab.transform.Find("ButtonUse").GetComponent<Button>();
            //set image
            image.sprite = i.skinSO.Image;
            image.preserveAspect = true;
            //set onclick
            btnBuy.onClick.AddListener(() => Buy(i));
            btnUse.onClick.AddListener(() => Use(i));
            //
            if (!i.isOwned)
            {
                btnBuy.gameObject.SetActive(true);
                btnUse.gameObject.SetActive(false);
                notify.text = ($"{i.skinSO.PriceBuy} dia");
            }
            else
            {
                if (i.isUsing)
                {
                    btnUse.gameObject.SetActive(false);
                    notify.text = "using";
                }

                Destroy(btnBuy);
                btnBuy.gameObject.SetActive(false);
                btnUse.gameObject.SetActive(true);
                notify.text = "owned";
            }
        }
    }

    public void Buy(Skin skin)
    {
        for (int i = 0; i < _skins.Count; i++)
        {
            if (_skins[i] == skin && !_skins[i].isOwned)
            {
                _skins[i].isOwned = true;
                var btnBuy = transform.GetChild(i).transform.Find("ButtonBuy").gameObject;
                var btnUse = transform.GetChild(i).transform.Find("ButtonUse").gameObject;
                var notify = transform.GetChild(i).transform.Find("Notify").GetComponent<Text>();
                btnBuy.SetActive(false);
                btnUse.SetActive(true);
                notify.text = "owned";
                return;
            }
        }
    }

    public void Use(Skin skin)
    {
        for (int i = 0; i < _skins.Count; i++)
        {
            if (_skins[i] == skin && !_skins[i].isUsing)
            {
                _skins[i].isUsing = true;
                var btnUse = transform.GetChild(i).transform.Find("ButtonUse").gameObject;
                var notify = transform.GetChild(i).transform.Find("Notify").GetComponent<Text>();
                btnUse.SetActive(false);
                notify.text = "using";
            }
            if (_skins[i].isOwned && _skins[i].skinSO.SkinSlot == skin.skinSO.SkinSlot && _skins[i] != skin)
            {
                _skins[i].isUsing = false;
                var btnUse = transform.GetChild(i).transform.Find("ButtonUse").gameObject;
                var notify = transform.GetChild(i).transform.Find("Notify").GetComponent<Text>();
                btnUse.SetActive(true);
                notify.text = "owned";
            }
        }
    }

    public Skin GetSkinMeleeBody()
    {
        foreach (var i in _skins)
            if (i.isOwned && i.isUsing && i.skinSO.SkinSlot == SkinSlot.MeleeBody)
                return i;
        return null;
    }

    public Skin GetSkinDistanceBody()
    {
        foreach (var i in _skins)
            if (i.isOwned && i.isUsing && i.skinSO.SkinSlot == SkinSlot.DistanceBody)
                return i;
        return null;
    }

    public Skin GetSkinMagicBody()
    {
        foreach (var i in _skins)
            if (i.isOwned && i.isUsing && i.skinSO.SkinSlot == SkinSlot.MagicBody)
                return i;
        return null;
    }

    public Skin GetSkinMeleeWeapon()
    {
        foreach (var i in _skins)
            if (i.isOwned && i.isUsing && i.skinSO.SkinSlot == SkinSlot.MeleeWeapon)
                return i;
        return null;
    }

    public Skin GetSkinDistanceWeapon()
    {
        foreach (var i in _skins)
            if (i.isOwned && i.isUsing && i.skinSO.SkinSlot == SkinSlot.DistanceWeapon)
                return i;
        return null;
    }

    public Skin GetSkinMagicWeapon()
    {
        foreach (var i in _skins)
            if (i.isOwned && i.isUsing && i.skinSO.SkinSlot == SkinSlot.MagicWeapon)
                return i;
        return null;
    }

    public Skin GetSkinShield()
    {
        foreach (var i in _skins)
            if (i.isOwned && i.isUsing && i.skinSO.SkinSlot == SkinSlot.Shield)
                return i;
        return null;
    }
}
