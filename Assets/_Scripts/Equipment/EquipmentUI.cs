using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentUI : MonoBehaviour
{
    public List<EquipmentSO> items;
    public Image meleeWeapon;
    public Image distanceWeapon;
    public Image magicWeapon;
    public Image shield;
    public Image helmet;
    public Image armor;
    public Image gloves;
    public Image shoes;
    public Image necklace;
    public Image ring1;
    public Image ring2;

    private void OnEnable()
    {
        meleeWeapon.gameObject.SetActive(false);
        distanceWeapon.gameObject.SetActive(false);
        magicWeapon.gameObject.SetActive(false);
        shield.gameObject.SetActive(false);
        helmet.gameObject.SetActive(false);
        armor.gameObject.SetActive(false);
        gloves.gameObject.SetActive(false);
        shoes.gameObject.SetActive(false);
        necklace.gameObject.SetActive(false);
        ring1.gameObject.SetActive(false);
        ring2.gameObject.SetActive(false);

        items = Equipment.Instance.items;
        SetListItemEquip();
    }

    private void SetListItemEquip()
    {
        foreach (var item in items)
        {
            //melee weapon
            if (item.EquipmentSlot == EquipmentSlot.MeleeWeapon)
            {
                var btn = meleeWeapon.transform.parent.GetComponent<Button>();
                btn.onClick.AddListener(() => OnSelectButtonClicked(item));

                meleeWeapon.gameObject.SetActive(true);
                meleeWeapon.sprite = item.ItemImage;
                meleeWeapon.preserveAspect = true;
            }
            //distance weapon
            if (item.EquipmentSlot == EquipmentSlot.DistanceWeapon)
            {
                var btn = distanceWeapon.transform.parent.GetComponent<Button>();
                btn.onClick.AddListener(() => OnSelectButtonClicked(item));

                distanceWeapon.gameObject.SetActive(true);
                distanceWeapon.sprite = item.ItemImage;
                distanceWeapon.preserveAspect = true;
            }
            //magic weapon
            if (item.EquipmentSlot == EquipmentSlot.MagicWeapon)
            {
                var btn = magicWeapon.transform.parent.GetComponent<Button>();
                btn.onClick.AddListener(() => OnSelectButtonClicked(item));

                magicWeapon.gameObject.SetActive(true);
                magicWeapon.sprite = item.ItemImage;
                magicWeapon.preserveAspect = true;
            }
            //shield
            if (item.EquipmentSlot == EquipmentSlot.Shield)
            {
                var btn = shield.transform.parent.GetComponent<Button>();
                btn.onClick.AddListener(() => OnSelectButtonClicked(item));

                shield.gameObject.SetActive(true);
                shield.sprite = item.ItemImage;
                shield.preserveAspect = true;
            }
            //helmet
            if (item.EquipmentSlot == EquipmentSlot.Helmet)
            {
                var btn = helmet.transform.parent.GetComponent<Button>();
                btn.onClick.AddListener(() => OnSelectButtonClicked(item));

                helmet.gameObject.SetActive(true);
                helmet.sprite = item.ItemImage;
                helmet.preserveAspect = true;
            }
            //armor
            if (item.EquipmentSlot == EquipmentSlot.Armor)
            {
                var btn = armor.transform.parent.GetComponent<Button>();
                btn.onClick.AddListener(() => OnSelectButtonClicked(item));

                armor.gameObject.SetActive(true);
                armor.sprite = item.ItemImage;
                armor.preserveAspect = true;
            }
            //gloves
            if (item.EquipmentSlot == EquipmentSlot.Gloves)
            {
                var btn = gloves.transform.parent.GetComponent<Button>();
                btn.onClick.AddListener(() => OnSelectButtonClicked(item));

                gloves.gameObject.SetActive(true);
                gloves.sprite = item.ItemImage;
                gloves.preserveAspect = true;
            }
            //shoes
            if (item.EquipmentSlot == EquipmentSlot.Shoes)
            {
                var btn = shoes.transform.parent.GetComponent<Button>();
                btn.onClick.AddListener(() => OnSelectButtonClicked(item));

                shoes.gameObject.SetActive(true);
                shoes.sprite = item.ItemImage;
                shoes.preserveAspect = true;
            }
            //necklace
            if (item.EquipmentSlot == EquipmentSlot.Necklace)
            {
                var btn = necklace.transform.parent.GetComponent<Button>();
                btn.onClick.AddListener(() => OnSelectButtonClicked(item));

                necklace.gameObject.SetActive(true);
                necklace.sprite = item.ItemImage;
                necklace.preserveAspect = true;
            }
            //ring
            if (item.EquipmentSlot == EquipmentSlot.Ring)
            {
                if (ring1.sprite == null)
                {
                    var btn = ring1.transform.parent.GetComponent<Button>();
                    btn.onClick.AddListener(() => OnSelectButtonClicked(item));

                    ring1.gameObject.SetActive(true);
                    ring1.sprite = item.ItemImage;
                    ring1.preserveAspect = true;
                }
                else
                {
                    var btn = ring2.transform.parent.GetComponent<Button>();
                    btn.onClick.AddListener(() => OnSelectButtonClicked(item));

                    ring2.gameObject.SetActive(true);
                    ring2.sprite = item.ItemImage;
                    ring2.preserveAspect = true;
                }
            }
        }
    }

    private void OnSelectButtonClicked(EquipmentSO item)
    {
        MenuManager.Instance.OpenMenu("Item");
        var menus = FindObjectsOfType<Menu>();
        foreach (var menu in menus)
        {
            if (menu.Name != "Item") continue;
            var btnDisabled = menu.GetComponent<Button>();
            var btnBack = menu.transform.Find("ButtonBack").GetComponent<Button>();
            var image = menu.transform.Find("Image").GetComponent<Image>();
            var listStats = FindObjectOfType<ListStats>();
            var listBtn = menu.transform.Find("ListButton");
            var btnDrop = listBtn.transform.Find("ButtonDrop").GetComponent<Button>();
            var btnRemove = listBtn.transform.Find("ButtonRemove").GetComponent<Button>();
            var btnUse = listBtn.transform.Find("ButtonUse").GetComponent<Button>();

            if (item.EquipmentSlot == EquipmentSlot.MeleeWeapon || item.EquipmentSlot == EquipmentSlot.DistanceWeapon || item.EquipmentSlot == EquipmentSlot.MagicWeapon)
            {
                btnDrop.gameObject.SetActive(false);
                btnUse.gameObject.SetActive(false);
                btnRemove.gameObject.SetActive(false);
            }
            else
            {
                btnDrop.gameObject.SetActive(false);
                btnUse.gameObject.SetActive(false);
                btnRemove.onClick.AddListener(() => Remove(item));
            }
            //show stats
            listStats.stats.Clear();
            listStats.stats.Add($"Name: {item.Name}");
            if (item.LevelRequired != 0)
                listStats.stats.Add($"Level: {item.LevelRequired}");
            if (item.Attack != 0)
                listStats.stats.Add($"Attack: {item.Attack}");
            if (item.Armor != 0)
                listStats.stats.Add($"Armor: {item.Armor}");
            if (item.Speed != 0)
                listStats.stats.Add($"Speed: {item.Speed}");
            if (item.RecoveryHp != 0)
                listStats.stats.Add($"RecoveryHp: {item.RecoveryHp}");
            if (item.RecoveryMp != 0)
                listStats.stats.Add($"RecoveryMp: {item.RecoveryMp}");
            if (item.RecoveryHealthPercent != 0)
                listStats.stats.Add($"RecoveryHealthPercent: {item.RecoveryHealthPercent}");
            if (item.RecoveryManaPercent != 0)
                listStats.stats.Add($"RecoveryManaPercent: {item.RecoveryManaPercent}");
            if (item.Melee != 0)
                listStats.stats.Add($"Melee: {item.Melee}");
            if (item.Distance != 0)
                listStats.stats.Add($"Distance: {item.Distance}");
            if (item.Magic != 0)
                listStats.stats.Add($"Magic: {item.Magic}");
            if (item.Defense != 0)
                listStats.stats.Add($"Defense: {item.Defense}");

            listStats.ShowListStats();
            //
            btnDisabled.onClick.AddListener(() => OnBackToInventory());
            btnBack.onClick.AddListener(() => OnBackToInventory());
            image.sprite = item.ItemImage;

            return;
        }
    }

    private void OnBackToInventory()
    {
        MenuManager.Instance.OpenMenu("Inventory");
    }

    private void Remove(EquipmentSO item)
    {
        Inventory.Instance.items.Add(item);
        items.Remove(item);
        MenuManager.Instance.OpenMenu("Inventory");
    }
}
