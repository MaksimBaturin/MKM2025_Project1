using UnityEngine;
using TMPro;
using System.IO;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameParamSaver : MonoBehaviour
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
    [SerializeField] private TMP_InputField SeedInput;
    [SerializeField] private TMP_InputField MinDistanceInput;
    [SerializeField] private TMP_InputField MaxDistanceInput;

    private string filePath;

    private void Start()
    {
        filePath = Path.Combine(Application.persistentDataPath, "start_data.json");
        if (File.Exists(filePath))
        {
            File.WriteAllText(filePath, "");
            Debug.Log("Файл очищен при старте.");
        }
    }

    public void OnClick()
    {
        // Сбросить цвет всех полей ввода
        ResetInputFieldColors();

        // Проверка на пустые поля
        bool hasEmptyFields = false;

        if (string.IsNullOrEmpty(FuelVelocityInput.text))
        {
            SetInputFieldColor(FuelVelocityInput, Color.red);
            hasEmptyFields = true;
        }
        if (string.IsNullOrEmpty(FuelLossInput.text))
        {
            SetInputFieldColor(FuelLossInput, Color.red);
            hasEmptyFields = true;
        }
        if (string.IsNullOrEmpty(SideFuelVelocityInput.text))
        {
            SetInputFieldColor(FuelVelocityInput, Color.red);
            hasEmptyFields = true;
        }
        if (string.IsNullOrEmpty(SideFuelLossInput.text))
        {
            SetInputFieldColor(FuelLossInput, Color.red);
            hasEmptyFields = true;
        }
        if (string.IsNullOrEmpty(MassInput.text))
        {
            SetInputFieldColor(MassInput, Color.red);
            hasEmptyFields = true;
        }
        if (string.IsNullOrEmpty(GInput.text))
        {
            SetInputFieldColor(GInput, Color.red);
            hasEmptyFields = true;
        }
        if (string.IsNullOrEmpty(AllowedVelocityInput.text))
        {
            SetInputFieldColor(AllowedVelocityInput, Color.red);
            hasEmptyFields = true;
        }
        if (string.IsNullOrEmpty(FuelMassInput.text))
        {
            SetInputFieldColor(FuelMassInput, Color.red);
            hasEmptyFields = true;
        }
        if (string.IsNullOrEmpty(DeltaTimeInput.text))
        {
            SetInputFieldColor(DeltaTimeInput, Color.red);
            hasEmptyFields = true;
        }
        if (string.IsNullOrEmpty(DeltaTimeForGraphsInput.text))
        {
            SetInputFieldColor(DeltaTimeForGraphsInput, Color.red);
            hasEmptyFields = true;
        }
        if (string.IsNullOrEmpty(SeedInput.text))
        {
            SetInputFieldColor(SeedInput, Color.red);
            hasEmptyFields = true;
        }
        if (string.IsNullOrEmpty(MinDistanceInput.text))
        {
            SetInputFieldColor(MinDistanceInput, Color.red);
            hasEmptyFields = true;
        }
        if (string.IsNullOrEmpty(MaxDistanceInput.text))
        {
            SetInputFieldColor(MaxDistanceInput, Color.red);
            hasEmptyFields = true;
        }

        if (hasEmptyFields)
        {
            Debug.Log("Ошибка: Не все поля заполнены.");
            return;
        }

        StartData startData = new StartData
        {
            FuelVelocity = Mathf.Abs(float.Parse(FuelVelocityInput.text)),
            FuelLoss = Mathf.Abs(float.Parse(FuelLossInput.text)),
            SideFuelVelocity = Mathf.Abs(float.Parse(SideFuelVelocityInput.text)),
            SideFuelLoss = Mathf.Abs(float.Parse(SideFuelLossInput.text)),
            FuelMass = Mathf.Abs(float.Parse(FuelMassInput.text)),
            AllowedVelocity = Mathf.Abs(float.Parse(AllowedVelocityInput.text)),
            Mass = Mathf.Abs(float.Parse(MassInput.text)),
            G = Mathf.Abs(float.Parse(GInput.text)),
            DeltaTime = Mathf.Abs(float.Parse(DeltaTimeInput.text)),
            DeltaTimeForGraphsInput = Mathf.Abs(float.Parse(DeltaTimeForGraphsInput.text)),
            Seed = Mathf.Abs(int.Parse(SeedInput.text)),
            MinDistance = Mathf.Abs(int.Parse(MinDistanceInput.text)),
            MaxDistance = Mathf.Abs(int.Parse(MaxDistanceInput.text))
        };

        List<StartData> dataList = new List<StartData> { startData };

        StartDataWrapper wrapper = new StartDataWrapper
        {
            data = dataList
        };


        string json = JsonUtility.ToJson(wrapper, true);

        File.WriteAllText(filePath, json);

        Debug.Log("Данные сохранены в файл: " + filePath);

        SceneManager.LoadScene("MainGame");
    }

    private void ResetInputFieldColors()
    {
        SetInputFieldColor(FuelVelocityInput, Color.white);
        SetInputFieldColor(FuelLossInput, Color.white);
        SetInputFieldColor(MassInput, Color.white);
        SetInputFieldColor(GInput, Color.white);
        SetInputFieldColor(AllowedVelocityInput, Color.white);
        SetInputFieldColor(FuelMassInput, Color.white);
        SetInputFieldColor(DeltaTimeInput, Color.white);
        SetInputFieldColor(DeltaTimeForGraphsInput, Color.white);
        SetInputFieldColor(SeedInput, Color.white);
        SetInputFieldColor(MinDistanceInput, Color.white);
        SetInputFieldColor(MaxDistanceInput, Color.white);
    }


    private void SetInputFieldColor(TMP_InputField inputField, Color color)
    {
        inputField.image.color = color; 

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