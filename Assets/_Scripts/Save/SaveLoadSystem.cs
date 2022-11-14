using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadSystem : MonoBehaviour
{
    public void ResetData()
    {
        GameData data = new GameData();
        Player.Instance.data = data.playerData;
    }

    public void SaveGame()
    {
        GameData data = new GameData();
        data.playerData = Player.Instance.data;

        SaveLoadManager.SaveData(data);
    }

    public void LoadData()
    {
        GameData data = SaveLoadManager.LoadData();
        Player.Instance.data = data.playerData;
    }
}
