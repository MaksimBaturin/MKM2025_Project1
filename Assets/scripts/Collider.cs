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
        else if (collision.collider.tag == "Wall")
        {
            ContactPoint2D contact = collision.GetContact(0);
            float bodyWidth = body.GetComponent<Renderer>().bounds.size.x / 2;

            if (Mathf.Abs(contact.normal.x) > 0.1f)
            {
                body.Velocity.x = 0;
                body.Position.x = contact.point.x + (contact.normal.x * bodyWidth);
            }
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
        else if (collision.collider.tag == "Wall")
        {
            ContactPoint2D contact = collision.GetContact(0);
            float bodyWidth = bodySize.x / 2;

            if (Mathf.Abs(contact.normal.x) > 0.1f)
            {
                body.Velocity.x = 0;
                body.Position.x = contact.point.x + (contact.normal.x * bodyWidth);
            }
        }
    }
}