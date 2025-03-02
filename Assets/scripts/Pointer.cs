using UnityEngine;

public class Pointer : MonoBehaviour
{
    private GameObject EndPoint;
    private GameObject rocket;
    [SerializeField] private float Radius = 70f;

    void Start()
    {
        EndPoint = GameObject.FindGameObjectWithTag("EndPad");
        rocket = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        Vector2 direction = (EndPoint.transform.position - rocket.transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, angle);

        transform.position = (rocket.transform.position + (Vector3)direction * Radius);
    }
}
