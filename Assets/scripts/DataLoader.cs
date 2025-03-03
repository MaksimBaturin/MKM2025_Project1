using System.Collections.Generic;
using System.IO;
using UnityEngine;
using static ParamSaver;

public class DataLoader : MonoBehaviour
{
    public List<RocketData> LoadDataFromJson()
    {
        string path = Path.Combine(Application.persistentDataPath, "rocket_data.json");
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            RocketDataWrapper wrapper = JsonUtility.FromJson<RocketDataWrapper>(json);
            return wrapper.data;
        }
        else
        {
            Debug.LogError("File not found: " + path);
            return null;
        }
    }

    [System.Serializable]
    private class RocketDataWrapper
    {
        public List<RocketData> data;
    }
}