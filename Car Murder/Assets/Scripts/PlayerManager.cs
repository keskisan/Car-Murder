
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

[UdonBehaviourSyncMode(BehaviourSyncMode.Manual)]
public class PlayerManager : UdonSharpBehaviour
{
    [SerializeField]
    GameManager gameManager;

    public Player[] players;

    private VRCPlayerApi localPlayer;
    Player _localPlayerCapsule = null;

    [UdonSynced, HideInInspector]
    public int playerHealthAtStart = 50;

    public Player LocalPlayerCapsule 
    {
        get
        {   
            return _localPlayerCapsule;
        }         
    }

    public void ChangeHealthAtStartM(int value)
    {
        if (Networking.IsMaster)
        {
            playerHealthAtStart = value;
            RequestSerialization();
        }
        
    }

    private void Start()
    {
        localPlayer = Networking.LocalPlayer;

        AssignePlayersACapsule();
    }

    private void AssignePlayersACapsule()
    { 
        if (Networking.IsMaster)
        {
            _localPlayerCapsule = players[0];
            gameManager.logger.LogEvent("Master has " + players[0].gameObject.name);
            return;
        }
        else
        {
            for (int i = 1; i < players.Length; i++) //all capsules except 0 that belongs to master that is unused
            {
                if (Networking.GetOwner(players[i].gameObject).isMaster)
                {
                    _localPlayerCapsule = players[i];
                    Networking.SetOwner(localPlayer, players[i].gameObject);
                    gameManager.logger.LogEvent("PlayerManager Transfering player capsule " + players[i].gameObject.name);
                    return;
                }
            }
        }
    }

    public bool AtLeastOnePlayerAlive()
    {
        if (Networking.GetOwner(players[0].gameObject).isMaster)
        {
            if (players[0].IsPlayerInGame())
            {
                return true;
            }
        }
        for (int i = 1; i<players.Length; i++)
        {
            if (!Networking.GetOwner(players[i].gameObject).isMaster)
            {
                if (players[i].IsPlayerInGame())
                {
                    return true;
                }
            }
        }
        return false;
    }

    public Player GetRandomPlayer()
    {
        //int randomInt = Random.Range(0, players.Length);
        //return (players[randomInt]);


        int[] tempArray = new int[players.Length]; //creates temporary array and shuffle it
        for (int i = 0; i < tempArray.Length; i++)
        {
            tempArray[i] = i;
        }


        for (int i = 0; i < tempArray.Length - 1; i++)
        {
            int j = Random.Range(i, tempArray.Length);
            int temp = tempArray[i];
            tempArray[i] = tempArray[j];
            tempArray[j] = temp;
        }

        for (int i = 0; i < tempArray.Length; i++)
        {
            if (IsPlayerValidAndIngame(players[i]))
            {
                return players[i];
            }  
        }

        return players[0]; //nothing found master default
    }

    public override void OnPlayerLeft(VRCPlayerApi player) //master always has capsule 0 so new masters loose thier capsule
    {
        gameManager.logger.LogEvent("PlayerManager OnPlayerLeft a player left");
        if (localPlayer.isMaster)
        {
            if (_localPlayerCapsule != players[0])
            {
                gameManager.logger.LogEvent("PlayerManager OnPlayerLeft master left new master transferring capsule");
                int health = _localPlayerCapsule.GetHealth();
                _localPlayerCapsule.SetOwnerPlayer();
                _localPlayerCapsule = players[0];
                _localPlayerCapsule.SetHealth(health);
            }
        }
    }

    public bool IsPlayerValidAndIngame(Player player)
    {
        if (player == players[0]) //true
        {
            if (player.IsPlayerInGame())
            {
                return true;
            } 
            else
            {
                return false;
            }
        }
        if (Networking.GetOwner(player.gameObject).isMaster)
        {
            return false;
        }
        else
        {
            if (player.IsPlayerInGame())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }


}
