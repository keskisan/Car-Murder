
using UdonSharp;
using UnityEngine;
using VRC.Core;
using VRC.SDKBase;
using VRC.Udon;
using VRC.SDK3.Components;

public enum CARSTATUS
{
    NOTDRIVING, FORWARD, FORWARDLEFT, FORWARDRIGHT, REVERSE, REVERSELEFT, REVERSERIGHT, FORWARDSLIGHTRIGHT, FORWARDSLIGHTLEFT,
}

public enum TARGETSTATUS
{
    BEHIND, LEFTBEHIND, LEFT,LEFTINFRONT, INFRONT, RIGHTINFRONT, RIGHT, RIGHTBEHIND, CLOSEBEHIND, CLOSELEFTBEHIND, CLOSELEFT,
    CLOSELEFTINFRONT, CLOSEINFRONT, CLOSERIGHTINFRONT, CLOSERIGHT, CLOSERIGHTBEHIND
}


[UdonBehaviourSyncMode(BehaviourSyncMode.Continuous)]
public class CarDriveAI : UdonSharpBehaviour
{
    [SerializeField]
    GameManager gameManager;

    Vector3 targetPositionTransform;

    [SerializeField]
    private Wheel[] wheels;

    [SerializeField]
    CarManager carManager;

    public string carName;

    public VRCObjectSync objectSync;

    public FindPath findPath;
    RaycastHit hit, hit1;

    [SerializeField]
    float turnAmountMax = 1000f;
    [SerializeField]
    float turnSpeed = 150f;

    float turnAmount = 0f;

    public float rotationSpeed = 5f;

    Rigidbody rb;

    Vector3 halfExtents;

    [SerializeField]
    PlayerManager playerManager;

    Player targetplayer;
    
    float forwardAmount = 0f;

    float changeOwnershipTime = 10f;
    float changeTimer = 0f;

    float distanceToTarget;

    
    [SerializeField]
    float distanceClose = 8f;

    [SerializeField]
    LayerMask layerMask;

    float myTimer = 100f;

    LocationMarker[] pathMarkers;

    float reverseTimer = 0f;
    [SerializeField]
    float reverseTimerMin = 0.2f, reverseTimerMax = 1f;
    bool reverseForASecond = false;
    bool randomRirection;
    float jumpedOnCarTimer = 0f;
    bool jumpedOnCarBool = false;

    [SerializeField]
    ParticleSystem partilesExplosion;

    float explosionTimer = -2f;

    [SerializeField]
    float forwardClip = 10f;

    [SerializeField]
    float forwardSideClip = 65f;

    [SerializeField]
    float backwardSideClip = 115f;

    [SerializeField]
    float backClip = 170f;

    public int carhealthDefault = 100;

    [SerializeField]
    SoundManagerCar soundManagerCar;

    [UdonSynced, FieldChangeCallback(nameof(CarHealth))]
    private int _carHealth;
    public int CarHealth
    {
        set
        {
            gameManager.logger.LogEvent("Gamemanager Car Health changed to " + value);
            if (_carHealth > 0 & value <= 0)
            {
                Idle();
            }
            _carHealth = Mathf.Max(0, value);
        }
        get => _carHealth;
    }

    public void PlayerJumpedOnCar() //this will only allow car to react to current target. ie not networked
    {
        jumpedOnCarTimer = 1f;
    }

    public void HTZ()
    {
        CarHealth = 0;
        CarExplode();
        gameManager.logger.LogEvent("CarDriveAI car dead");
        
    }

    public void SetHealthToZero()
    {
        SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "HTZ");
    }

    public void TFD()
    {
        if (Networking.IsOwner(gameObject))
        {
            CarHealth = CarHealth - 40;
        } 
        gameManager.logger.LogEvent("CarDriveAI car lost 40");
        CarExplode();

    }

    public void TakeFourtyDamage()
    {
        SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "TFD");
    }

    private void CarExplode()
    {
        partilesExplosion.Play();
        explosionTimer = 5f;
        soundManagerCar.PlayExplosionSound();
    }

    private void Start()
    {
        partilesExplosion.Stop();
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = new Vector3(0f, -0.5f, 0f);
        halfExtents = new Vector3(2f, 0.5f, 2f);
        targetplayer = playerManager.players[0]; //temp set to master
    }

    private void ReverseTime()
    {
        if (reverseTimer > 0f)
        {
            reverseTimer -= Time.deltaTime;
            reverseForASecond = true;
        } else
        {
            reverseForASecond = false;
        }
    }

    private void jumpedOnCarTime()
    {
        if (jumpedOnCarTimer > 0f)
        {
            jumpedOnCarTimer -= Time.deltaTime;
            jumpedOnCarBool = true;
        }
        else
        {
            jumpedOnCarBool = false;
        }
    }

    private void Update()
    {
        StopIfExploding();

        if (!Networking.IsOwner(Networking.LocalPlayer, gameObject)) return;
        if (!carManager.CarsAreActive)
        {
            Idle();
            return;
        }
        if (CarHealth <= 0) return;
        if (!playerManager.IsPlayerValidAndIngame(targetplayer))
        {
            Idle();
            ChangeOwner();
            return;
        }

        ChangeOwnerEverySoOften();
        Vector3 targetPosition = SetTargetPosition();
        Debug.DrawLine(transform.position, targetPosition, Color.green);
        ReverseTime();
        jumpedOnCarTime();
        TARGETSTATUS targetStatus = CalculateTargetStatus(targetPosition);

        CARSTATUS carstatus = CalculateCarStatus(targetStatus);

        CarLogic(carstatus);
    }

    private void StopIfExploding()
    {
        if (explosionTimer > 0f)
        {
            explosionTimer -= Time.deltaTime;
        } 
        else if (explosionTimer > -1f)
        {

            if (CarHealth <= 0)
            {
                transform.position = carManager.transform.position; //spawn out if dead
                carManager.CheckIfCarsDeadGameOver();
            }
            explosionTimer = -2f;
        } 
        else
        {
            partilesExplosion.Stop();
        }
    }

    private void Idle()
    {
        foreach (Wheel weel in wheels)
        {
            weel.SetInputs(0f, 0f); //do nothing
        }
    }

    private void ChangeOwnerEverySoOften()
    {
        if (changeOwnershipTime < 1f)
        {
            changeOwnershipTime = Random.Range(5f, 30f);
        }


        changeTimer += Time.deltaTime;
        if (changeTimer > changeOwnershipTime)
        {
            ChangeOwner();
        }
    }

    public void ChangeOwner() //when player dies just change owner as dead player most likely target
    {
        targetplayer = playerManager.GetRandomPlayer();
        if (!playerManager.IsPlayerValidAndIngame(targetplayer))
        {
            gameManager.logger.LogEvent("CardriveAI changeOwner, player " + targetplayer.gameObject.name + "not valid");
            return;
        }
        gameManager.logger.LogEvent("CardriveAI changeOwner, player " + targetplayer.gameObject.name + "is valid");
        Networking.SetOwner(Networking.GetOwner(targetplayer.gameObject), gameObject);
        changeOwnershipTime = 0f;
        changeTimer = 0f;
    }

    public Vector3 SetTargetPosition()
    {
        targetPositionTransform = Networking.GetOwner(gameObject).GetTrackingData(VRCPlayerApi.TrackingDataType.Head).position;

        myTimer += Time.deltaTime;

        if (myTimer > 3f) //check this every so often and change route if necessary
        {
            myTimer = 0f;

            Vector3 toPosition = targetPositionTransform;
            Vector3 direction = toPosition - transform.position;
            float distance = Vector3.Distance(transform.position, toPosition);

            if (Physics.BoxCast(transform.position, halfExtents, direction, out hit1, Quaternion.identity, distance, layerMask)) //hit something so a star
            {

                pathMarkers = findPath.FindPathToTarget(transform.position, targetPositionTransform, gameManager.CurrentMap.locationMarkers);
            }
            else //clear path go directly
            {
                pathMarkers = null;
            }
        }

        if (pathMarkers != null) //if a star follow path if not or end of path follow target
        {
            Vector3 toPosition = targetPositionTransform;
            Vector3 direction = toPosition - transform.position;
            float distance = Vector3.Distance(transform.position, toPosition);

            if (!Physics.BoxCast(transform.position, halfExtents, direction, out hit1, Quaternion.identity, distance, layerMask)) // clear path to target go target
            {
                return targetPositionTransform;
            }
            else
            {
                //for (int i = pathMarkers.Length - 1; i >= 0; i--) //odd after a refraction of the code this changed
                for (int i = 0; i < pathMarkers.Length; i++)
                {
                    if (pathMarkers[i] != null)
                    {
                        toPosition = pathMarkers[i].transform.position;
                        direction = toPosition - transform.position;
                        distance = Vector3.Distance(transform.position, toPosition);

                        if (!Physics.BoxCast(transform.position, halfExtents, direction, out hit1, Quaternion.identity, distance, layerMask)) //clear path to this marker
                        {
                            return pathMarkers[i].transform.position;
                        }
                    }
                }
                return targetPositionTransform; //there is no path to player
            }
        }
        else
        {
            return targetPositionTransform;
        }
    }

    private TARGETSTATUS CalculateTargetStatus(Vector3 targetPosition)
    {
        distanceToTarget = Vector3.Distance(transform.position, targetPosition);

        Vector3 targetDir = targetPosition - transform.position;
        Vector3 forward = transform.forward;
        float angle = Vector3.SignedAngle(targetDir, forward, Vector3.up); // The angle returned will always be between -180 and 180 degrees, because the method returns the smallest angle between the vectors. That is, it will never return a reflex angle.

        if (distanceToTarget > distanceClose)
        {
            if (angle < -backClip)
            {
                return TARGETSTATUS.BEHIND;
            }
            if (angle < -backwardSideClip)
            {
                return TARGETSTATUS.LEFTBEHIND;
            }
            if (angle < -forwardSideClip)
            {
                return TARGETSTATUS.LEFT;
            }
            if (angle < -forwardClip)
            {
                return TARGETSTATUS.LEFTINFRONT;
            }
            if (angle < forwardClip)
            {
                return TARGETSTATUS.INFRONT;
            }
            if (angle < forwardSideClip)
            {
                return TARGETSTATUS.RIGHT;
            }
            if (angle < backwardSideClip)
            {
                return TARGETSTATUS.RIGHTINFRONT;
            }
            if (angle < backClip)
            {
                return TARGETSTATUS.RIGHTBEHIND;
            }
            return TARGETSTATUS.BEHIND;
        }
        else
        {
            if (angle < -backClip)
            {
                return TARGETSTATUS.CLOSEBEHIND;
            }
            if (angle < -backwardSideClip)
            {
                return TARGETSTATUS.CLOSELEFTBEHIND;
            }
            if (angle < -forwardSideClip)
            {
                return TARGETSTATUS.CLOSELEFT;
            }
            if (angle < -forwardClip)
            {
                return TARGETSTATUS.CLOSELEFTINFRONT;
            }
            if (angle < forwardClip)
            {
                return TARGETSTATUS.CLOSEINFRONT;
            }
            if (angle < forwardSideClip)
            {
                return TARGETSTATUS.CLOSERIGHT;
            }
            if (angle < backwardSideClip)
            {
                return TARGETSTATUS.CLOSERIGHTINFRONT;
            }
            if (angle < backClip)
            {
                return TARGETSTATUS.CLOSERIGHTBEHIND;
            }
            return TARGETSTATUS.CLOSEBEHIND;
        }
    }

    private bool TouchFrontLeft()
    {
        Vector3 left = transform.right * -1f;
        Vector3 forwardLeft = (transform.forward + left) * 0.5f;

        if (Physics.Raycast(transform.position, forwardLeft, distanceClose, layerMask))
        {
            return true;
        }
        return false;
        
    }

    private bool TouchFrontRight()
    {
        Vector3 forwardRight = (transform.forward + transform.right) * 0.5f;

        if (Physics.Raycast(transform.position, forwardRight, distanceClose, layerMask))
        {
            return true;
        }
        return false;    
    }

    private void SetReverseTimer(Collision collision)
    {
        Player hitplayer = collision.gameObject.GetComponent<Player>();

        if (hitplayer != null) return;

        ContactPoint contact = collision.GetContact(0);
        float dotProduct = Vector3.Dot(transform.forward, contact.point - transform.position);

        if (dotProduct > 0.5f)
        {
            reverseTimer = Random.Range(reverseTimerMin, reverseTimerMax);
            randomRirection = Random.Range(0f, 1f) > 0.5f ? true : false;
        }   
    }

    void OnCollisionEnter(Collision collision)
    {
        SetReverseTimer(collision);
    }

    void OnCollisionStay(Collision collision)
    {
        SetReverseTimer(collision);
    }

    private CARSTATUS RandomReverse()
    {
        if (randomRirection)
        {
            return CARSTATUS.REVERSELEFT;
        }
        return CARSTATUS.REVERSERIGHT;
    }

    private CARSTATUS CarstatusForwardModifier(CARSTATUS defaultStatus)
    {
        bool touchfrontLeft = TouchFrontLeft();
        bool touchfrontRight = TouchFrontRight();
        if (touchfrontLeft && touchfrontRight)
        {
            return CARSTATUS.REVERSE;
        }
        if (touchfrontLeft)
        {
            return CARSTATUS.FORWARDSLIGHTLEFT;
        }
        if (touchfrontRight)
        {
            return CARSTATUS.FORWARDSLIGHTRIGHT;
        }
        return defaultStatus;
    }

    private CARSTATUS CalculateCarStatus(TARGETSTATUS targetStatus)
    {
        if (reverseForASecond) //when crashed reverse out it
        {
            return RandomReverse();
        }

        if (jumpedOnCarBool)
        {
            return CarstatusForwardModifier(CARSTATUS.FORWARD);
        }

        switch (targetStatus)
        {
            case TARGETSTATUS.INFRONT:
                {     
                    return CarstatusForwardModifier(CARSTATUS.FORWARD);
                }
            case TARGETSTATUS.CLOSEINFRONT:
                {
                    return CARSTATUS.FORWARD;
                }
            case TARGETSTATUS.LEFTINFRONT:
                {

                    return CarstatusForwardModifier(CARSTATUS.FORWARDLEFT);
                }
            case TARGETSTATUS.CLOSELEFTINFRONT:
                {
                    return CARSTATUS.FORWARDLEFT;
                }
            case TARGETSTATUS.RIGHTINFRONT:
                {
                    return CarstatusForwardModifier(CARSTATUS.FORWARDRIGHT);
                }
            case TARGETSTATUS.CLOSERIGHTINFRONT:
                {
                    return CARSTATUS.FORWARDRIGHT;  
                }
            case TARGETSTATUS.BEHIND:
                {
                    return CarstatusForwardModifier(CARSTATUS.FORWARDLEFT);
                }
            case TARGETSTATUS.CLOSEBEHIND:
                {
                    return CARSTATUS.REVERSE;
                }
            case TARGETSTATUS.RIGHTBEHIND:
                {
                    return CarstatusForwardModifier(CARSTATUS.FORWARDRIGHT);
                }
            case TARGETSTATUS.CLOSERIGHTBEHIND:
                {
                    return CARSTATUS.REVERSERIGHT;
                }
            case TARGETSTATUS.CLOSERIGHT:
                {
                    return CarstatusForwardModifier(CARSTATUS.FORWARD); //stops going in circles round player
                }
            case TARGETSTATUS.RIGHT:
                {
                    return CarstatusForwardModifier(CARSTATUS.FORWARDRIGHT);
                }
            case TARGETSTATUS.LEFTBEHIND:
                {
                    return CarstatusForwardModifier(CARSTATUS.FORWARDLEFT);
                }
            case TARGETSTATUS.LEFT:
                {
                    return CarstatusForwardModifier(CARSTATUS.FORWARDLEFT);
                }
            case TARGETSTATUS.CLOSELEFTBEHIND:
                {
                    return CARSTATUS.REVERSELEFT;
                }
            case TARGETSTATUS.CLOSELEFT:
                {
                    return CarstatusForwardModifier(CARSTATUS.FORWARD); //stops going in circles round player
                }


            default:
                {
                    gameManager.logger.LogError("CarDriveAI Invalid Target status");
                    return CARSTATUS.FORWARD;
                }
        }
    }


    
 
    private void CarLogic(CARSTATUS state)
    {
        switch (state)
        {
            case CARSTATUS.NOTDRIVING:
                {
                    forwardAmount = 0f;
                    turnAmount = 0f;
                    break;
                }
            case CARSTATUS.FORWARD:
                {
                    
                    turnAmount = 0f;
                    forwardAmount = 1f;
                    break;
                }
            case CARSTATUS.FORWARDLEFT:
                {
                    turnAmount += turnSpeed * Time.deltaTime;
                    forwardAmount = 1f;
                    break;
                }
            case CARSTATUS.FORWARDRIGHT:
                {
                    turnAmount -= turnSpeed * Time.deltaTime;
                    forwardAmount = 1f;
                    break;
                }
            case CARSTATUS.FORWARDSLIGHTLEFT:
                {
                    turnAmount += turnSpeed * 0.3f * Time.deltaTime;
                    forwardAmount = 1f;
                    break;
                }
            case CARSTATUS.FORWARDSLIGHTRIGHT:
                {
                    turnAmount -= turnSpeed * 0.3f * Time.deltaTime;
                    forwardAmount = 1f;
                    break;
                }
            case CARSTATUS.REVERSE:
                {
                    forwardAmount = -1f;
                    break;
                }
            case CARSTATUS.REVERSELEFT:
                {
                    turnAmount += turnSpeed * Time.deltaTime;
                    forwardAmount = -1f;
                    break;
                }
            case CARSTATUS.REVERSERIGHT:
                {
                    turnAmount -= turnSpeed * Time.deltaTime;
                    forwardAmount = -1f;
                    break;
                }
            default:
                {
                    gameManager.logger.LogError("CarDriveAI invalid state");
                    break;
                }

        }

        turnAmount = Mathf.Clamp(turnAmount, -turnAmountMax * Time.deltaTime, turnAmountMax * Time.deltaTime);

        foreach (Wheel weel in wheels)
        {
            weel.SetInputs(forwardAmount, turnAmount);
        }
    }

    



}
