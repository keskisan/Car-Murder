
using TMPro;
using UdonSharp;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;
using VRC.Core;
using VRC.SDKBase;
using VRC.Udon;

[UdonBehaviourSyncMode(BehaviourSyncMode.Manual)]
public class MenuManager : UdonSharpBehaviour
{
    [SerializeField]
    GameManager gameManager;

    [SerializeField]
    TextMeshProUGUI textCountdown, textOutcome, textMasterName, textGameStatus, textButtonMasterOrPlayers, textCarsValue, textVolumeValue, textHealthValue, trackName;

    [SerializeField]
    UnityEngine.UI.Button buttonStart, buttonAbort;

    [SerializeField]
    Slider sliderVolume, sliderCarAmount, sliderHealthAmount;

    [UdonSynced, FieldChangeCallback(nameof(MasterOnlyBool))]
    private bool _masterOnlyBool;
    public bool MasterOnlyBool
    {
        set
        {
            gameManager.logger.LogEvent("MenumanagerMasterOnlyBool changed to" + value);
            if (value)
            {
                textButtonMasterOrPlayers.text = "Everyone";
            } else
            {
                textButtonMasterOrPlayers.text = "Master Only";
            }

            _masterOnlyBool = value;
        }
        get => _masterOnlyBool;
    }

    private void Start()
    {
        textMasterName.text = Networking.GetOwner(gameObject).displayName;
    }

    private void ToggleButtonVisibilityInGame()
    {
        buttonAbort.gameObject.SetActive(true);
        buttonStart.gameObject.SetActive(false);
        sliderCarAmount.gameObject.SetActive(false);
        sliderHealthAmount.gameObject.SetActive(false);
    }

    private void ToggleButtonVisibilityOutGame()
    {
        buttonAbort.gameObject.SetActive(false);
        buttonStart.gameObject.SetActive(true);
        sliderCarAmount.gameObject.SetActive(true);
        sliderHealthAmount.gameObject.SetActive(true);
    }

    public void ToggleButtonCountdown()
    {
        buttonAbort.gameObject.SetActive(false);
        buttonStart.gameObject.SetActive(false);
        sliderCarAmount.gameObject.SetActive(false);
        sliderHealthAmount.gameObject.SetActive(false);
    }

    public void STR() //start master only networked
    {
        if (gameManager.Status == GAMESTATUS.LOST || gameManager.Status == GAMESTATUS.WON || gameManager.Status == GAMESTATUS.ABORTED || gameManager.Status == GAMESTATUS.NOTPLAYING)
        {
            gameManager.PickARandomMap();
        }
    }

    public void StartButton() //master or everyone master networked
    {
        if (MasterOnlyBool && Networking.IsMaster || !MasterOnlyBool)
        {
            SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.Owner, "STR");
        }
    }


    public void ABT() //start master only networked
    {
        if ((int) gameManager.Status >= (int) GAMESTATUS.MAP1)
        {
            gameManager.AbortMap();
        }
    }

    public void AbortButton() //master or everyone master networked
    {
        if (MasterOnlyBool && Networking.IsMaster || !MasterOnlyBool)
        {
            SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.Owner, "ABT");
        }
    }

    public void MasterOnlyButton() //master or everyone master networked
    {
        if (Networking.IsMaster)
        {
            MasterOnlyBool = !MasterOnlyBool;
            RequestSerialization();
        }
    }

    public void DisplayCountdown(string value)
    {
        textCountdown.text = value;
    }

    public void UpdateMenuItemVisibility(GAMESTATUS status, Map[] maps)
    {
        switch (status)
        {
            case GAMESTATUS.NOTPLAYING:
                {

                    ToggleButtonVisibilityOutGame();
                    textGameStatus.text = "Not Playing";
                    textOutcome.text = "Not Playing";
                    break;
                }
            case GAMESTATUS.ABORTED:
                {
                    ToggleButtonVisibilityOutGame();
                    textGameStatus.text = "Not Playing";
                    textOutcome.text = "Aborted";
                    break;
                }
            case GAMESTATUS.LOST:
                {
                    ToggleButtonVisibilityOutGame();
                    textGameStatus.text = "Not Playing";
                    textOutcome.text = "Lost";
                    break;
                }
            case GAMESTATUS.WON:
                {
                    ToggleButtonVisibilityOutGame();
                    textGameStatus.text = "Not Playing";
                    textOutcome.text = "Won";
                    break;
                }
            case GAMESTATUS.MAP1:
                {
                    ToggleButtonVisibilityInGame();
                    textGameStatus.text = "playing " + maps[0].mapName;
                    textOutcome.text = "";
                    break;
                }
            case GAMESTATUS.MAP2:
                {
                    ToggleButtonVisibilityInGame();
                    textGameStatus.text = "playing " + maps[1].mapName; ;
                    textOutcome.text = "";
                    break;
                }
            case GAMESTATUS.MAP3:
                {
                    ToggleButtonVisibilityInGame();
                    textGameStatus.text = "playing " + maps[2].mapName;
                    textOutcome.text = "";
                    break;
                }
            case GAMESTATUS.MAP4:
                {
                    ToggleButtonVisibilityInGame();
                    textGameStatus.text = "playing " + maps[3].mapName; ;
                    textOutcome.text = "";
                    break;
                }
            case GAMESTATUS.MAP5:
                {
                    ToggleButtonVisibilityInGame();
                    textGameStatus.text = "playing " + maps[4].mapName; ;
                    textOutcome.text = "";
                    break;
                }
            default:
                {
                    gameManager.logger.LogError("Menumanager UpdateMenuItem... invalid gamestate");
                    break;
                }
        }
    }

    public void CA0()
    {
        //sliderCarAmount.value = 0f; //everytime slider changes it recalls function
        textCarsValue.text = "1";
        gameManager.carManager.SetCarAmount(0);
    }

    public void CA1()
    {
        textCarsValue.text = "2";
        gameManager.carManager.SetCarAmount(1);
    }

    public void CA2()
    {
        textCarsValue.text = "3";
        gameManager.carManager.SetCarAmount(2);
    }

    public void CarsAmountUpdate()
    {
        if (MasterOnlyBool && Networking.IsMaster || !MasterOnlyBool)
        {
            if ((int)sliderCarAmount.value == 0) SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "CA0");
            if ((int)sliderCarAmount.value == 1) SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "CA1");
            if ((int)sliderCarAmount.value == 2) SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "CA2");
        }  
    }

    public void H05()
    {
        textHealthValue.text = "50";
        gameManager.playerManager.ChangeHealthAtStartM(50);
    }

    public void H10()
    {
        textHealthValue.text = "100";
        gameManager.playerManager.ChangeHealthAtStartM(100);
    }

    public void H15()
    {
        textHealthValue.text = "150";
        gameManager.playerManager.ChangeHealthAtStartM(150);
    }

    public void H20()
    {
        textHealthValue.text = "200";
        gameManager.playerManager.ChangeHealthAtStartM(200);
    }

    public void H25()
    {
        textHealthValue.text = "250";
        gameManager.playerManager.ChangeHealthAtStartM(250);
    }

    public void PlayerHealthAmountUpdate()
    {
        if (MasterOnlyBool && Networking.IsMaster || !MasterOnlyBool)
        {
            if ((int)sliderHealthAmount.value == 0) SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "H05");
            if ((int)sliderHealthAmount.value == 1) SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "H10");
            if ((int)sliderHealthAmount.value == 2) SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "H15");
            if ((int)sliderHealthAmount.value == 3) SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "H20");
            if ((int)sliderHealthAmount.value == 4) SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "H25");
        }
    }

    public void MusicVolumeUpdate()
    {
        gameManager.musicController.changeVolume(sliderVolume.value);
        textVolumeValue.text = sliderVolume.value.ToString("#.##");
    }

    public void UpdateTrackName(string name)
    {
        trackName.text = name;
    }
}
