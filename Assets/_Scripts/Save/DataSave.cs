using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DataSave : MonoBehaviour
{
    [SerializeField] private Vector3 _position;
    [SerializeField] private int _level;
    [SerializeField] private int _currentHealth;
    [SerializeField] private int _currentExp;
    [SerializeField] private int _melee;
    [SerializeField] private int _currentExpMelee;
    [SerializeField] private int _distance;
    [SerializeField] private int _currentExpDistance;
    [SerializeField] private int _magic;
    [SerializeField] private int _currentExpMagic;
    [SerializeField] private int _defense;
    [SerializeField] private int _currentDefense;
    [SerializeField] private int _gold;

    public void SaveData()
    {
        // Player player = FindObjectOfType<Player>();
        // // _position = player.transform.position;
        // // _melee = player.Melee;
        // // _distance = player.Distance;
        // // _magic = player.Magic;
        // // _defense = player.Defense;

        // PlayerLevel playerLevel = FindObjectOfType<PlayerLevel>();
        // _level = playerLevel.Level;
        // _currentExp = playerLevel.CurrentExp;

        // PlayerHealth playerHealth = FindObjectOfType<PlayerHealth>();
        // _currentHealth = playerHealth.CurrentHealth;
    }
}
