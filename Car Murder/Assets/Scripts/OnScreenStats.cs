
using TMPro;
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

[UdonBehaviourSyncMode(BehaviourSyncMode.None)]
public class OnScreenStats : UdonSharpBehaviour
{
    [SerializeField]
    TextMeshProUGUI textHealth, textCarHealth, textCarHealth1, textCarHealth2, textCountdownTimer, textHealthInGame, textCarHealthInGame, 
        textCarHealthInGame1, textCarHealthInGame2, textCountdownTimerInGame, textCarTag1, textCarTag2, textCarTag3, textCarTag1InGame, textCarTag2InGame, textCarTag3InGame;

    [SerializeField]
    GameObject healthTagText, carHealthTagText, carHealthTagText1, carHealthTagText2;

    [SerializeField]
    Transform inGameMenu;

    VRCPlayerApi localPlayer;

    public void SetHealthDisplay(int healthAmount)
    {
        if (healthAmount == 0)
        {
            textHealth.text = textHealthInGame.text = "";
        }
        else
        {
            textHealth.text = textHealthInGame.text = healthAmount.ToString();
        }
       
    }

    public void SetCarHealthDisplay(int healthAmount, int healthAmount1, int healthAmount2)
    {
        if (healthAmount == 0)
        {
            textHealth.text = textHealthInGame.text = "";
        }
        else
        {
            textCarHealth.text = textCarHealthInGame.text = healthAmount.ToString();
        }

        if (healthAmount1 == 0)
        {
            textCarHealth1.text = textCarHealthInGame1.text = "";
        }
        else
        {
            textCarHealth1.text = textCarHealthInGame1.text = healthAmount1.ToString();
        }

        if (healthAmount2 == 0)
        {
            textCarHealth2.text = textCarHealthInGame2.text = "";
        }
        else
        {
            textCarHealth2.text = textCarHealthInGame2.text = healthAmount2.ToString();
        }  
    }

    public void SetCarNamesDisplay(string carName1, string carName2, string carName3)
    {
        textCarTag1.text = textCarTag1InGame.text = carName1;
        textCarTag2.text = textCarTag2InGame.text = carName2;
        textCarTag3.text = textCarTag3InGame.text = carName3;
    }

    private void Start()
    {
        localPlayer = Networking.LocalPlayer;
        textCountdownTimer.text = textCountdownTimerInGame.text = "";

        if (localPlayer.IsUserInVR())
        {
            inGameMenu.gameObject.SetActive(true);

            healthTagText.gameObject.SetActive(false);
            carHealthTagText.gameObject.SetActive(false);
            carHealthTagText1.gameObject.SetActive(false);
            carHealthTagText2.gameObject.SetActive(false);
            textHealth.gameObject.SetActive(false);
            textCarHealth.gameObject.SetActive(false);
            textCarHealth1.gameObject.SetActive(false);
            textCarHealth2.gameObject.SetActive(false);
        }
        else
        {
            inGameMenu.gameObject.SetActive(false);

            healthTagText.gameObject.SetActive(true);
            carHealthTagText.gameObject.SetActive(true);
            carHealthTagText1.gameObject.SetActive(true);
            carHealthTagText2.gameObject.SetActive(true);
            textHealth.gameObject.SetActive(true);
            textCarHealth.gameObject.SetActive(true);
            textCarHealth1.gameObject.SetActive(true);
            textCarHealth2.gameObject.SetActive(true);
        }
    }

    private void Update()
    {
        if (localPlayer.IsUserInVR())
        {
            inGameMenu.transform.position = localPlayer.GetBonePosition(HumanBodyBones.LeftHand);
            inGameMenu.transform.rotation = localPlayer.GetBoneRotation(HumanBodyBones.LeftHand);
        }
    }

    public void SetCountdownTimerText(int value)
    {
        if (value <= 0)
        {
            textCountdownTimer.text = textCountdownTimerInGame.text = "";
        } else
        {
            textCountdownTimer.text = textCountdownTimerInGame.text = value.ToString();
        }
    }
}
