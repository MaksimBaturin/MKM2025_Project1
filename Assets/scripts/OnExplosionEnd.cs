using Unity.VisualScripting;
using UnityEngine;

public class OnExplosionEnd : MonoBehaviour
{
    private Rocket rocket;
    void Start()
    {
        rocket = GameObject.FindGameObjectWithTag("Player").GetComponent<Rocket>();
    }

    private void onExplosionEnd()
    {
        rocket.enabled = false;
        GameObject.Find("RocketSprite").GetComponent<SpriteRenderer>().enabled = false;
        Destroy(GameObject.Find("ParamSaver"));
    }
}
