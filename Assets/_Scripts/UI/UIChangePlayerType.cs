using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIChangePlayerType : MonoBehaviour
{
    [SerializeField] private Button buttonMelee;
    [SerializeField] private Button buttonDistance;
    [SerializeField] private Button buttonMagic;
    [SerializeField] private Button[] btns;

    private void Start()
    {
        buttonMelee.onClick.AddListener(() => ChangeTypeMelee());
        buttonDistance.onClick.AddListener(() => ChangeTypeDistance());
        buttonMagic.onClick.AddListener(() => ChangeTypeMagic());
    }

    public void ChangeTypeMelee()
    {
        Player.Instance.Type = PlayerType.Melee;
        SetButtonSelected(buttonMelee);
    }

    public void ChangeTypeDistance()
    {
        Player.Instance.Type = PlayerType.Distance;
        SetButtonSelected(buttonDistance);
    }

    public void ChangeTypeMagic()
    {
        Player.Instance.Type = PlayerType.Magic;
        SetButtonSelected(buttonMagic);
    }

    private void SetButtonSelected(Button btn)
    {
        foreach (var i in btns)
            if (btn == i)
                i.image.color = new Color32(255, 255, 255, 0);
            else
                i.image.color = new Color32(255, 255, 255, 90);
    }
}
