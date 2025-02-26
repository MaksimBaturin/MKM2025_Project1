using JetBrains.Annotations;
using UnityEngine;

public class FrictionForce : MonoBehaviour
{
    public float FrictionCoefficient = 0.8f;
    private void OnCollisionEnter2D(Collision2D collision)
    {

        Rocket body = collision.otherCollider.GetComponentInParent<Rocket>();
        if (collision.collider.tag == "Terrain" && body.Velocity.magnitude > 0)
        {
            Vector2 FrictionForce = FrictionCoefficient * body.Mass * body.AccelerationOfFreeFall.magnitude * (-body.Velocity.normalized);
            body.ApplyForce(FrictionForce);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        Rocket body = collision.otherCollider.GetComponentInParent<Rocket>();
        if (collision.collider.tag == "Terrain" && body.Velocity.magnitude > 0)
        {
            Vector2 normal = collision.GetContact(0).normal;
            Vector2 tangent = new Vector2(-normal.y, normal.x);

            float frictionMagnitude = FrictionCoefficient * body.Mass * body.AccelerationOfFreeFall.magnitude;
            Vector2 FrictionForce = -tangent * frictionMagnitude * Vector2.Dot(body.Velocity.normalized, tangent);

            body.ApplyForce(FrictionForce);
        }
    }
}
