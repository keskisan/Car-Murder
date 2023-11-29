
using System;
using UdonSharp;
using UnityEngine;
using VRC.SDK3.Components;
using VRC.SDKBase;
using VRC.Udon;


[UdonBehaviourSyncMode(BehaviourSyncMode.Continuous)]
public class TNT : BaseWeapons
{
    [SerializeField]
    WeaponsManager weaponsManager;

    [SerializeField]
    VRC_Pickup pickup;

    [SerializeField]
    float distanceCanpPlace = 3f;

    [SerializeField]
    LayerMask layerMaskCarOnly;

    [SerializeField]
    VRCObjectSync objectSync;

    RaycastHit hit;

    Collider objectCollider;

    float countDownTimer = -2f;

    CarDriveAI driveAI = null;

    Vector3 startposition;

    private Rigidbody body;

    [SerializeField]
    float broughtTogetherDistance = 3f;

    [UdonSynced]
    bool clockActive, wireActive;

    [SerializeField]
    GameObject clockOnTNT, wireOnTNT;

    void Start()
    {
        objectCollider = GetComponent<Collider>();
        startposition = transform.position;

        body = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        clockOnTNT.SetActive(clockActive);
        wireOnTNT.SetActive(wireActive);


        if (!Networking.IsOwner(gameObject)) return;

        CountdownToExplode();
        CheckForComponentsInProximity();
    }

    private void CheckForComponentsInProximity()
    {
        if (!pickup.IsHeld) return;

        if (!wireActive)
        {
            for (int i = 0; i < weaponsManager.wires.Length; i++)
            {
                if (weaponsManager.wires[i].objectActive)
                {
                    if (Vector3.Distance(transform.position, weaponsManager.wires[i].transform.position) < broughtTogetherDistance)
                    {
                        wireActive = true;
                        weaponsManager.wires[i].BroughtTogether();
                    }
                }
            }
        }           

        if (!clockActive)
        {
            for (int i = 0; i < weaponsManager.clocks.Length; i++)
            {
                if (weaponsManager.clocks[i].objectActive)
                {
                    if (Vector3.Distance(transform.position, weaponsManager.clocks[i].transform.position) < broughtTogetherDistance)
                    {
                        clockActive = true;
                        weaponsManager.clocks[i].BroughtTogether();
                    }
                }
            }
        }       
    }

    private void CountdownToExplode()
    {
        if (countDownTimer >= 0f)
        {
            countDownTimer -= Time.deltaTime;
            gameManager.onScreenStats.SetCountdownTimerText((int)countDownTimer);
        }
        else if (countDownTimer > -1f)
        {
            countDownTimer = -2f;
            gameManager.logger.LogEvent("TNT kills car");
            driveAI.TakeFourtyDamage();
            transform.parent = null;
            SPO(); //spawn out
            
        }
    }

    public override void OnPickup()
    {
        objectCollider.enabled = false;
    }

    public override void OnDrop()
    {
        objectCollider.enabled = true;
        
        if (!clockActive) return;
        if (!wireActive) return;

        transform.rotation = Networking.LocalPlayer.GetTrackingData(VRCPlayerApi.TrackingDataType.Head).rotation;

        if (Physics.Raycast(transform.position, transform.forward, out hit, distanceCanpPlace, layerMaskCarOnly))
        {
            objectCollider.enabled = false;
            transform.position = hit.point;
            GameObject carObject = hit.collider.gameObject;
            transform.parent = carObject.transform;
            body.isKinematic = true;
            countDownTimer = 5f;

            driveAI = carObject.GetComponent<CarDriveAI>();
            if (driveAI == null)
            {
                gameManager.logger.LogError("TNT onDrop returned a non cardriveai object");
                return;
            }
        }
    }


    public void SPI()
    {
        clockActive = false;
        wireActive = false;
        objectSync.FlagDiscontinuity();
        transform.position = gameManager.CurrentMap.spawnPointParts[UnityEngine.Random.Range(0, gameManager.CurrentMap.spawnPointParts.Length)].position;
        body.isKinematic = false;
        objectCollider.enabled = true;
        ObjectInGame = true;
    }

    public override void SpawnWeaponsIn()
    {
        SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.Owner, "SPI");
    }

    public void SPO()
    {
        pickup.Drop();
        objectSync.FlagDiscontinuity();
        transform.position = startposition;
        body.isKinematic = true;
        objectCollider.enabled = false;
        ObjectInGame = false;
    }

    public override void SpawnWeaponsOut()
    {
        SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.Owner, "SPO");
    }
}
