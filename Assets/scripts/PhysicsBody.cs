
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
    private RaycastHit2D hit;
    private Ray2D ray;

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
        MomentOfInertia = 0.5f * Mass * (bodySize.y * 0.5f) * (bodySize.y * 0.5f);

        if (useGravity && !IsOnFloor)
        {
            Acceleration += AccelerationOfFreeFall;
        }

        ray = new Ray2D(transform.position, -transform.up);
        float rayLength = bodySize.y / 2 + 2f;
        Debug.DrawRay(transform.position, -transform.up * rayLength, Color.red);

        hit = Physics2D.Raycast(ray.origin, ray.direction, rayLength, layerMask: Physics2D.DefaultRaycastLayers, minDepth: -1);
        if (!hit)
        {
            IsOnFloor = false;
        }


        // Верле в скоростной формулировке
        Position += Velocity * deltaTime + 0.5f * Acceleration * deltaTime * deltaTime;
        Velocity += 0.5f * (PrevAcceleration + Acceleration) * deltaTime;

        RotationAngle += AngularVelocity * deltaTime + 0.5f * AngularAcceleration * deltaTime * deltaTime;
        AngularVelocity += AngularAcceleration * deltaTime;

        PrevAcceleration = Acceleration;
        Acceleration = Vector2.zero;
        AngularAcceleration = 0f;

        transform.position = Position;
        float eulerZ = RotationAngle * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, eulerZ);
    }
    public void ApplyReactiveForce(Vector2 force)
    {
        if (force.y > -(AccelerationOfFreeFall * Mass).y)
        {
            Acceleration += force / Mass;
        }

    }

    public void ApplyFrictionForce(Vector2 force)
    {
        Acceleration += force / Mass;
    }

    public void ApplyTorque(float torque)
    {
        AngularAcceleration += torque / MomentOfInertia;
    }

    public void ApplyCollisionResponse()
    {
        AngularVelocity *= 0.5f;
    }
}
