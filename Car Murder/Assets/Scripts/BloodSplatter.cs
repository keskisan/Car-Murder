
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using System;
using System.Runtime.ConstrainedExecution;

[UdonBehaviourSyncMode(BehaviourSyncMode.Manual)]
public class BloodSplatter : UdonSharpBehaviour
{
    [SerializeField]
    GameManager gameManager;

    [SerializeField]
    Player ownerPlayer;

    bool hitbyCar = false;
    bool firstHitByCar = false;
    float delayTime = 0f;
    float y;

    Vector3[] newVertices;
    int VerticeCounter = 0;
    Vector2[] newUV;
    int[] newTriangles;
    int TriangleCounter = 0;

    [SerializeField]
    LayerMask layerMaskEverything;

    Mesh mesh;

    Vector3 downCastPosition;

    [SerializeField]
    GameObject partilesSplat;

    RaycastHit hit;

    private CarRigidBody carHit;

    Vector3 lastposition;

    void Start()
    {
        lastposition = Vector3.zero;
        mesh = gameObject.GetComponent<MeshFilter>().mesh;
        ResetObjectToDefaults();
    }

    public void ResetObjectToDefaults()
    {
        hitbyCar = false;
        firstHitByCar = false;
        delayTime = 0f;

        VerticeCounter = 0;
        TriangleCounter = 0;

        downCastPosition = new Vector3(0f, 1f, 0f);
        mesh.Clear();

        newVertices = new Vector3[2400];
        newUV = new Vector2[2400];
        newTriangles = new int[800];
        RequestSerialization();
    }

    private void Update()
    {
        HitByCarUpdate();
    }

    private void HitByCarUpdate()
    {
        if (!hitbyCar)
        {
            partilesSplat.SetActive(false);
            return;
        }

        if (firstHitByCar)
        {
            firstHitByCar = false;
            StartMesh();    
        }

        delayTime += Time.deltaTime;
        partilesSplat.SetActive(true);
        partilesSplat.transform.position = (Networking.GetOwner(gameObject).GetPosition() + Networking.GetOwner(gameObject).GetTrackingData(VRCPlayerApi.TrackingDataType.Head).position) * 0.5f;

        if (delayTime > 0.4f)
        {
            delayTime = 0f;
            hitbyCar = false;

            FinishMesh();
        }
    }


    private void StartMesh()
    {
        if (Physics.Raycast(Networking.GetOwner(ownerPlayer.gameObject).GetPosition() + downCastPosition, Vector3.down, out hit, Mathf.Infinity, layerMaskEverything))
        {
            lastposition = hit.point;   
        }

        y = (transform.eulerAngles.y + 90f) * Mathf.Deg2Rad;
        newVertices[VerticeCounter] = new Vector3(lastposition.x + Mathf.Sin(y), lastposition.y + 0.01f, lastposition.z + Mathf.Cos(y)); //1
        newUV[VerticeCounter] = new Vector2(1f, 0f);
        VerticeCounter++;
        newVertices[VerticeCounter] = new Vector3(lastposition.x - Mathf.Sin(y), lastposition.y + 0.01f, lastposition.z - Mathf.Cos(y)); //2
        newUV[VerticeCounter] = new Vector2(0f, 0f);
        VerticeCounter++;
    }
    private void FinishMesh()
    {
        if (Physics.Raycast(Networking.GetOwner(ownerPlayer.gameObject).GetPosition() + downCastPosition, Vector3.down, out hit, Mathf.Infinity, layerMaskEverything))
        {
            lastposition = hit.point; 
        }

        y = (transform.eulerAngles.y + 90f) * Mathf.Deg2Rad;
        newVertices[VerticeCounter] = new Vector3(lastposition.x + Mathf.Sin(y), lastposition.y + 0.01f, lastposition.z + Mathf.Cos(y)); //3
        newUV[VerticeCounter] = new Vector2(1f, 0.5f);
        VerticeCounter++;
        newVertices[VerticeCounter] = new Vector3(lastposition.x - Mathf.Sin(y), lastposition.y + 0.01f, lastposition.z - Mathf.Cos(y)); //4
        newUV[VerticeCounter] = new Vector2(0f, 0.5f);
        VerticeCounter++;
        newTriangles[TriangleCounter] = VerticeCounter - 4; //1
        TriangleCounter++;
        newTriangles[TriangleCounter] = VerticeCounter - 2; //2
        TriangleCounter++;
        newTriangles[TriangleCounter] = VerticeCounter - 3; //3
        TriangleCounter++;
        newTriangles[TriangleCounter] = VerticeCounter - 3; //3
        TriangleCounter++;
        newTriangles[TriangleCounter] = VerticeCounter - 2; //2
        TriangleCounter++;
        newTriangles[TriangleCounter] = VerticeCounter - 1; //4
        TriangleCounter++;


        newVertices[VerticeCounter] = new Vector3(hit.point.x + 2f, hit.point.y + 0.01f, hit.point.z - 2f); //1
        newUV[VerticeCounter] = new Vector2(1f, 0f);
        VerticeCounter++;
        newVertices[VerticeCounter] = new Vector3(hit.point.x + 2f, hit.point.y + 0.01f, hit.point.z + 2f); //2
        newUV[VerticeCounter] = new Vector2(1f, 1f);
        VerticeCounter++;
        newVertices[VerticeCounter] = new Vector3(hit.point.x - 2f, hit.point.y + 0.01f, hit.point.z - 2f); //3
        newUV[VerticeCounter] = new Vector2(0f, 0f);
        VerticeCounter++;
        newVertices[VerticeCounter] = new Vector3(hit.point.x - 2f, hit.point.y + 0.01f, hit.point.z + 2f); //4
        newUV[VerticeCounter] = new Vector2(0f, 1f);
        VerticeCounter++;
        newTriangles[TriangleCounter] = VerticeCounter - 3; //2
        TriangleCounter++;
        newTriangles[TriangleCounter] = VerticeCounter - 4; //1
        TriangleCounter++;
        newTriangles[TriangleCounter] = VerticeCounter - 2; //3
        TriangleCounter++;
        newTriangles[TriangleCounter] = VerticeCounter - 3; //2
        TriangleCounter++;
        newTriangles[TriangleCounter] = VerticeCounter - 2; //3
        TriangleCounter++;
        newTriangles[TriangleCounter] = VerticeCounter - 1; //4
        TriangleCounter++;

        DrawMesh();
    }

    private void DrawMesh()
    {
        Vector3[] tmpVertices = new Vector3[VerticeCounter];
        Array.Copy(newVertices, 0, tmpVertices, 0, VerticeCounter);
        Vector2[] tmpUVS = new Vector2[VerticeCounter];
        Array.Copy(newUV, 0, tmpUVS, 0, VerticeCounter);
        int[] tmpTriangles = new int[TriangleCounter];
        Array.Copy(newTriangles, 0, tmpTriangles, 0, TriangleCounter);
        mesh.vertices = tmpVertices;
        mesh.uv = tmpUVS;
        mesh.triangles = tmpTriangles;
    }

    public void HBC() //when ever player gets hit locally it shows up for everyone not when they seeming get hit in other instances nothing else here synced
    {
        firstHitByCar = true;
        hitbyCar = true;
    }

    public void HitByCar(CarRigidBody car)
    {
        carHit = car;
        carHit.PlaySplatSound();
        carHit.PlayHonkSoundDelayed(0.4f);
        SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "HBC");
    }
}
