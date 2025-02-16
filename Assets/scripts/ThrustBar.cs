using UnityEngine;

public class ThrustBar : MonoBehaviour
{
    private Rocket rocket;
    private Vector3 initialScale;

    void Start()
    {
        rocket = GameObject.FindGameObjectWithTag("Player").GetComponent<Rocket>();
        initialScale = transform.localScale;
    }

    void Update()
    {
        Vector3 newScale = transform.localScale;
        newScale.y = initialScale.y * rocket.thrustController.CurrentThrust;
        transform.localScale = newScale;
    }
}