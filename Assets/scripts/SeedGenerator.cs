using TMPro;
using UnityEngine;

public class SeedGenerator : MonoBehaviour
{
    private TMP_InputField Input;

    void Start()
    {
        Input = GetComponent<TMP_InputField>();
        GenerateRandomSeed();
    }

    public void GenerateRandomSeed()
    {

        int randomSeed = Random.Range(1000000, 9999999);
        Input.text = randomSeed.ToString();
    }
}