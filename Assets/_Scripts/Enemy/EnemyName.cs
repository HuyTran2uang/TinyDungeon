using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyName : MonoBehaviour
{
    Enemy _enemy;
    UIShortInfo _shortInfo;

    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
        _shortInfo = GetComponentInChildren<UIShortInfo>();
    }

    private void OnEnable()
    {
        _shortInfo.SetNameText(_enemy.Name);
    }
}
