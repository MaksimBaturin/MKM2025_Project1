using UnityEngine;

public class Rocket : PhysicsBody
{
    Vector2 Force = new Vector2(0, 0);

    float FuelMass = 1500;
    Vector2 FuelVelocity = new Vector2(0, -5);
    float FuelLossRate = 10f;



    public void Start()
    {
        Mass += FuelMass;
        base.Start();
    }
    public void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            float CoeffOfFuelLoss = FuelLossRate / Time.fixedDeltaTime;
            Force = -CoeffOfFuelLoss * FuelVelocity;
            FuelMass -= FuelLossRate;
            Mass -= FuelLossRate;
            Debug.Log(Force.ToString());
            ApplyForce(Force);
        }
        base.Update();
    }
}