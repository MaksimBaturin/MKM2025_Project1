using System.Runtime.CompilerServices;
using UnityEngine;

public class Collider : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Rocket body = collision.otherCollider.GetComponentInParent<Rocket>();

        if (collision.collider.tag == "Terrain")
        {
            body.IsOnFloor = true;
            body.Velocity.y = 0;
            body.ApplyCollisionResponse();
        }
    }


    private void OnCollisionStay2D(Collision2D collision)
    {
        Rocket body = collision.otherCollider.GetComponentInParent<Rocket>();
        Vector2 bodySize = body.GetComponent<Renderer>().bounds.size;
        if (collision.collider.tag == "Terrain")
        {
            body.Position.y = collision.GetContact(0).point.y + bodySize.y / 2;

        }
        
    }
}


