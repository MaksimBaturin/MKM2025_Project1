using UnityEngine;

public class PhysicsBody : MonoBehaviour
{
    public Vector2 Position;
    public Vector2 Velocity;
    public Vector2 Acceleration;
    private Vector2 PrevAcceleration;

    public float Mass = 1f;
    public Vector2 AccelerationOfFreeFall = new Vector2(0, -9.8f);
    public bool useGravity = true;

    public bool IsOnFloor = false;

    public float TimeStep = 1f;

    public void Start()
    {
        Time.fixedDeltaTime = TimeStep;
        Position = transform.position;
        PrevAcceleration = Acceleration;
    }

    public void FixedUpdate()
    {
        ApplyPhysics(Time.fixedDeltaTime);
    }

    private void ApplyPhysics(float deltaTime)
    {
        if (useGravity && !IsOnFloor)
        {
            Acceleration += AccelerationOfFreeFall;
        }

        // Верле в скоростной формулировке
        Position += Velocity * deltaTime + 0.5f * Acceleration * deltaTime * deltaTime;
        Velocity += 0.5f * (PrevAcceleration + Acceleration) * deltaTime;

        if (Velocity.y > 0 && IsOnFloor)
        {
            IsOnFloor = false;
        }

        PrevAcceleration = Acceleration;
        Acceleration = Vector2.zero;
        transform.position = Position;
    }

    public void ApplyForce(Vector2 force)
    {
        Acceleration += force / Mass;
    }
}
