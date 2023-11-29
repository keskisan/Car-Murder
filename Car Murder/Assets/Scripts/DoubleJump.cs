
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using VRC.Udon.Common;

[UdonBehaviourSyncMode(BehaviourSyncMode.Manual)]
public class DoubleJump : UdonSharpBehaviour
{
    [SerializeField]
    PlayerManager playerManager;

    [SerializeField]
    float jupminpulse = 5f;
    [SerializeField]
    float sidewaysImpulse = 2f;
    int canjump = 0;

    bool canJumpSideways;
    float sidewaysMovement;

    VRCPlayerApi localPlayer;

    private void Start()
    {
        localPlayer = Networking.LocalPlayer;
    }

    private void Update()
    {
        if (localPlayer.IsPlayerGrounded())
        {
            canjump = 1;
            canJumpSideways = true;
        }
    }

    public override void InputMoveHorizontal(float value, UdonInputEventArgs args)
    {
        sidewaysMovement = value;
        
    }


    public override void InputJump(bool value, VRC.Udon.Common.UdonInputEventArgs args)
    {
        if (value)
        {
            if (canjump >= 0)
            {
                canjump -= 1;
                
                Vector3 newVelocity = localPlayer.GetVelocity();
                if (canJumpSideways)
                {
                    Vector3 tmpVelocity = new Vector3(sidewaysMovement * sidewaysImpulse, jupminpulse, 0f);
                    newVelocity += playerManager.LocalPlayerCapsule.transform.TransformDirection(tmpVelocity);     
                }
                else
                {
                    newVelocity += new Vector3(0f, jupminpulse, 0f);
                }
                
                localPlayer.SetVelocity(newVelocity);

                canJumpSideways = false;
            }
        }   
    }
}
