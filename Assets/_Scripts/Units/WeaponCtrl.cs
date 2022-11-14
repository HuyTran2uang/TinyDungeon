using System.Collections;
using UnityEngine;

public class WeaponCtrl : MonoBehaviour
{
    [field: SerializeField]
    public EquipmentSO Weapon { get; set; }
    SpriteRenderer spriteRenderer;
    Vector3 originalEuler;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        originalEuler = transform.localEulerAngles;
    }

    private void Update()
    {
        if (Weapon != null)
            spriteRenderer.sprite = Weapon.ItemImage;
    }

    public void Attack()
    {
        StartCoroutine(HitDown());
    }

    IEnumerator HitDown()
    {
        transform.localRotation = Quaternion.Lerp(Quaternion.Euler(originalEuler), Quaternion.Euler(Vector3.forward * -90), 10);
        yield return new WaitForSeconds(0.5f);
        transform.localRotation = Quaternion.Lerp(Quaternion.Euler(transform.localEulerAngles), Quaternion.Euler(originalEuler), 10);
        yield return new WaitForSeconds(0.5f);
    }

    public void ChangeWeapon(EquipmentSO _base)
    {
        Weapon = _base;
    }
}
