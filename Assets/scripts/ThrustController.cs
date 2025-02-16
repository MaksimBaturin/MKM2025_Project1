using UnityEngine;

public class ThrustController : MonoBehaviour
{
    private Rocket rocket;

    private float MaxThrust = 1;
    public float CurrentThrust = 0;

    public float ThrustChangeSpeed = 2f;

    void Start()
    {
        rocket = GetComponent<Rocket>();
    }


    void Update()
    {
        if (Input.GetKey(KeyCode.W) && CurrentThrust < MaxThrust)
        {
            CurrentThrust += ThrustChangeSpeed * Time.deltaTime;
            if (CurrentThrust >= MaxThrust) CurrentThrust = 1;
            //Debug.Log($"CurrentThrust: {CurrentThrust}");
        }
        else if (Input.GetKey(KeyCode.S) && CurrentThrust > 0)
        {
            CurrentThrust -= ThrustChangeSpeed * Time.deltaTime;
            if (CurrentThrust <= 0) CurrentThrust = 0;
            //Debug.Log($"CurrentThrust: {CurrentThrust}");
        }
    }
}
