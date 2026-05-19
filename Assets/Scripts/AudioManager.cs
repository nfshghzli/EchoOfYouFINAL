using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("Audio Sources")]
    public AudioSource whisperSource;
    public AudioSource heartbeatSource;
    public AudioSource sfxSource;

    [Header("Audio Clips")]
    public AudioClip whisperClip;
    public AudioClip heartbeatClip;
    public AudioClip hitClip;

    void Awake()
    {
        instance = this;
    }

    public void PlayWhisper()
    {
        whisperSource.PlayOneShot(whisperClip);
    }

    public void StartHeartbeat()
    {
        if (!heartbeatSource.isPlaying)
        {
            heartbeatSource.clip = heartbeatClip;
            heartbeatSource.loop = true;
            heartbeatSource.Play();
        }
    }

    public void StopHeartbeat()
    {
        heartbeatSource.Stop();
    }

    public void PlayHit()
    {
        sfxSource.PlayOneShot(hitClip);
    }
}