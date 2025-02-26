using UnityEngine;

public class PhysicsBody : MonoBehaviour
{
    public Vector2 Position;
    public Vector2 Velocity;
    private Vector2 Acceleration;
    private Vector2 PrevAcceleration;

    public float RotationAngle;
    public float AngularVelocity;
    public float AngularAcceleration;
    public float MomentOfInertia;

    public float Mass = 1f;
    public Vector2 AccelerationOfFreeFall = new Vector2(0, -9.8f);
    public bool useGravity = true;

    public bool IsOnFloor = false;
    private RaycastHit2D hitLeft;
    private RaycastHit2D hitRight;

    public float TimeStep = 1f;

    private Vector2 bodySize;


    public void Start()
    {
        bodySize = this.GetComponent<Renderer>().bounds.size;
        Time.fixedDeltaTime = TimeStep;
        Position = transform.position;
        PrevAcceleration = Acceleration;
    }

    public void FixedUpdate()
    {
        CheckIsOnFloor();
        ApplyPhysics(Time.fixedDeltaTime);
    }

    private void CheckIsOnFloor()
    {
        float rayLength = bodySize.y + 2f;

        Vector2 rayStartWorldLeft = new Vector2(transform.position.x - bodySize.x * 0.5f - 0.5f, transform.position.y + bodySize.y * 0.5f + 1f);
        Vector2 rayStartWorldRight = new Vector2(transform.position.x + bodySize.x * 0.5f, transform.position.y + bodySize.y * 0.5f + 1f);

        rayStartWorldLeft = RotatePointAroundPivot(rayStartWorldLeft, transform.position, transform.rotation.eulerAngles.z);
        rayStartWorldRight = RotatePointAroundPivot(rayStartWorldRight, transform.position, transform.rotation.eulerAngles.z);

        Vector2 rayDirection = transform.TransformDirection(Vector2.down);

        Debug.DrawRay(rayStartWorldLeft, rayDirection * rayLength, Color.red);
        Debug.DrawRay(rayStartWorldRight, rayDirection * rayLength, Color.blue);

        hitLeft = Physics2D.Raycast(rayStartWorldLeft, rayDirection, rayLength, Physics.DefaultRaycastLayers, -1f);
        hitRight = Physics2D.Raycast(rayStartWorldRight, rayDirection, rayLength, Physics.DefaultRaycastLayers, -1f);

        if (hitLeft.collider == null && hitRight.collider == null) IsOnFloor = false;
    }

    private Vector2 RotatePointAroundPivot(Vector2 point, Vector2 pivot, float angle)
    {
        float rad = angle * Mathf.Deg2Rad;
        float cos = Mathf.Cos(rad);
        float sin = Mathf.Sin(rad);

        Vector2 dir = point - pivot;
        Vector2 rotatedDir = new Vector2(dir.x * cos - dir.y * sin, dir.x * sin + dir.y * cos);

        return pivot + rotatedDir;
    }

    private void ApplyPhysics(float deltaTime)
    {
        MomentOfInertia = 0.5f * Mass * (bodySize.y * 0.5f) * (bodySize.y * 0.5f);

        if (useGravity && !IsOnFloor)
        {
            Acceleration += AccelerationOfFreeFall;
        }

        if (IsOnFloor)
        {
            Vector2 FrictionForce = 1.5f * Mass * AccelerationOfFreeFall.magnitude * (-Velocity.normalized);
            ApplyForce(FrictionForce);
        }

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

    public void ApplyForce(Vector2 force)
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