
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

[UdonBehaviourSyncMode(BehaviourSyncMode.None)]
public class Map : UdonSharpBehaviour
{

    public Transform spawnPoint;
    public GameObject mapObject; 
    public string mapName;

    public Transform carSpawnPoint, carSpawnPoint1, carSpawnPoint2;
    public Transform[] spawnPointParts;
    public LocationMarker[] locationMarkers;

    public Material mapSky;

    void Start()
    {
        mapObject.SetActive(false);
    }
}
