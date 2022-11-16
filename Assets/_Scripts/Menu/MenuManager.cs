using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviourSingleton<MenuManager>
{
    [field: SerializeField]
    public Menu[] Menus { get; private set; }

    public void OpenMenu(Menu menu)
    {
        foreach (Menu i in Menus)
        {
            if (i == menu)
                i.Open();
            else
                i.Close();
        }
    }

    public void OpenMenu(string menu)
    {
        foreach (Menu i in Menus)
        {
            if (i.Name == menu)
                i.Open();
            else
                i.Close();
        }
    }
}
