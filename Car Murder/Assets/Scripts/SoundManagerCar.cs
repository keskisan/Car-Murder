
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

[UdonBehaviourSyncMode(BehaviourSyncMode.Continuous)]
public class SoundManagerCar : UdonSharpBehaviour
{
    [SerializeField]
    AudioSource splatSound, bonnetSound, honkSound, explosionSound;

    float honkDelayTimer = -2f;

    public void PSS()
    {
        splatSound.Play();
    }

    public void PlaySplatSound()
    {
        SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "PSS");
    }


    public void BNS()
    {
        bonnetSound.Play();
    }

    public void PlayBonnetSound()
    {
        SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "BNS");
    }

    public void HSP()
    {
        honkSound.Play();
    }

    public void PlayHonkSoundDelayed(float delay)
    {
        honkDelayTimer = delay;  
    }

    private void Update()
    {
        if (honkDelayTimer > 0f)
        {
            honkDelayTimer -= Time.deltaTime;
        }
        else if (honkDelayTimer > -1f)
        {
            SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "HSP");
            honkDelayTimer = -2f;
        }

    }

    public void EXP()
    {
        explosionSound.Play();
    }

    public void PlayExplosionSound()
    {
        SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "EXP");
    }
}
