using UnityEditor.UI;
using UnityEngine;

public class Rocket : PhysicsBody
{
    Vector2 Force = new Vector2(0, 0);

    public float FuelMass = 1500;
    public float MaxFuelVelocity = 50;
    public float CurrentFuelVelocity;
    public float FuelLossRate = 10f;

    public float TorqueForce = 1f;

    public float MassOnStart { get; private set; }

    public ThrustController thrustController;

    public Vector2 FuelVelocityDirection { get; private set; }

    public Animator ExplosionAnimator;

    private bool isDeath = false;
    public bool IsDeath
    {
        get { return isDeath; }
        set { if (value == true)
            {
                isDeath = true;
                ExplosionAnimator.SetBool("IsDeath", isDeath);
            }
        } 
    }
        
    public void Start()
    {
        IsDeath = false;
        thrustController = GetComponentInChildren<ThrustController>();
        Mass += FuelMass;
        MassOnStart = Mass;
        base.Start();

        ExplosionAnimator = GameObject.Find("Explosion").GetComponent<Animator>();
    }

    public void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.F))
        {
            IsDeath = true;
        }
        if (Input.GetKey(KeyCode.A))
        {
            ApplyTorque(TorqueForce);
        }
        if (Input.GetKey(KeyCode.D))
        {
            ApplyTorque(-TorqueForce);
        }

        FuelVelocityDirection = -transform.up; 

        CurrentFuelVelocity = MaxFuelVelocity;
        if (FuelMass > 0)
        {
            float Mu = FuelLossRate * thrustController.CurrentThrust / Time.fixedDeltaTime;
            Force = CurrentFuelVelocity * FuelVelocityDirection * Mu;
            FuelMass -= FuelLossRate * thrustController.CurrentThrust;
            if (FuelMass < 0) FuelMass = 0;
            Mass -= FuelLossRate * thrustController.CurrentThrust;

            ApplyForce(Force);
        }

        base.FixedUpdate();
    }
}