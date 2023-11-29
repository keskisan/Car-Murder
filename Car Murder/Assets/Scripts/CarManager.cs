
using Newtonsoft.Json.Linq;
using System;
using UdonSharp;
using UnityEngine;
using VRC.Core;
using VRC.SDKBase;
using VRC.Udon;



[UdonBehaviourSyncMode(BehaviourSyncMode.Manual)]
public class CarManager : UdonSharpBehaviour
{
    [SerializeField]
    GameManager gameManager;

    [SerializeField]
    CarDriveAI[] cars;

    [SerializeField]
    Transform spawnOutPosition;

    float countdowntoEnemiesActive = -2f;

    [HideInInspector, UdonSynced]
    public bool CarsAreActive = false;

    [UdonSynced, FieldChangeCallback(nameof(CurrentCar))]
    private int _currentCar;

    [UdonSynced]
    public int carAmount;

    public int CurrentCar
    {
        set
        {
            if (Networking.IsOwner(cars[value].gameObject))
            {
                cars[value].objectSync.FlagDiscontinuity();
                cars[value].transform.position = gameManager.CurrentMap.carSpawnPoint.position;
                cars[value].transform.rotation = gameManager.CurrentMap.carSpawnPoint.rotation;
                cars[value].CarHealth = cars[value].carhealthDefault;

                cars[value].GetComponent<Rigidbody>().isKinematic = false;
            }


            _currentCar = value;
        }
        get => _currentCar;
    }

    [UdonSynced, FieldChangeCallback(nameof(CurrentCar1))]
    private int _currentCar1;
    public int CurrentCar1
    {
        set
        {
            if (Networking.IsOwner(cars[value].gameObject))
            {
                cars[value].objectSync.FlagDiscontinuity();
                cars[value].transform.position = gameManager.CurrentMap.carSpawnPoint1.position;
                cars[value].transform.rotation = gameManager.CurrentMap.carSpawnPoint1.rotation;
                cars[value].CarHealth = cars[value].carhealthDefault;
                cars[value].GetComponent<Rigidbody>().isKinematic = false;
            }


            _currentCar1 = value;
        }
        get => _currentCar1;
    }

    [UdonSynced, FieldChangeCallback(nameof(CurrentCar2))]
    private int _currentCar2;
    public int CurrentCar2
    {
        set
        {
            if (Networking.IsOwner(cars[value].gameObject))
            {
                cars[value].objectSync.FlagDiscontinuity();
                cars[value].transform.position = gameManager.CurrentMap.carSpawnPoint2.position;
                cars[value].transform.rotation = gameManager.CurrentMap.carSpawnPoint2.rotation;
                cars[value].CarHealth = cars[value].carhealthDefault;

                cars[value].GetComponent<Rigidbody>().isKinematic = false;
            }

            _currentCar2 = value;
        }
        get => _currentCar2;
    }

   

    void Update()
    {
        gameManager.onScreenStats.SetCarHealthDisplay(
            cars[CurrentCar].CarHealth, 
            carAmount >= 1 ? cars[CurrentCar1].CarHealth : 0,
            carAmount >= 2 ? cars[CurrentCar2].CarHealth : 0
        );
        SetCarsActive(); 
    }

    public void DCN()
    {
        gameManager.onScreenStats.SetCarNamesDisplay(
            cars[CurrentCar].carName,
            carAmount >= 1 ? cars[CurrentCar1].carName : "",
            carAmount >= 2 ? cars[CurrentCar2].carName : ""
        );
    }

    private void SetCarsActive()
    {
        if (countdowntoEnemiesActive > 0f)
        {
            countdowntoEnemiesActive -= Time.deltaTime;
        } else if (countdowntoEnemiesActive > -1f)
        {
            countdowntoEnemiesActive = -2f;
            CarsAreActive = true;
            SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "DCN");
            RequestSerialization();

        }
    }

    public void DGO() //master only
    {
        if (cars[CurrentCar].CarHealth > 0) return;
        if (cars[CurrentCar1].CarHealth > 0) return;
        if (cars[CurrentCar2].CarHealth > 0) return;
        if (Networking.IsMaster)
        {
            CarsAreActive = false;
            gameManager.GameWon();
            RequestSerialization();
        }

        if (Networking.IsOwner(cars[CurrentCar].gameObject))
        {
            cars[CurrentCar].objectSync.FlagDiscontinuity();
            cars[CurrentCar].transform.position = spawnOutPosition.position;
            cars[CurrentCar].CarHealth = 0;
            cars[CurrentCar].GetComponent<Rigidbody>().isKinematic = true;
        }
        if (Networking.IsOwner(cars[CurrentCar1].gameObject))
        {
            cars[CurrentCar1].objectSync.FlagDiscontinuity();
            cars[CurrentCar1].transform.position = spawnOutPosition.position;
            cars[CurrentCar1].CarHealth = 0;
            cars[CurrentCar1].GetComponent<Rigidbody>().isKinematic = true;
        }
        if (Networking.IsOwner(cars[CurrentCar2].gameObject))
        {
            cars[CurrentCar2].objectSync.FlagDiscontinuity();
            cars[CurrentCar2].transform.position = spawnOutPosition.position;
            cars[CurrentCar2].CarHealth = 0;
            cars[CurrentCar2].GetComponent<Rigidbody>().isKinematic = true;
        }
        gameManager.onScreenStats.SetCarNamesDisplay("", "", "");
    }

    public void CheckIfCarsDeadGameOver()
    {
        SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "DGO");
    }

    public void SpawnCar()
    {
        if (Networking.IsMaster) //owner this gameobject
        {
            gameManager.logger.LogEvent("Spawning cars");
            countdowntoEnemiesActive = 4f;

            int tmp; //dont assign directly as assign causes spawn in
            int tmp1;
            int tmp2;
            do
            {
                tmp = UnityEngine.Random.Range(0, cars.Length);
                tmp1 = UnityEngine.Random.Range(0, cars.Length);
                tmp2 = UnityEngine.Random.Range(0, cars.Length);
            } while (tmp == tmp1 || tmp1 == tmp2 || tmp == tmp2 || tmp == CurrentCar || tmp1 == CurrentCar1 || tmp2 == CurrentCar2);

            if (carAmount == 0) //if new number same as an old one menu shows wrong values
            {
                CurrentCar = tmp;
            }
            else if (carAmount == 1)
            {
                CurrentCar = tmp;
                CurrentCar1 = tmp1;
            }
            else
            {
                CurrentCar = tmp;
                CurrentCar1 = tmp1;
                CurrentCar2 = tmp2;
            }
        }
    }

    public void SPO()
    {
        if (Networking.IsMaster) //owner this gameobject
        {
            CarsAreActive = false;
            RequestSerialization();
        }

        if (Networking.IsOwner(cars[CurrentCar].gameObject))
        {
            cars[CurrentCar].objectSync.FlagDiscontinuity();
            cars[CurrentCar].transform.position = spawnOutPosition.position;
            cars[CurrentCar].CarHealth = 0;
            cars[CurrentCar].GetComponent<Rigidbody>().isKinematic = true;
        }

        if (Networking.IsOwner(cars[CurrentCar1].gameObject))
        {
            cars[CurrentCar1].objectSync.FlagDiscontinuity();
            cars[CurrentCar1].transform.position = spawnOutPosition.position;
            cars[CurrentCar1].CarHealth = 0;
            cars[CurrentCar1].GetComponent<Rigidbody>().isKinematic = true;

        }

        if (Networking.IsOwner(cars[CurrentCar2].gameObject))
        {
            cars[CurrentCar2].objectSync.FlagDiscontinuity();
            cars[CurrentCar2].transform.position = spawnOutPosition.position;
            cars[CurrentCar2].CarHealth = 0;
            cars[CurrentCar2].GetComponent<Rigidbody>().isKinematic = true;
        }

        gameManager.onScreenStats.SetCarNamesDisplay("", "", "");
    }

    public void SpawnCarsOut()
    {
        SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "SPO");
    }

    public void SetCarAmount(int value)
    {
        if (Networking.IsMaster)
        {
            carAmount = value;
            RequestSerialization();
        }  
    }
}
