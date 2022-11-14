using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerName : MonoBehaviourSingleton<PlayerName>
{
    UIShortInfo _shortInfo;

    private void Awake() => _shortInfo = GetComponentInChildren<UIShortInfo>();

    char[] _forbiddenChars = {'`', '~', '!', '@', '#', '$', '%', '^', '&', '*',
                            '(', ')', '-', '_', '=', '+', '[','{',']','}', ';',
                            ':', '"', '\'', ',', '<', '.', '>', '/', '?', '\\', '|',
                            '1', '2', '3', '4', '5', '6', '7', '8', '9', '0'};

    public string Name => Player.Instance.data.name;

    public void SetName(string name)
    {
        if (name.Length > 8) return;
        foreach (var c in _forbiddenChars)
        {
            if (name.Contains(c)) return;
        }
        Player.Instance.data.name = name;
        _shortInfo.SetNameText(name);
    }

    private void Start() => _shortInfo.SetNameText(Name);
}
