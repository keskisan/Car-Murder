
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

[UdonBehaviourSyncMode(BehaviourSyncMode.None)]
public class Wheel : UdonSharpBehaviour //addforces to car so must be on owner
{
    [SerializeField]
    CarRigidBody carRigidBody;
    [SerializeField]
    Transform carTransform;
    [SerializeField]
    float springStrength = 1f, springDamper = 0.5f, suspensionRestDist = 0.25f;

    [SerializeField] 
    AnimationCurve tireGripFactorCurve;
    [SerializeField] 
    float tireMass = 0.2f;

    [SerializeField]
    LayerMask layerMask;
    RaycastHit tireRay;

    [SerializeField]
    float raylength = 1f;

    [SerializeField]
    float carTopSpeed = 10f;
    [SerializeField]
    float carAccelerateSpeed = 2f;
    [SerializeField]
    AnimationCurve powerCurve;

    [SerializeField]
    float rotationSpeed = 1f; //used to block rotation on some wheels mainly


    float forwards, turns;

    public void SetInputs(float forwardAmount, float turnAmount)
    {

        forwards = Mathf.Clamp(forwardAmount, -1, 1) * carAccelerateSpeed * Time.fixedDeltaTime;
        turns = turnAmount * rotationSpeed;
    }

    void FixedUpdate()
    {
        if (Networking.IsOwner(carRigidBody.gameObject))
        {
            if (Physics.Raycast(transform.position, Vector3.down, out tireRay, raylength, layerMask))
            {
                SuspensionSpringForce();
                SteeringForce();
                AccelerateAndBreak();
            }

            transform.localRotation = Quaternion.Euler(0f, turns, 0f);
        }  
    }

    private void SuspensionSpringForce()
    {
        Vector3 springDir = transform.up;

        Vector3 tireWorldVel = carRigidBody.GetpointVelocity(transform.position);

        float offset = suspensionRestDist - tireRay.distance;

        float vel = Vector3.Dot(springDir, tireWorldVel);

        float force = (offset * springStrength) - (vel * springDamper);

        carRigidBody.addForceAtPosition(springDir * force, transform.position);

        Debug.DrawLine(transform.position, tireRay.point, Color.white);
        Debug.DrawLine(transform.position, transform.position + springDir * force, Color.green);
    }

    private void SteeringForce()
    {
        Vector3 steeringDir = transform.right;

        Vector3 tireWorldVel = carRigidBody.GetpointVelocity(transform.position);

        float steeringVel = Vector3.Dot(steeringDir, tireWorldVel);

        float desiredVelChange = -steeringVel * tireGripFactorCurve.Evaluate(Mathf.Abs(transform.InverseTransformDirection(tireWorldVel).x)); //use lookup curve here

        float desiredAccel = desiredVelChange / Time.fixedDeltaTime;

        carRigidBody.addForceAtPosition(steeringDir * tireMass * desiredAccel, transform.position);
    }

    private void AccelerateAndBreak()
    {
        Vector3 accelDir = transform.forward;

        float carSpeed = Vector3.Dot(carTransform.forward, carRigidBody.velocity);

        float normalizedSpeed = Mathf.Clamp01(Mathf.Abs(carSpeed) / carTopSpeed);

        float availableTorque = powerCurve.Evaluate(normalizedSpeed) * forwards;

        carRigidBody.addForceAtPosition(accelDir * availableTorque, transform.position);
    }
}
