using UnityEngine;
using UnityEngine.UIElements;
using System.Collections;
using Unity.VisualScripting;
using System.Collections.Generic;
using System.IO;

public class GameInit : MonoBehaviour
{
    [SerializeField] private GameObject rocket;
    [SerializeField] private GameObject startPad;
    [SerializeField] private GameObject endPad;
    [SerializeField] private GameObject ui;
    [SerializeField] private GameObject _camera;
    [SerializeField] private GameObject terrain;
    [SerializeField] private GameObject pointer;
    [SerializeField] private GameObject gameController;

    private string filePath;

    private RaycastHit2D hit;

    void Start()
    {
        filePath = Path.Combine(Application.persistentDataPath, "start_data.json");
        Debug.Log(filePath);
        StartCoroutine(InitializeGame());
    }

    private IEnumerator InitializeGame()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            StartDataWrapper wrapper = JsonUtility.FromJson<StartDataWrapper>(json);
            StartData startData = wrapper.data[0];

            float FuelVelocity = startData.FuelVelocity;
            float FuelLoss = startData.FuelLoss;
            float SideFuelVelocity = startData.SideFuelVelocity;
            float SideFuelLoss = startData.SideFuelLoss;
            float FuelMass = startData.FuelMass;
            float AllowedVelocity = startData.AllowedVelocity;
            float Mass = startData.Mass;
            float G = startData.G;
            float DeltaTime = startData.DeltaTime;
            float DeltaTimeForGraphs = startData.DeltaTimeForGraphsInput;
            int Seed = startData.Seed;
            int MinDistance = startData.MinDistance;
            int MaxDistance = startData.MaxDistance;

            //terrain
            terrain.GetComponent<MoonTerrainGenerator>().seed = Seed;
            Instantiate(terrain, new Vector3(-4099f, 0, 0), Quaternion.identity);

            //startPad
            yield return PerformRaycast(new Vector2(0, 500f));
            Instantiate(startPad, hit.point.ConvertTo<Vector3>() + new Vector3(0, 150, 0), Quaternion.identity);

            //endPad
            System.Random rand = new System.Random();
            Debug.Log(MinDistance);
            Debug.Log(MaxDistance);
            Vector3 endPadPos = new Vector3(rand.Next(MinDistance, MaxDistance), 500f);
            int direction = rand.Next(-1, 1);
            if (direction == -1) endPadPos.x *= -1;

            yield return PerformRaycast(endPadPos);
            Instantiate(endPad, hit.point.ConvertTo<Vector3>() + new Vector3(0, 150, 0), Quaternion.identity);

            //rocket
            Vector3 rocketSpawnPosition = GameObject.Find("RocketSpawn").transform.position;
            Rocket _rocket = rocket.GetComponent<Rocket>();
            _rocket.MaxFuelVelocity = -FuelVelocity;
            _rocket.FuelLossRate = FuelLoss;
            _rocket.SideThrusterFuelLossRate = SideFuelLoss;
            _rocket.SideThrusterFuelVelocity = SideFuelVelocity;
            _rocket.FuelMass = FuelMass;
            _rocket.MaxSpeedOnCollision = AllowedVelocity;
            _rocket.AccelerationOfFreeFall = new Vector2(0, -G);
            _rocket.Mass = Mass;
            _rocket.TimeStep = DeltaTime;
            Instantiate(rocket, rocketSpawnPosition, Quaternion.identity);

            //camera
            Vector3 cameraPosition = rocketSpawnPosition - new Vector3(0, 0, 10);
            Instantiate(_camera, cameraPosition, Quaternion.identity);

            //UI     
            Instantiate(ui);

            //pointer
            Instantiate(pointer, rocketSpawnPosition, Quaternion.identity);

            //gameController
            Instantiate(gameController);
            GameObject.FindAnyObjectByType<ParamSaver>().deltaTimeForGraphs = DeltaTimeForGraphs;
        }
    }

    private IEnumerator PerformRaycast(Vector2 rayPos)
    {
        yield return null;
        hit = Physics2D.Raycast(rayPos, -Vector2.up, 2000f, Physics.DefaultRaycastLayers, -1f);
    }

    [System.Serializable]
    public class StartData
    {
        public float FuelVelocity;
        public float FuelLoss;
        public float SideFuelVelocity;
        public float SideFuelLoss;
        public float FuelMass;
        public float AllowedVelocity;
        public float Mass;
        public float G;
        public float DeltaTime;
        public float DeltaTimeForGraphsInput;
        public int Seed;
        public int MinDistance;
        public int MaxDistance;
    }

    [System.Serializable]
    private class StartDataWrapper
    {
        public List<StartData> data;
    }

}