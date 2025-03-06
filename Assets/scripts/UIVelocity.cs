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
        Text.text = $"Текущая скорость: {rocket.Velocity.magnitude}\n Скорость по Циолковскому: {rocket.TsiolkovskyVelocity.magnitude}";
    }
}
