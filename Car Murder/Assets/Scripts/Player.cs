
using UdonSharp;
using UnityEngine;
using VRC.Core;
using VRC.SDKBase;
using VRC.Udon;
using static VRC.SDKBase.VRC_Pickup;

[UdonBehaviourSyncMode(BehaviourSyncMode.Manual)]
public class Player : UdonSharpBehaviour
{
    [SerializeField]
    GameManager gameManager;

    [SerializeField]
    PlayerManager playerManager;

    public BloodSplatter bloodSplatter;

    private VRCPlayerApi ownerPlayer = null; //owner if object in game
     
    [UdonSynced, FieldChangeCallback(nameof(PlayerHealthInt))]  //health > 0 player in game
    private int _playerHealthInt;
    public int PlayerHealthInt
    {
        set
        {
            if(value <= 0)
            {
                _playerHealthInt = 0;
            }
            else
            {
                _playerHealthInt = value;
            }

            gameManager.logger.LogEvent(Networking.GetOwner(gameObject).playerId +  "has " + gameObject.name + " and health changed to" + _playerHealthInt);
            if (Networking.GetOwner(gameObject) == Networking.LocalPlayer) gameManager.onScreenStats.SetHealthDisplay(_playerHealthInt);
        }
        get => _playerHealthInt;
    }



    private void Start()
    {
        SetOwnerPlayer();
    }

    public override void OnOwnershipTransferred(VRCPlayerApi player)
    {
        SetOwnerPlayer();
    }

    public void SetOwnerPlayer()
    {
        ownerPlayer = Networking.GetOwner(gameObject);
        if (ownerPlayer.isMaster) 
        {
            if (this != playerManager.players[0]) //returns to pool
            {
                ownerPlayer = null;
            }
        }
        else
        {
            SetHealth(0); //someone gets it
        }
    }

    void UpdatePosition()
    {
        if (ownerPlayer == null) return;
        transform.position = ownerPlayer.GetPosition();
        transform.rotation = ownerPlayer.GetRotation();
    }
    private void Update()
    {
        UpdatePosition();
    }

    public void SetHealth(int value)
    {
        PlayerHealthInt = value;
        RequestSerialization();
    }

    public void DefaultHealth()
    {
        PlayerHealthInt = playerManager.playerHealthAtStart;
        RequestSerialization();
    }

    public void ChangeHealth(int value)
    {
        PlayerHealthInt += value;
        if (PlayerHealthInt == 0)
        {
            if (ownerPlayer == Networking.LocalPlayer)
            {
                VRC_Pickup pickup = ownerPlayer.GetPickupInHand(PickupHand.Left);
                if (pickup != null) pickup.Drop();
                pickup = ownerPlayer.GetPickupInHand(PickupHand.Right);
                if (pickup != null) pickup.Drop();
                ownerPlayer.Respawn();
            }
        }
        RequestSerialization();
    }

    public bool IsPlayerInGame()
    {
        return PlayerHealthInt > 0;
    }

    public int GetHealth()
    {
        return PlayerHealthInt;
    }
}
