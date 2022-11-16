using UnityEngine;
using UnityEngine.UI;

public class UIDamageable : MonoBehaviour
{
    [SerializeField] private GameObject _damageablePrefabs;

    private void Awake()
    {
        _damageablePrefabs = Resources.Load<GameObject>("UI/DamageablePrefab");
    }

    public void ShowDamageable(int damage)
    {
        GameObject dam = Instantiate(_damageablePrefabs);
        dam.GetComponent<Text>().text = damage.ToString();
    }
}
