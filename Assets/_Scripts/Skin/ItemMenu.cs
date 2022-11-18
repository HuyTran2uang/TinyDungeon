using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemMenu : MonoBehaviourSingleton<ItemMenu>
{
    public Button buttonDisabled;
    public Button buttonBack;
    public Image image;
    public Text description;
    public Button buttonUnequip;
    public Button buttonRemove;
    public Button buttonUse;
    public Button buttonBuy;

    private void OnEnable() => ResetToDefault();

    private void ResetToDefault()
    {
        buttonDisabled.onClick.RemoveAllListeners();
        buttonBack.onClick.RemoveAllListeners();
        buttonUnequip.onClick.RemoveAllListeners();
        buttonRemove.onClick.RemoveAllListeners();
        buttonUse.onClick.RemoveAllListeners();
        buttonBuy.onClick.RemoveAllListeners();

        buttonUnequip.gameObject.SetActive(false);
        buttonBuy.gameObject.SetActive(false);
        buttonRemove.gameObject.SetActive(false);
        buttonUse.gameObject.SetActive(false);
    }
}
