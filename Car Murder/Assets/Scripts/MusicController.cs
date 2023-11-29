
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class MusicController : UdonSharpBehaviour
{
    [SerializeField]
    GameManager gameManager;

    [SerializeField]
    AudioSource music;

    [SerializeField]
    AudioClip[] audioClips;

    int currentClipIndex;

    private void Start()
    {
        SelectTrack();
    }

    private void Update()
    {
        if (music.isPlaying) return;

        SelectTrack();
    }

    private void SelectTrack()
    {
        currentClipIndex = Random.Range(0, audioClips.Length);

        music.clip = audioClips[currentClipIndex];
        music.Play();
        gameManager.menuManager.UpdateTrackName(audioClips[currentClipIndex].name);
    }

    public void changeVolume(float volume)
    {
        music.volume = volume;
    }
}
