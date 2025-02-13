using UnityEngine;

public class Rocket : PhysicsBody
{
    Vector2 Force = new Vector2(0, 0);

    public float FuelMass = 1500;
    public Vector2 FuelVelocity = new Vector2(0, -50);
    public float FuelLossRate = 10f;



    public void Start()
    {
        Mass += FuelMass;
        base.Start();
    }
    public void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {   
            if (FuelMass > 0)
            {
                float CoeffOfFuelLoss = FuelLossRate / Time.fixedDeltaTime;
                Force = -CoeffOfFuelLoss * FuelVelocity;

                FuelMass -= FuelLossRate;
                Mass -= FuelLossRate;

                Debug.Log(Force.ToString());
                ApplyForce(Force);
            }
        }
        base.Update();
    }
}