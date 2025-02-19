using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class CameraBehaviour : MonoBehaviour
{
    private Rocket rocket;
    private Vector3 offset;
    private void Start()
    {
        rocket = GameObject.FindGameObjectWithTag("Player").GetComponent<Rocket>();
        offset = transform.position - rocket.transform.position;
    }
    private void LateUpdate()
    {
        transform.position = rocket.transform.position + offset;
    }
}   

