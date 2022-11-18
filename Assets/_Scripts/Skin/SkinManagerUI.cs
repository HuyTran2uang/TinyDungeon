using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinManagerUI : MonoBehaviour
{
    [SerializeField] private GameObject _skinItemPrefab;
    [SerializeField] private Transform _left;
    [SerializeField] private Transform _mid;
    [SerializeField] private Transform _right;
    public List<Skin> Skins => SkinManager.Instance.Skins;

    private void Awake()
    {
        _skinItemPrefab = Resources.Load<GameObject>("UI/SkinItem");
        _left = transform.Find("Left");
        _mid = transform.Find("Mid");
        _right = transform.Find("Right");
    }

    private void OnEnable()
    {
        Helpers.DestroyChildren(_left);
        Helpers.DestroyChildren(_mid);
        Helpers.DestroyChildren(_right);
        ListSkins();
    }

    private void ListSkins()
    {
        switch (Player.Instance.Type)
        {
            case PlayerType.Melee:
                ListSkinsMelee();
                break;
            case PlayerType.Distance:
                ListSkinsDistance();
                break;
            case PlayerType.Magic:
                ListSkinsMagic();
                break;
        }
    }

    private void ListSkinsMelee()
    {
        foreach (var skin in Skins)
        {
            if (skin.SkinBase.SkinSlot == SkinSlot.MeleeBody)
            {
                GameObject skinItem = Instantiate(_skinItemPrefab, _left);
                SetSkinItem(skinItem, skin);
                continue;
            }

            if (skin.SkinBase.SkinSlot == SkinSlot.MeleeWeapon)
            {
                GameObject skinItem = Instantiate(_skinItemPrefab, _mid);
                SetSkinItem(skinItem, skin);
                continue;
            }

            if (skin.SkinBase.SkinSlot == SkinSlot.Shield)
            {
                GameObject skinItem = Instantiate(_skinItemPrefab, _right);
                SetSkinItem(skinItem, skin);
                continue;
            }
        }
    }

    private void ListSkinsDistance()
    {
        foreach (var skin in Skins)
        {
            if (skin.SkinBase.SkinSlot == SkinSlot.DistanceBody)
            {
                GameObject skinItem = Instantiate(_skinItemPrefab, _left);
                SetSkinItem(skinItem, skin);
                continue;
            }

            if (skin.SkinBase.SkinSlot == SkinSlot.DistanceWeapon)
            {
                GameObject skinItem = Instantiate(_skinItemPrefab, _mid);
                SetSkinItem(skinItem, skin);
                continue;
            }
        }
    }

    private void ListSkinsMagic()
    {
        foreach (var skin in Skins)
        {
            if (skin.SkinBase.SkinSlot == SkinSlot.MagicBody)
            {
                GameObject skinItem = Instantiate(_skinItemPrefab, _left);
                SetSkinItem(skinItem, skin);
                continue;
            }

            if (skin.SkinBase.SkinSlot == SkinSlot.MagicWeapon)
            {
                GameObject skinItem = Instantiate(_skinItemPrefab, _mid);
                SetSkinItem(skinItem, skin);
                continue;
            }
        }
    }

    private void SetSkinItem(GameObject skinItem, Skin skin)
    {
        Button button = skinItem.GetComponent<Button>();
        Image image = skinItem.transform.Find("Image").GetComponent<Image>();
        Text notify = skinItem.transform.Find("Notify").GetComponent<Text>();
        button.onClick.AddListener(() => ShowSkin(skin));
        //set
        if (!skin.isOwned && !skin.isUsing)
        {
            image.sprite = skin.SkinBase.Image;
            notify.text = $"{skin.SkinBase.PriceBuy} Dia";
        }
        else if (!skin.isUsing)
        {
            image.sprite = skin.SkinBase.Image;
            notify.text = "Owned";
        }
        else
        {
            image.sprite = skin.SkinBase.Image;
            notify.text = "Using";
        }
    }

    private void ShowSkin(Skin skin)
    {
        MenuManager.Instance.OpenMenu("Item");
        ItemMenu.Instance.buttonDisabled.onClick.AddListener(() => OpenSkinMenu());
        ItemMenu.Instance.buttonBack.onClick.AddListener(() => OpenSkinMenu());
        ItemMenu.Instance.image.sprite = skin.SkinBase.Image;
        ItemMenu.Instance.description.text = $"{skin.SkinBase.Name}\n{skin.SkinBase.PriceBuy} Diamond";
        if (skin.isOwned && !skin.isUsing)
        {
            ItemMenu.Instance.buttonUse.gameObject.SetActive(true);
            ItemMenu.Instance.buttonUse.onClick.AddListener(() => skin.Use());
        }
        if (!skin.isOwned)
        {
            ItemMenu.Instance.buttonBuy.gameObject.SetActive(true);
            ItemMenu.Instance.buttonBuy.onClick.AddListener(() => skin.Buy());
        }
    }

    private void OpenSkinMenu()
    {
        MenuManager.Instance.OpenMenu("Skin");
    }
}