using UnityEngine;
using UnityEngine.UI;

public class ChangePlayerType : MonoBehaviour
{
    [SerializeField] private Button _buttonMelee;
    [SerializeField] private Button _buttonDistance;
    [SerializeField] private Button _buttonMagic;
    [SerializeField] private Button[] _buttons;

    private void Start()
    {
        _buttons = GetComponentsInChildren<Button>();

        _buttonMelee = transform.Find("ButtonMelee").GetComponent<Button>();
        _buttonDistance = transform.Find("ButtonDistance").GetComponent<Button>();
        _buttonMagic = transform.Find("ButtonMagic").GetComponent<Button>();

        _buttonMelee.onClick.AddListener(() => ChangeTypeMelee());
        _buttonDistance.onClick.AddListener(() => ChangeTypeDistance());
        _buttonMagic.onClick.AddListener(() => ChangeTypeMagic());
    }

    public void ChangeTypeMelee()
    {
        SetButtonSelected(_buttonMelee);
        Player.Instance.ChangeCharacter(PlayerType.Melee);
    }

    public void ChangeTypeDistance()
    {
        SetButtonSelected(_buttonDistance);
        Player.Instance.ChangeCharacter(PlayerType.Distance);
    }

    public void ChangeTypeMagic()
    {
        SetButtonSelected(_buttonMagic);
        Player.Instance.ChangeCharacter(PlayerType.Magic);
    }

    private void SetButtonSelected(Button btn)
    {
        foreach (var i in _buttons)
            if (btn == i)
                i.image.color = new Color32(255, 255, 255, 0);
            else
                i.image.color = new Color32(255, 255, 255, 90);
    }
}
