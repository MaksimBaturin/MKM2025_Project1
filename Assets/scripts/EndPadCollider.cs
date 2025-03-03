using UnityEngine;

public class EndPadCollider : MonoBehaviour
{
    private Rocket rocket;
    void Start()
    {
        rocket = GameObject.FindGameObjectWithTag("Player").GetComponent<Rocket>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player" && collision.otherCollider.tag == "EndPad") {
            rocket.IsWin = true;
        }
    }
}
