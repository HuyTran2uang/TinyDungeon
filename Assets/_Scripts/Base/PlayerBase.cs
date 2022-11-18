using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerBase
{
    [SerializeField] private int _exp;
    [SerializeField] private int _health;
    [SerializeField] private int _mana;
    [SerializeField] private int _speed;
    [SerializeField] private int _recoveryHealth;
    [SerializeField] private int _recoveryMana;
    [SerializeField] private int _melee;
    [SerializeField] private int _maxExpMelee;
    [SerializeField] private int _distance;
    [SerializeField] private int _maxExpDistance;
    [SerializeField] private int _magic;
    [SerializeField] private int _maxExpMagic;
    [SerializeField] private int _defense;
    [SerializeField] private int _maxExpDefense;

    public int Exp
    {
        get { return _exp; }
    }
    public int Health
    {
        get { return _health; }
    }
    public int Mana
    {
        get { return _mana; }
    }
    public int Speed
    {
        get { return _speed; }
    }
    public int RecoveryHealth
    {
        get { return _recoveryHealth; }
    }
    public int RecoveryMana
    {
        get { return _recoveryMana; }
    }
    public int Melee
    {
        get { return _melee; }
    }
    public int Distance
    {
        get { return _distance; }
    }
    public int Magic
    {
        get { return _magic; }
    }
    public int Defense
    {
        get { return _defense; }
    }
    public int MaxExpMelee
    {
        get { return _maxExpMelee; }
    }
    public int MaxExpDistance
    {
        get { return _maxExpDistance; }
    }
    public int MaxExpMagic
    {
        get { return _maxExpMagic; }
    }
    public int MaxExpDefense
    {
        get { return _maxExpDefense; }
    }

    public PlayerBase()
    {
        _exp = 10;
        _health = 100;
        _mana = 100;
        _speed = 200;
        _recoveryHealth = 2;
        _recoveryMana = 2;
        _melee = 5;
        _maxExpMelee = 10;
        _distance = 5;
        _maxExpDistance = 10;
        _magic = 5;
        _maxExpMagic = 10;
        _defense = 5;
        _maxExpDefense = 10;
    }
}
