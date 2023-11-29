
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class LocationMarker : UdonSharpBehaviour
{
    public LocationMarker[] locationMarkersVisible; //in inspector
    [HideInInspector]
    public float[] locationCost; //here

    [HideInInspector]
    public float distanceToHere;
    [HideInInspector]
    public float shortestDistanceToTarget;
    [HideInInspector]
    public float toatalDisctanceCost;

    [HideInInspector]
    public LocationMarker parentLocation;

    [HideInInspector]
    public bool hasbeenFilled;
    [HideInInspector]
    public bool hasbeenChecked;
    [HideInInspector]
    public bool Checkeing;

    private MeshRenderer mesh;

    private void Start()
    {

        locationCost = new float[locationMarkersVisible.Length];

        for (int i = 0; i < locationMarkersVisible.Length; i++) //cache the distances
        {
            locationCost[i] = Vector3.Distance(transform.position, locationMarkersVisible[i].transform.position);
        }

        mesh = GetComponent<MeshRenderer>();
        mesh.enabled = false;
    }
}
