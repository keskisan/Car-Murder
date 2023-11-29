
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class BaseWeapons : UdonSharpBehaviour
{
    [SerializeField]
    public GameManager gameManager;

    [UdonSynced, HideInInspector]
    public bool ObjectInGame;

    [UdonSynced, HideInInspector]
    public bool objectActive;

    public virtual void SpawnWeaponsIn()
    {
       
    }

    public virtual void SpawnWeaponsOut()
    {

    }

    public virtual void BroughtTogether()
    {
        gameManager.logger.LogError("BaseWeapons this code should never run");
    }
}
