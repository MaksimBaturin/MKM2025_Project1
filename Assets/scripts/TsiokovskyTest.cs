using UnityEngine;

public class TsiokovskyTest : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Rocket body = collision.otherCollider.GetComponentInParent<Rocket>();
        if (collision.collider.tag == "Test")
        {
            Vector2 ExpectedVelocity = body.MaxFuelVelocity * body.FuelVelocityDirection * Mathf.Log(body.MassOnStart / body.Mass) + body.AccelerationOfFreeFall * Time.fixedDeltaTime;
            Debug.Log($"Tsiolkovsky velocity: {Mathf.Abs(ExpectedVelocity.y)}\nActual velocity: {Mathf.Abs(body.Velocity.y)}");
        }
    }
}
