using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class CameraBehaviour : MonoBehaviour
{
    [SerializeField] private Rocket rocket;
    [SerializeField]private Vector3 offset;
    private void Start()
    {
        rocket = GameObject.FindAnyObjectByType<Rocket>();
        offset = transform.position - rocket.transform.position;
    }
    private void LateUpdate()
    {
        transform.position = rocket.transform.position + offset;
    }
}   

