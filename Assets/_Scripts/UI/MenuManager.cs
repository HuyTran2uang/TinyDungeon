using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
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
}
