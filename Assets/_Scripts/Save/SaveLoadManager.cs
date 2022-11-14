using System.IO;
using UnityEngine;
using Newtonsoft.Json;

public static class SaveLoadManager
{
    public static void SaveData(GameData data)
    {
        string path = Application.persistentDataPath + "/GameData.txt";
        Debug.Log(Application.persistentDataPath + "/GameData.txt");
        string jsonData = JsonUtility.ToJson(data, true);
        File.WriteAllText(path, jsonData);
    }

    public static GameData LoadData()
    {
        string path = Application.persistentDataPath + "/GameData.txt";
        Debug.Log(Application.persistentDataPath + "/GameData.txt");

        if (File.Exists(path))
        {
            string jsonString = File.ReadAllText(path);
            GameData data = JsonConvert.DeserializeObject<GameData>(jsonString);
            return data;
        }

        return null;
    }
}