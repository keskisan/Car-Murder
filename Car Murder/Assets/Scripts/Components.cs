
using UdonSharp;
using UnityEngine;
using VRC.SDK3.Components;
using VRC.SDKBase;
using VRC.Udon;


[UdonBehaviourSyncMode(BehaviourSyncMode.Continuous)] //objectsynced
public class Components : BaseWeapons
{
    private Collider objectCollider = null;
    VRC_Pickup pickup; //might cause errors

    [UdonSynced]
    bool colliderActive;

    [SerializeField]
    GameObject componentObject;

    [SerializeField]
    VRCObjectSync objectSync;

    Vector3 startposition;

    private Rigidbody body;

    private void Start()
    {
        body = GetComponent<Rigidbody>();
        objectCollider = GetComponent<Collider>();
        pickup = GetComponent<VRC_Pickup>();
        colliderActive = true;
        objectActive = true;
        objectCollider.enabled = colliderActive;
        startposition = transform.position;
    }

    private void Update()
    {
        componentObject.SetActive(objectActive);
        objectCollider.enabled = colliderActive;
    }

    public override void OnPickup()
    {
        colliderActive = false;
    }

    public override void OnDrop()
    {
        colliderActive = true;
    }

    public override void BroughtTogether()
    {
        SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.Owner, "SPO");
    }

    public void SPI()
    {
        objectSync.FlagDiscontinuity();
        transform.position = gameManager.CurrentMap.spawnPointParts[Random.Range(0, gameManager.CurrentMap.spawnPointParts.Length)].position;
        colliderActive = true;
        objectActive = true;
        ObjectInGame = true;
        body.isKinematic = false;
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
        colliderActive = true;
        objectActive = true;
        ObjectInGame = false;
        body.isKinematic = true;
    }

    public override void SpawnWeaponsOut()
    {
        SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.Owner, "SPO");
    }
}
