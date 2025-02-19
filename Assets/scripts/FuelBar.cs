using UnityEngine;

public class Fuel : MonoBehaviour
{
    private Rocket rocket;
    private Vector3 initialScale;
    private float initialFuelMass;

    void Start()
    {
        rocket = GameObject.FindGameObjectWithTag("Player").GetComponent<Rocket>();
        initialScale = transform.localScale;
        initialFuelMass = rocket.FuelMass;
    }

    void Update()
    {
        Vector3 newScale = transform.localScale;
        newScale.y = initialScale.y * (rocket.FuelMass / initialFuelMass);
        transform.localScale = newScale;
    }
}