using System.Runtime.CompilerServices;
using UnityEngine;

public class Collider : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Rocket body = collision.otherCollider.GetComponentInParent<Rocket>();
        Vector2 bodySize = body.GetComponent<Renderer>().bounds.size;
        if (collision.collider.tag == "Terrain")
        {
            switch (collision.otherCollider.tag)
            {
                case "LeftCollider":
                    //Debug.Log("left");
                    //body.Velocity.x = Mathf.Max(0, body.Velocity.x);
                    break;
                case "RightCollider":
                    //Debug.Log("right");
                    //body.Velocity.x = Mathf.Min(0, body.Velocity.x);
                    break;
                case "TopCollider":
                    //Debug.Log("top");
                    //body.Velocity.y = Mathf.Min(0, body.Velocity.y);
                    break;
                case "BottomCollider":
                    //Debug.Log("bottom");
                    body.Velocity.y = 0;
                    body.IsOnFloor = true;
                    break;
            }
        }
        else if(collision.collider.tag == "Test")
        {
            Vector2 ExpectedVelocity = body.MaxFuelVelocity * body.FuelVelocityDirection * Mathf.Log(body.MassOnStart / body.Mass) +  body.AccelerationOfFreeFall * Time.fixedDeltaTime;
            Debug.Log($"Tsiolkovsky velocity: {Mathf.Abs(ExpectedVelocity.y)}\nActual velocity: {Mathf.Abs(body.Velocity.y)}");
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        Rocket body = collision.otherCollider.GetComponentInParent<Rocket>();
        Vector2 bodySize = body.GetComponent<Renderer>().bounds.size;
        if (collision.collider.tag == "Terrain")
        {
            switch (collision.otherCollider.tag)
            {
                case "LeftCollider":
                    //Debug.Log("left");
                    //body.Velocity.x = Mathf.Max(0, body.Velocity.x);
                    break;
                case "RightCollider":
                    //Debug.Log("right");
                    //body.Velocity.x = Mathf.Min(0, body.Velocity.x);
                    break;
                case "TopCollider":
                    //Debug.Log("top");
                    //body.Velocity.y = Mathf.Min(0, body.Velocity.y);
                    break;
                case "BottomCollider":
                    //Debug.Log("bottom");
                    body.Position.y = collision.collider.bounds.max.y + bodySize.y / 2;
                    break;
            }
        }
        
    }
}


