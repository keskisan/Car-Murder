
using TMPro;
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

[UdonBehaviourSyncMode(BehaviourSyncMode.None)]
public class FindPath : UdonSharpBehaviour
{
    [SerializeField]
    GameManager gameManager;

    RaycastHit hit;

    [SerializeField]
    LayerMask layerMask;


    float bestLocationDistance;

    LocationMarker startLoc;
    LocationMarker endLoc;

    bool pathfound = false;
    bool noPathExist = false;


    public LocationMarker FindLocationMarker(Vector3 Position, LocationMarker[] locationMarkers)  //Find valid marker closest to position >
    {
        LocationMarker bestLocation;

        bestLocation = null;

        for (int i = 0; i < locationMarkers.Length; i++)
        {
            Vector3 toPosition = locationMarkers[i].transform.position;
            Vector3 direction = toPosition - Position;
            float distance = Vector3.Distance(Position, toPosition);

            if (!Physics.Raycast(Position, direction, out hit, distance, layerMask))
            {
                if (bestLocation == null)
                {
                    bestLocation = locationMarkers[i];
                    bestLocationDistance = distance;
                }
                else if (distance < bestLocationDistance)
                {
                    bestLocation = locationMarkers[i];
                    bestLocationDistance = distance;
                }
            }
        }
        return bestLocation;
    }

    bool FillLocationDetails(LocationMarker previousLocation, LocationMarker checkLocation, float distance) //calculate distancesc >
    {
        if (checkLocation == null) return false;
        if (previousLocation == null)
        {
            checkLocation.distanceToHere = 0f;
        }
        else
        {
            checkLocation.distanceToHere = previousLocation.distanceToHere + distance;
        }

        if (endLoc == null)
        {
            checkLocation.shortestDistanceToTarget = Vector3.Distance(checkLocation.transform.position, Vector3.zero); //stops bug of script crashing?
        }
        else
        {
            checkLocation.shortestDistanceToTarget = Vector3.Distance(checkLocation.transform.position, endLoc.transform.position);
        }
        checkLocation.toatalDisctanceCost = checkLocation.distanceToHere + checkLocation.shortestDistanceToTarget;
        checkLocation.parentLocation = previousLocation;
        checkLocation.hasbeenFilled = true;
        checkLocation.Checkeing = true;
        return true;
    }

    bool CheckLocationsLocations(LocationMarker location) //calculate distances for all visible locations seem>
    {

        for (int i = 0; i < location.locationMarkersVisible.Length; i++)
        {
            if (!location.locationMarkersVisible[i].hasbeenChecked)
            {
                if (!location.locationMarkersVisible[i].hasbeenFilled) //stops refilling with incorrect info (parent)
                {
                    if (!FillLocationDetails(location, location.locationMarkersVisible[i], location.distanceToHere)) //last one will also get info filled.
                    {
                        return false;
                    }
                }
            }
            if (location.locationMarkersVisible[i] == endLoc)
            {
                pathfound = true;
            }
        }
        location.hasbeenChecked = true;
        location.Checkeing = false;
        return true;
    }

    float shortestDistanceCost;
    LocationMarker nextBestLocation;

    bool FindNextLocationToCheck(LocationMarker[] locationMarkers) //find location with shortest distance values to investigate >
    {
        shortestDistanceCost = Mathf.Infinity;
        for (int i = 0; i < locationMarkers.Length; i++)
        {
            if (locationMarkers[i].Checkeing)
            {
                if (locationMarkers[i].toatalDisctanceCost < shortestDistanceCost)
                {
                    shortestDistanceCost = locationMarkers[i].toatalDisctanceCost;
                    nextBestLocation = locationMarkers[i];
                }
            }
        }

        if (nextBestLocation == null) //no path found
        {
            noPathExist = true;
        }
        else
        {
            if (!CheckLocationsLocations(nextBestLocation))
            {
                return false;
            }
        }
        return true;
    }

    public LocationMarker[] FindPathToTarget(Vector3 startPosition, Vector3 targetPosition, LocationMarker[] locationMarkers) //main function >
    {
        LocationMarker[] locationPath = new LocationMarker[locationMarkers.Length];
        ResetAll(locationMarkers);

        endLoc = FindLocationMarker(targetPosition, locationMarkers);
        startLoc = FindLocationMarker(startPosition, locationMarkers);
        FillLocationDetails(null, startLoc, 0f);

        int i = 0; //Avoids infinate loop with bugs

        while (i < 80 && !noPathExist && !pathfound)
        {
            if (!FindNextLocationToCheck(locationMarkers))
            {
                return null; //no path found
            }
            i++;
            /*if (i == 79)
            {
                gameManager.logger.LogError("Findpath FindPathToTarget while loop failed");
            }*/
        }

        if (pathfound) //Trace back to make an array of this path
        {
            if (startLoc == null) return null;
            if (endLoc == null) return null;

            LocationMarker currentTraceBackPath = endLoc;

            int pathLength = 0;

            while (currentTraceBackPath != startLoc)
            {
                locationPath[pathLength] = currentTraceBackPath;
                currentTraceBackPath = currentTraceBackPath.parentLocation;
                pathLength++;
            }
            locationPath[pathLength] = currentTraceBackPath;
            return locationPath;
        }
        else
        {
            return null;
        }
    }

    void ResetAll(LocationMarker[] locationMarkers) //reset values
    {
        pathfound = false;
        noPathExist = false;
        shortestDistanceCost = Mathf.Infinity;
        for (int i = 0; i < locationMarkers.Length; i++)
        {
            locationMarkers[i].hasbeenChecked = false;
            locationMarkers[i].Checkeing = false;
            locationMarkers[i].hasbeenFilled = false;
        }
    }

    /// <summary>
    /// debugging stuff
    /// </summary>

    [SerializeField]
    Transform testTransform, targetTransform;

    [SerializeField]
    LocationMarker[] locationMarkers;

    private void Update() //debugging functionality
    {
        //ShowAllLinks();
        //ShowFindPath();
    }

    private void ShowAllLinks()
    {
        for (int i = 0; i < locationMarkers.Length; i++)
        {
            for (int n = 0; n < locationMarkers[i].locationMarkersVisible.Length; n++)
            {
                Debug.DrawLine(locationMarkers[i].transform.position, locationMarkers[i].locationMarkersVisible[n].transform.position, Color.green);
            }
        }
    }

    private void ShowFindPath()
    {
        LocationMarker[] testPath = FindPathToTarget(testTransform.position, targetTransform.position, locationMarkers);

        if (testPath == null) return;
        for (int i = 1; i < testPath.Length; i++)
        {
            if (testPath[i] != null)
            {
                Debug.DrawLine(testPath[i - 1].transform.position, testPath[i].transform.position, Color.red);
            }
        }
    }
}
