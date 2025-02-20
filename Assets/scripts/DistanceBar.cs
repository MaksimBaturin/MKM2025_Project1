using UnityEngine;

public class DistanceBar : MonoBehaviour
{
    private float heightOfBar;
    private RectTransform heightPointer;
    public float MaxSkyHeight = 5000;

    private Rocket rocket;
    void Start()
    {
        rocket = GameObject.FindGameObjectWithTag("Player").GetComponent<Rocket>();
        heightPointer = GameObject.Find("HeightPointer").GetComponent<RectTransform>();
        heightOfBar = GetComponent<RectTransform>().rect.height-5;
    }


    void Update()
    {
        float currentHeight = rocket.Position.y;

        float normalizedHeight = Mathf.Clamp01(currentHeight / MaxSkyHeight);
        Vector3 newLocalPosition = heightPointer.localPosition;
        newLocalPosition.y = normalizedHeight * heightOfBar;
        heightPointer.localPosition = newLocalPosition;
    }
}
