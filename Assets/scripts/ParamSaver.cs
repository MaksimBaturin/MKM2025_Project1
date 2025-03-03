using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ParamSaver : MonoBehaviour
{
    [SerializeField] private Rocket rocket;

    private float lastPointTime1 = 0f;
    public float deltaTimeForGraphs = 0.2f;

    private List<RocketData> rocketDataList = new List<RocketData>();
    private const int SaveInterval = 100;

    void Start()
    {
        rocket = GameObject.FindGameObjectWithTag("Player").GetComponent<Rocket>();

        if (rocket == null)
        {
            Debug.LogError("Rocket not found!");
        }
    }

    void FixedUpdate()
    {
        float velocity = rocket.Velocity.magnitude;
        Vector2 position = rocket.Position;

        if (Time.time - lastPointTime1 >= deltaTimeForGraphs)
        {
            lastPointTime1 = Time.time;

            RocketData data = new RocketData
            {
                time = Time.time,
                velocity = velocity,
                position = position
            };

            rocketDataList.Add(data);

            if (rocketDataList.Count >= SaveInterval)
            {
                SaveDataToJson();
                rocketDataList.Clear();
                Debug.Log("Data saved and list cleared.");
            }
        }
    }

    void OnDestroy()
    {
        if (rocketDataList.Count > 0)
        {
            SaveDataToJson();
            Debug.Log("Final data saved on destroy.");
        }
    }

    private void SaveDataToJson()
    {
        RocketDataWrapper wrapper = new RocketDataWrapper
        {
            data = rocketDataList
        };

        string json = JsonUtility.ToJson(wrapper, true);

        string path = Path.Combine(Application.persistentDataPath, "rocket_data.json");
        File.WriteAllText(path, json);
        Debug.Log("Data saved to " + path);
    }

    [System.Serializable]
    public class RocketData
    {
        public float time;
        public float velocity;
        public Vector2 position;
    }

    [System.Serializable]
    private class RocketDataWrapper
    {
        public List<RocketData> data;
    }
}