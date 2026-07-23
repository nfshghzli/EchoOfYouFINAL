using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;



    [Header("Audio Sources")]
    public AudioSource whisperSource;
    public AudioSource heartbeatSource;
    public AudioSource sfxSource;
    public AudioSource runningSource;



    [Header("Audio Clips")]
    public AudioClip whisperClip;
    public AudioClip heartbeatClip;
    public AudioClip hitClip;

    public AudioClip runningClip;
    public AudioClip jumpClip;
    public AudioClip slideClip;

    public AudioClip deathClip;




    void Awake()
    {
        instance = this;
    }






    // =========================
    // WHISPER
    // =========================

    public void PlayWhisper()
    {
        if(whisperSource != null && whisperClip != null)
        {
            whisperSource.PlayOneShot(whisperClip);
        }
    }







    // =========================
    // HEARTBEAT
    // =========================

    public void StartHeartbeat()
    {
        if(heartbeatSource != null &&
           !heartbeatSource.isPlaying)
        {
            heartbeatSource.clip = heartbeatClip;
            heartbeatSource.loop = true;
            heartbeatSource.Play();
        }
    }





    public void StopHeartbeat()
    {
        if(heartbeatSource != null)
        {
            heartbeatSource.Stop();
        }
    }







    // =========================
    // NORMAL SFX
    // =========================

    public void PlayHit()
    {
        if(sfxSource != null && hitClip != null)
        {
            sfxSource.PlayOneShot(hitClip);
        }
    }





    public void PlayJump()
    {
        if(sfxSource != null && jumpClip != null)
        {
            sfxSource.PlayOneShot(jumpClip);
        }
    }





    public void PlaySlide()
    {
        if(sfxSource != null && slideClip != null)
        {
            sfxSource.PlayOneShot(slideClip);
        }
    }







    // =========================
    // RUNNING LOOP
    // =========================

    public void StartRunning()
    {
        if(runningSource != null &&
           runningClip != null &&
           !runningSource.isPlaying)
        {
            runningSource.clip = runningClip;
            runningSource.loop = true;
            runningSource.Play();
        }
    }





    public void StopRunning()
    {
        if(runningSource != null)
        {
            runningSource.Stop();
        }
    }



    public void PlayDeath()
    {
        if(deathClip != null)
        {
            sfxSource.PlayOneShot(deathClip);
        }
    }




    // =========================
    // PAUSE AUDIO
    // =========================

    public void PauseAllAudio()
    {
        if(runningSource != null)
            runningSource.Pause();


        if(whisperSource != null)
            whisperSource.Pause();


        if(heartbeatSource != null)
            heartbeatSource.Pause();


        if(sfxSource != null)
            sfxSource.Pause();
    }







    public void ResumeAllAudio()
    {
        if(runningSource != null)
            runningSource.UnPause();


        if(whisperSource != null)
            whisperSource.UnPause();


        if(heartbeatSource != null)
            heartbeatSource.UnPause();


        if(sfxSource != null)
            sfxSource.UnPause();
    }
}