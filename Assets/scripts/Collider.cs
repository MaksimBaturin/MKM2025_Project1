using UnityEngine;

public class Collider : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        PhysicsBody body = collision.otherCollider.GetComponentInParent<PhysicsBody>();
        Debug.Log(body.ToString());
        switch (collision.otherCollider.tag)
        {
            case "LeftCollider":
                Debug.Log("left");
                body.Velocity.x = Mathf.Max(0, body.Velocity.x);
                break;
            case "RightCollider":
                Debug.Log("right");
                body.Velocity.x = Mathf.Min(0, body.Velocity.x);
                break;
            case "TopCollider":
                Debug.Log("top");
                body.Velocity.y = Mathf.Min(0, body.Velocity.y);
                break;
            case "BottomCollider":
                Debug.Log("bottom");
                body.Velocity.y = 0;
                body.IsOnFloor = true;
                break; 
        }
    }
}
