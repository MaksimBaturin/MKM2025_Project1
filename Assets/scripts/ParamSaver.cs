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
    private string filePath;

    void Start()
    {
        rocket = GameObject.FindGameObjectWithTag("Player").GetComponent<Rocket>();

        if (rocket == null)
        {
            Debug.LogError("Rocket not found!");
        }

        filePath = Path.Combine(Application.persistentDataPath, "rocket_data.json");

        // ������� ����� ��� ������
        if (File.Exists(filePath))
        {
            File.WriteAllText(filePath, "");
            Debug.Log("���� ������ ��� ������.");
        }
    }

    void FixedUpdate()
    {
        float velocity = rocket.Velocity.magnitude;
        float tsiolkovskyVelocity = rocket.TsiolkovskyVelocity.magnitude;
        Vector2 position = rocket.Position;

        if (Time.time - lastPointTime1 >= deltaTimeForGraphs)
        {
            lastPointTime1 = Time.time;

            RocketData data = new RocketData
            {
                time = Time.time,
                velocity = velocity,
                tsiolkovsky = tsiolkovskyVelocity,
                position = position,
                fuelmass = rocket.FuelMass
            };

            rocketDataList.Add(data);

            if (rocketDataList.Count >= SaveInterval)
            {
                AppendDataToJson();
                rocketDataList.Clear();
                Debug.Log("������ ��������� � ������ ������.");
            }
        }
    }

    void OnDestroy()
    {
        if (rocketDataList.Count > 0)
        {
            AppendDataToJson();
            Debug.Log("��������� ������ ��������� ��� ����������� �������.");
        }
    }

    private void AppendDataToJson()
    {
        List<RocketData> existingData = new List<RocketData>();

        // ���� ���� ���������� � �� ����, ��������� ������ ������
        if (File.Exists(filePath) && new FileInfo(filePath).Length > 0)
        {
            string existingJson = File.ReadAllText(filePath);
            RocketDataWrapper existingWrapper = JsonUtility.FromJson<RocketDataWrapper>(existingJson);
            if (existingWrapper != null && existingWrapper.data != null)
            {
                existingData = existingWrapper.data;
            }
        }

        // ��������� ����� ������
        existingData.AddRange(rocketDataList);

        // ����������� � ���������
        RocketDataWrapper wrapper = new RocketDataWrapper { data = existingData };
        string json = JsonUtility.ToJson(wrapper, true);
        File.WriteAllText(filePath, json);

        Debug.Log("������ ��������� � " + filePath);
    }

    [System.Serializable]
    public class RocketData
    {
        public float time;
        public float velocity;
        public float tsiolkovsky;
        public float fuelmass;
        public Vector2 position;
    }

    [System.Serializable]
    private class RocketDataWrapper
    {
        public List<RocketData> data;
    }
}
