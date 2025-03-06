using UnityEngine;
using TMPro;
public class UIVelocity : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI Text;
    [SerializeField] private Rocket rocket;
    void Start()
    {
        rocket = GameObject.FindGameObjectWithTag("Player").GetComponent<Rocket>();
    }


    void Update()
    {
        Text.text = $"������� ��������: {rocket.Velocity.magnitude}\n �������� �� ������������: {rocket.TsiolkovskyVelocity.magnitude}";
    }
}
