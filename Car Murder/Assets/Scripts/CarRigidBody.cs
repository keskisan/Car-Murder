
using Newtonsoft.Json.Linq;
using System.Reflection;
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

[UdonBehaviourSyncMode(BehaviourSyncMode.Continuous)]
public class CarRigidBody : UdonSharpBehaviour
{
    [SerializeField]
    PlayerManager playerManager;

    [SerializeField]
    CarDriveAI carDrive;


    [SerializeField]
    Rigidbody body;

    [SerializeField]
    SoundManagerCar soundManagerCar;

    Player hitplayer = null;

    VRCPlayerApi localplayer;

    [SerializeField]
    float oncarJumpHop = 2f;

    Vector3 oldVelocity, veryOldVelocity;

    [SerializeField]
    float hitPush = 70f;

    public Vector3 velocity
    {
        get
        {
            return body.velocity;
        }
        set
        {
        }
    }

    private void Start()
    {
        localplayer = Networking.LocalPlayer;
    }

    void FixedUpdate()
    {
        veryOldVelocity = oldVelocity; //lag a frame or 2 behind, property gets overwritten when needed
        oldVelocity = body.velocity;
    }

    public Vector3 GetpointVelocity(Vector3 position)
    {
        return body.GetPointVelocity(position);

    }

    public void addForceAtPosition(Vector3 directionForce, Vector3 position)
    {
        body.AddForceAtPosition(directionForce, position);
    }

    void OnCollisionEnter(Collision collision)
    {
        hitplayer = collision.gameObject.GetComponent<Player>();

        if (hitplayer == null) return;
        if (localplayer != Networking.GetOwner(hitplayer.gameObject)) return;

        ContactPoint contact = collision.GetContact(0);

        float dotProduct = Vector3.Dot(Vector3.up, (hitplayer.transform.position + Vector3.up) - contact.point); //on hit player if player not jump on car ie center of capsule which is 1 unit above axis is not above colision boint

        if (dotProduct < 0.9f) //player hit by car
        {
            localplayer.SetVelocity(veryOldVelocity.normalized * hitPush);  //local player
            hitplayer.ChangeHealth(-50);
            hitplayer.bloodSplatter.HitByCar(this);
            if (!hitplayer.IsPlayerInGame()) //player died change target
            {
                carDrive.ChangeOwner();
            }
        }
        else //player jump on car
        {
            carDrive.PlayerJumpedOnCar();
            body.AddForceAtPosition(new Vector3(0f, -200f, 0f), Networking.LocalPlayer.GetPosition());

            soundManagerCar.PlayBonnetSound();
            Vector3 newVelocity = localplayer.GetVelocity();
            newVelocity += new Vector3(0f, oncarJumpHop, 0f);
            localplayer.SetVelocity(newVelocity);
        }
    }

    public void PlaySplatSound()
    {
        soundManagerCar.PlaySplatSound();
    }

    public void PlayHonkSoundDelayed(float delay)
    {
        soundManagerCar.PlayHonkSoundDelayed(delay);
    }
}
