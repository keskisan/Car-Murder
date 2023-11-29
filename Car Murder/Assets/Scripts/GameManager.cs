using Newtonsoft.Json.Linq;
using System;
using UdonSharp;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;
using VRC.SDKBase;
using VRC.Udon;
using static VRC.SDKBase.VRC_Pickup;

//car sometimes stall. happend when master died but client still alive

public enum GAMESTATUS
{
    NOTPLAYING, ABORTED, WON, LOST, MAP1, MAP2, MAP3, MAP4, MAP5, NumberOfTypes
}


[DefaultExecutionOrder(-1), UdonBehaviourSyncMode(BehaviourSyncMode.Manual)]
public class GameManager : UdonSharpBehaviour
{
    public Loger logger;
    public PlayerManager playerManager;

    public MenuManager menuManager;

    [SerializeField]
    Map[] maps;

    [HideInInspector]
    public Map CurrentMap { get; private set; }

    [SerializeField]
    Transform spawnInArea, startPosition;

    [SerializeField]
    float spawnIndistance;

    public CarManager carManager;

    [SerializeField]
    WeaponsManager weaponsManager;

    public OnScreenStats onScreenStats;

    private float countdown = -1f;

    private float time_player_teleports_in = 5f;

    private bool resetHappenOnce = true;

    VRCPlayerApi localPlayer;

    [SerializeField]
    int startHealth = 100;

    public MusicController musicController;

    [UdonSynced, FieldChangeCallback(nameof(Status))]
    private GAMESTATUS _status;

    [SerializeField]
    GameObject lobby, lobbySky;

    public GAMESTATUS Status
    {
        set
        {
            if (_status == GAMESTATUS.LOST && value == GAMESTATUS.WON) return; //avoids bug where status change to won on car despawn
            if (Networking.IsMaster && countdown > 0f) return;
            logger.LogEvent("Gamemanager Gamestatus changed to " + value);
            hideAllMaps();
            if  ((int)value >= (int)GAMESTATUS.MAP1)
            {
                countdown = 10f;
                resetHappenOnce = true;
                UpdateMap(value);
            }
            else
            {
                EndGame(value);
            }
            menuManager.UpdateMenuItemVisibility(value, maps);
            _status = value;
        }
        get => _status;
    }

    public bool IsGamePlaying()
    {
        return (int)Status >= (int)GAMESTATUS.MAP1;
    }

    private void hideAllMaps()
    {
        for (int i = 0; i < maps.Length; i++)
        {
            maps[i].mapObject.SetActive(false);
        }
    }

    public void GameWon()
    {
        logger.LogEvent("GameManager Game Won");
        Status = GAMESTATUS.WON;
        RequestSerialization();
    }

    private void EndGame(GAMESTATUS value)
    {
        if (playerManager.LocalPlayerCapsule != null) playerManager.LocalPlayerCapsule.SetHealth(0); //avoids a bug on start 

        switch (value)
        {
            case GAMESTATUS.NOTPLAYING:
                {   
                    break;
                }
            case GAMESTATUS.ABORTED:
                {
                    weaponsManager.SpawnWeaponsOut();
                    carManager.SpawnCarsOut();
                    TeleportPlayersOut();
                    break;
                }
            case GAMESTATUS.LOST:
                {
                    weaponsManager.SpawnWeaponsOut();
                    carManager.SpawnCarsOut();
                    TeleportPlayersOut();
                    break;
                }
            case GAMESTATUS.WON:
                {
                    weaponsManager.SpawnWeaponsOut();
                    //cars died
                    TeleportPlayersOut();
                    break;
                }
            default:
                {
                    logger.LogError("GameManager Endgame function called with invalid value");
                    break;
                }

        }
    }
    private void TeleportPlayersOut()
    {
        if (playerManager.LocalPlayerCapsule.IsPlayerInGame())
        {
            /*VRC_Pickup pickup = localPlayer.GetPickupInHand(PickupHand.Left);
            if (pickup != null) pickup.Drop();
            pickup = localPlayer.GetPickupInHand(PickupHand.Right);
            if (pickup != null) pickup.Drop();*/

            localPlayer.TeleportTo(startPosition.position, startPosition.rotation);
        }  
    }

    private void Start()
    {
        logger.LogEvent("GameManager player joint game in state " + Status.ToString());
        menuManager.UpdateMenuItemVisibility(Status, maps);
        localPlayer = Networking.LocalPlayer;
    }

    private void UpdateGameStatus(GAMESTATUS newStatus)
    {
        Status = newStatus;
        RequestSerialization();
    }

    public void PickARandomMap()
    {
        if (!Networking.IsMaster)
        {
            logger.LogError("GameManager non master tried calling PickARandomMap");
            return;
        }
        int randomNum = UnityEngine.Random.Range((int)GAMESTATUS.MAP1, (int)GAMESTATUS.NumberOfTypes);

        UpdateGameStatus((GAMESTATUS)randomNum);
    }

    public void AbortMap()
    {
        if (!Networking.IsMaster)
        {
            logger.LogError("GameManager non master tried calling AbortMap");
            return;
        }
        UpdateGameStatus(GAMESTATUS.ABORTED);
    }

    public void Update()
    {
        DoCountDown();

        CheckEndGameConditions();

        LobbyVisibility();
    }

    private void LobbyVisibility()
    {
        if (playerManager.LocalPlayerCapsule == null) return;
        if (playerManager.LocalPlayerCapsule.GetHealth() > 0)
        {
            lobby.SetActive(false);
        }
        else
        {
            lobby.SetActive(true);
            lobbySky.transform.position = localPlayer.GetPosition();
        }
    }

    private void CheckEndGameConditions()
    {
        if (!Networking.IsMaster) return;
        if (countdown > 0f) return;
        if ((int)Status >= (int)GAMESTATUS.MAP1)
        {
            if (!playerManager.AtLeastOnePlayerAlive())
            {
                UpdateGameStatus(GAMESTATUS.LOST);
            }
        }
    }

    private void DoCountDown()
    {
        if (countdown < 0f)
        {     
            return;
        }
        countdown -= Time.deltaTime;
        int countdownToSpawnIn = (int) (countdown - time_player_teleports_in);
        if (countdownToSpawnIn > 0) 
        {
            menuManager.ToggleButtonCountdown();
            menuManager.DisplayCountdown(countdownToSpawnIn.ToString());
        }
        else
        {
            HappenOnce();    
        }
    }


    private void HappenOnce()
    {
        if (resetHappenOnce)
        {
            resetHappenOnce = false;
            TelepostPlayerIn(CurrentMap.spawnPoint);
            menuManager.UpdateMenuItemVisibility(Status, maps);
            menuManager.DisplayCountdown("");
            carManager.SpawnCar();
        }
    }



    private void UpdateMap(GAMESTATUS value)
    {
        switch (value)
        {
            case GAMESTATUS.MAP1:
                {
                    CurrentMap = maps[0];
                    CurrentMapVisible();
                    break;
                }
            case GAMESTATUS.MAP2:
                {
                    CurrentMap = maps[1];
                    CurrentMapVisible();
                    break;
                }
            case GAMESTATUS.MAP3:
                {
                    CurrentMap = maps[2];
                    CurrentMapVisible();
                    break;
                }
            case GAMESTATUS.MAP4:
                {
                    CurrentMap = maps[3];
                    CurrentMapVisible();
                    break;
                }
            case GAMESTATUS.MAP5:
                {
                    CurrentMap = maps[4];
                    CurrentMapVisible();
                    break;
                }
            default:
                {
                    logger.LogError("GameManager UpdateMapVisibility... maps not in case statement");
                    break;
                }

        }
    }

    private void CurrentMapVisible()
    {
        CurrentMap.mapObject.SetActive(true);
        RenderSettings.skybox = CurrentMap.mapSky;
    }

    private void TelepostPlayerIn(Transform teleportTo)
    {
        if (Vector3.Distance(localPlayer.GetPosition(), spawnInArea.position) < spawnIndistance * 0.5f)
        {
            logger.LogEvent("GameManager TeleportPlayerIn player within distance and has playercapsule: " + playerManager.LocalPlayerCapsule.gameObject.name);
            localPlayer.TeleportTo(teleportTo.position, teleportTo.rotation);
            playerManager.LocalPlayerCapsule.DefaultHealth();
        }
    }

    public override void OnPlayerRespawn(VRCPlayerApi player)
    {
        if (player == Networking.LocalPlayer)
        {
            playerManager.LocalPlayerCapsule.SetHealth(0);
            VRC_Pickup pickup = localPlayer.GetPickupInHand(PickupHand.Left);
            if (pickup != null) pickup.Drop();
            pickup = localPlayer.GetPickupInHand(PickupHand.Right);
            if (pickup != null) pickup.Drop();
        }
    }
}
