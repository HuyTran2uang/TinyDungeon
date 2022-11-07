using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayerBar : MonoBehaviourSingleton<UIPlayerBar>
{
    [Header("Hp")]
    [SerializeField] private Slider sliderHealthBar;
    [SerializeField] private Text hpText;

    [Header("Mp")]
    [SerializeField] private Slider sliderManaBar;

    [Header("Exp")]
    [SerializeField] private Slider sliderExpBar;


    public void SetHealthBar(int cur, int max)
    {
        sliderHealthBar.maxValue = max;
        sliderHealthBar.value = cur;
        hpText.text = $"{cur}/{max}";
    }

    public void SetManaBar(int cur, int max)
    {
        sliderManaBar.maxValue = max;
        sliderManaBar.value = cur;
    }

    public void SetExpBar(int cur, int max)
    {
        sliderExpBar.maxValue = max;
        sliderExpBar.value = cur;
    }
}
