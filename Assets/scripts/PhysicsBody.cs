using UnityEditor.UI;
using UnityEngine;

public class PhysicsBody : MonoBehaviour
{
    public Vector2 Position;
    public Vector2 Velocity;
    public Vector2 Acceleration;
    private Vector2 PrevAcceleration;

    public float RotationAngle;
    public float AngularVelocity;
    public float AngularAcceleration;
    public float MomentOfInertia;

    public float Mass = 1f;
    public Vector2 AccelerationOfFreeFall = new Vector2(0, -9.8f);
    public bool useGravity = true;

    public bool IsOnFloor = false;

    public float TimeStep = 1f;

    Vector2 bodySize;


    public void Start()
    {
        bodySize = this.GetComponent<Renderer>().bounds.size;
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
        MomentOfInertia = 0.001f * Mass * (bodySize.y * 0.5f) * (bodySize.y * 0.5f);

        if (useGravity && !IsOnFloor)
        {
            Acceleration += AccelerationOfFreeFall;
        }

        // Верле в скоростной формулировке
        Position += Velocity * deltaTime + 0.5f * Acceleration * deltaTime * deltaTime;
        Velocity += 0.5f * (PrevAcceleration + Acceleration) * deltaTime;

        RotationAngle += AngularVelocity * deltaTime + 0.5f * AngularAcceleration * deltaTime * deltaTime;

        AngularVelocity += AngularAcceleration * deltaTime;

        if (Velocity.y > 0 && IsOnFloor)
        {
            IsOnFloor = false;
        }

        PrevAcceleration = Acceleration;
        Acceleration = Vector2.zero;

        AngularAcceleration = 0f;

        transform.position = Position;

        float eulerZ = RotationAngle * Mathf.Rad2Deg;
        
        transform.rotation = Quaternion.Euler(0, 0, eulerZ);

        //Debug.Log($"RotationAngle: {RotationAngle}, AngularVelocity: {AngularVelocity}, AngularAcceleration: {AngularAcceleration}, MomentOfInertia: {MomentOfInertia}");
    }

    public void ApplyForce(Vector2 force)
    {
        Acceleration += force / Mass;
    }

    public void ApplyTorque(float torque)
    {
        AngularAcceleration += torque / MomentOfInertia;
    }
}
