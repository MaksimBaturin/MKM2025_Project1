using System.Runtime.CompilerServices;
using UnityEngine;

public class Collider : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Rocket body = collision.otherCollider.GetComponentInParent<Rocket>();

        if (collision.collider.tag == "Terrain")
        {
            ContactPoint2D contact = collision.GetContact(0);
            Vector2 normal = contact.normal;

            // Отражение скорости относительно нормали
            body.Velocity = Vector2.Reflect(body.Velocity, normal) * 0.3f;

            if (Vector2.Dot(normal, Vector2.up) > 0.7f)
            {
                body.IsOnFloor = true;
                body.Velocity.y = 0;
            }

            body.ApplyCollisionResponse();
        }
    }


    private void OnCollisionStay2D(Collision2D collision)
    {
        Rocket body = collision.otherCollider.GetComponentInParent<Rocket>();
        Vector2 bodySize = body.GetComponent<Renderer>().bounds.size;
        if (collision.collider.tag == "Terrain" && body.IsOnFloor)
        {
            body.Position.y = collision.GetContact(0).point.y + bodySize.y / 2;

        }
        
    }
}


