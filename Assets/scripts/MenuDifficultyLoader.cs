using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class MenuDifficultyLoader : MonoBehaviour
{
    [SerializeField] private TMP_InputField FuelVelocityInput;
    [SerializeField] private TMP_InputField FuelLossInput;
    [SerializeField] private TMP_InputField SideFuelVelocityInput;
    [SerializeField] private TMP_InputField SideFuelLossInput;
    [SerializeField] private TMP_InputField MassInput;
    [SerializeField] private TMP_InputField GInput;
    [SerializeField] private TMP_InputField AllowedVelocityInput;
    [SerializeField] private TMP_InputField FuelMassInput;
    [SerializeField] private TMP_InputField DeltaTimeInput;
    [SerializeField] private TMP_InputField DeltaTimeForGraphsInput;

    [SerializeField] private TMP_InputField MinDistanceInput;
    [SerializeField] private TMP_InputField MaxDistanceInput;

    [SerializeField] private string fileName;

    public void OnClick()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, fileName);

        if (File.Exists(filePath))
        {
            string jsonData = File.ReadAllText(filePath);
            StartDataWrapper wrapper = JsonUtility.FromJson<StartDataWrapper>(jsonData);

            if (wrapper.data.Count > 0)
            {
                StartData startData = wrapper.data[0];

                FuelVelocityInput.text = startData.FuelVelocity.ToString();
                FuelLossInput.text = startData.FuelLoss.ToString();
                SideFuelVelocityInput.text = startData.SideFuelVelocity.ToString();
                SideFuelLossInput.text = startData.SideFuelLoss.ToString();
                FuelMassInput.text = startData.FuelMass.ToString();
                AllowedVelocityInput.text = startData.AllowedVelocity.ToString();
                MassInput.text = startData.Mass.ToString();
                GInput.text = startData.G.ToString();
                DeltaTimeInput.text = startData.DeltaTime.ToString();
                DeltaTimeForGraphsInput.text = startData.DeltaTimeForGraphsInput.ToString();
                MinDistanceInput.text = startData.MinDistance.ToString();
                MaxDistanceInput.text = startData.MaxDistance.ToString();
            }
            else
            {
                Debug.LogError("No data found in JSON file.");
            }
        }
        else
        {
            Debug.LogError("File not found: " + filePath);
        }
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