
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

[UdonBehaviourSyncMode(BehaviourSyncMode.None)]
public class WeaponsManager : UdonSharpBehaviour
{
    [SerializeField]
    GameManager gameManager;

    [SerializeField]
    BaseWeapons[] baseWeapons;

    public BaseWeapons[] clocks;

    public BaseWeapons[] wires;

    [SerializeField]
    float timeToWeaponSpawnInMax = 40f;

    float spawnInTimer;
    float timeToNextSpawnIn = 1f;

    private void Update()
    {
        if (gameManager.IsGamePlaying())
        {
            spawnInTimer += Time.deltaTime;

            if (spawnInTimer > timeToNextSpawnIn)
            {
                timeToNextSpawnIn = Random.Range(0f, timeToWeaponSpawnInMax);
                spawnInTimer = 0f;
                SpawnWeaponInEverySoOften();
            }


        }
    }

    private void SpawnWeaponInEverySoOften() //spawn in every so often
    {
        int i = Random.Range(0, baseWeapons.Length);
        if (!baseWeapons[i].ObjectInGame) 
        {
            baseWeapons[i].SpawnWeaponsIn();
        }
    }

    public void SpawnWeaponsOut() //spawn everything out
    {
        for (int i = 0; i < baseWeapons.Length; i++)
        {
            baseWeapons[i].SpawnWeaponsOut();
        }
    }
}
