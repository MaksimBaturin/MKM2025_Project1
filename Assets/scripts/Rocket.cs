using UnityEngine;

public class Rocket : PhysicsBody
{
    Vector2 Force = new Vector2(0, 0);

    public float FuelMass = 1500;
    public Vector2 MaxFuelVelocity = new Vector2(0, -50);
    public Vector2 CurrentFuelVelocity = Vector2.zero;
    public float FuelLossRate = 10f;

    public float MassOnStart;

    public ThrustController thrustController;



    public void Start()
    {
        thrustController = GetComponentInChildren<ThrustController>();
        Mass += FuelMass;
        MassOnStart = Mass;
        base.Start();
    }


    public void FixedUpdate()
    {

        CurrentFuelVelocity = MaxFuelVelocity * thrustController.CurrentThrust;
        if (FuelMass > 0 && CurrentFuelVelocity.y < 0)
        {
            float CoeffOfFuelLoss = FuelLossRate / Time.fixedDeltaTime;
            Force = -CurrentFuelVelocity * CoeffOfFuelLoss;

            FuelMass -= FuelLossRate * thrustController.CurrentThrust;
            if (FuelMass < 0) FuelMass = 0;
            Mass -= FuelLossRate * thrustController.CurrentThrust;

            ApplyForce(Force);
        }
        base.FixedUpdate();
    }
}