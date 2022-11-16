using UnityEngine;
using UnityEngine.UI;

public class SkinManagerUI : MonoBehaviourSingleton<SkinManager>
{
    Skin[] Skins => SkinManager.Instance.skins;
    [SerializeField] private GameObject _skinPrefabs;

    private void Awake() => _skinPrefabs = Resources.Load<GameObject>("UI/SkinItem");

    private void OnEnable()
    {
        SetListSkins();
    }

    private void SetListSkins()
    {
        Helpers.DestroyChildren(transform);
        foreach (var i in Skins)
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
                Destroy(btnBuy.gameObject);
                if (i.isUsing)
                {
                    btnUse.gameObject.SetActive(false);
                    notify.text = "using";
                }
                else
                {
                    btnUse.gameObject.SetActive(true);
                    notify.text = "owned";
                }
            }
        }
    }

    public void Buy(Skin skin)
    {
        for (int i = 0; i < Skins.Length; i++)
        {
            if (Skins[i] == skin && !Skins[i].isOwned)
            {
                if (Player.Instance.data.diamond < skin.skinSO.PriceBuy) return;
                Player.Instance.data.diamond -= skin.skinSO.PriceBuy;
                Skins[i].isOwned = true;
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
        for (int i = 0; i < Skins.Length; i++)
        {
            if (!Skins[i].isOwned) continue;
            if (Skins[i] == skin && !Skins[i].isUsing)
            {
                Skins[i].isUsing = true;
                var btnUse = transform.GetChild(i).transform.Find("ButtonUse").gameObject;
                var notify = transform.GetChild(i).transform.Find("Notify").GetComponent<Text>();
                btnUse.SetActive(false);
                notify.text = "using";
                PlayerSkin.Instance.SetSkin(Player.Instance.Type);
            }
            if (Skins[i].skinSO.SkinSlot == skin.skinSO.SkinSlot && Skins[i] != skin)
            {
                Skins[i].isUsing = false;
                var btnUse = transform.GetChild(i).transform.Find("ButtonUse").gameObject;
                var notify = transform.GetChild(i).transform.Find("Notify").GetComponent<Text>();
                btnUse.SetActive(true);
                notify.text = "owned";
            }
        }
    }
}
