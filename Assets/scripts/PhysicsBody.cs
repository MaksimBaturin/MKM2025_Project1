using UnityEngine;

public class PhysicsBody : MonoBehaviour
{
    public Vector2 Position;
    public Vector2 Velocity;
    public Vector2 Acceleration;

    public float Mass = 1f;
    public Vector2 AccelerationOfFreeFall = new Vector2(0, -9.8f);
    public bool useGravity = true;

    public bool IsOnFloor = false;

    public float TimeStep = 1f;

    public void Start()
    {
        Time.fixedDeltaTime = TimeStep;
        Position = transform.position;
    }

    public void Update()
    {
       
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

        Velocity += Acceleration * deltaTime;
        Position += Velocity * deltaTime;

        if (Velocity.y > 0 && IsOnFloor)
        {
            IsOnFloor = false;
        }

        Acceleration = Vector2.zero;
        transform.position = Position;
        //Debug.Log($"Position: {Position}, Velocity: {Velocity}, Acceleration: {Acceleration}");
    }

    public void ApplyForce(Vector2 force)
    {
        Acceleration += force / Mass;
    }
}
