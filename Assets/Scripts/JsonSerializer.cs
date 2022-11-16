using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

public class JsonSerializer : MonoBehaviour
{
    public GameSaveData GSD;
    public string path;


    public void Start()
    {
        if (Application.isEditor)
        {
            path = Application.dataPath + "/SaveData/";
        }
        else
        {
            path = Application.persistentDataPath + "/SaveData/";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
                Debug.LogWarning(path);
            }
        }
        
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            Save();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            Load();
        }
    }

    public void Save()
    {
        var convertedJson = JsonConvert.SerializeObject(GSD, Formatting.None, new JsonSerializerSettings()
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        }) ;
        Debug.Log(path + "Data.json");
        File.WriteAllText(path + "Data.json", convertedJson);
    }

    public void Load()
    {
        if (File.Exists(path + "Data.json"))
        {
            var loadedData = File.ReadAllText(path + "Data.json");
            GSD = JsonConvert.DeserializeObject<GameSaveData>(loadedData);
            Debug.LogWarning(GSD.Number);
        }
    }
}

[System.Serializable]
public class GameSaveData
{
    public int Number;
    public string Word;
}