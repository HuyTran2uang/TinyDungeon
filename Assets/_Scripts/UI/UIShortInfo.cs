using UnityEngine;
using UnityEngine.UI;

public class UIShortInfo : MonoBehaviour
{
    [SerializeField] private Text _nameText;
    [SerializeField] private Text _levelText;

    private void Awake()
    {
        _nameText = transform.Find("Name").GetComponent<Text>();
        _levelText = transform.Find("Level").GetComponent<Text>();
    }

    public void SetNameText(string name)
    {
        _nameText.text = name;
    }

    public void SetLevelText(int level)
    {
        _levelText.text = level.ToString();
    }
}
