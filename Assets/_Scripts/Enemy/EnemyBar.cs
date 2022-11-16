using UnityEngine;
using UnityEngine.UI;

public class EnemyBar : MonoBehaviour
{
    [SerializeField] private Text nameText;
    [SerializeField] private Slider sliderHealthBar;

    public void SetHealthBar(int cur, int max)
    {
        sliderHealthBar.maxValue = max;
        sliderHealthBar.value = cur;
    }

    public void SetNameText(string name)
    {
        nameText.text = name;
    }
}
