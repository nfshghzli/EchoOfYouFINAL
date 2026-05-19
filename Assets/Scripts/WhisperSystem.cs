using UnityEngine;
using UnityEngine.SceneManagement;

public class WhisperSystem : MonoBehaviour
{
    public PlayerController player;

    public EntityFollow entity;

    [Header("Whisper Timing")]
    public float minTriggerTime = 10f;
    public float maxTriggerTime = 12f;

    [Header("Player Reaction")]
    public float reactionTime = 1.5f;

    [Header("Death Timer")]
    public float failTime = 3f;

    private float triggerTimer;
    private float reactionTimer;
    private float failTimer;

    private bool whisperActive = false;
    private bool turningBack = false;

    void Start()
    {
        SetNextTrigger();
    }

    void Update()
    {
        // Trigger whisper randomly
        triggerTimer -= Time.deltaTime;

        if (triggerTimer <= 0
            && !whisperActive
            && !turningBack)
        {
            TriggerWhisper();
        }

        // RESIST WHISPER
        if (whisperActive
            && Input.GetKeyDown(KeyCode.Space))
        {
            ResistWhisper();
        }

        // Reaction timer
        if (whisperActive)
        {
            reactionTimer -= Time.deltaTime;

            if (reactionTimer <= 0)
            {
                StartTurningBack();
            }
        }

        // TURNING BACK STATE
        if (turningBack)
        {
            failTimer -= Time.deltaTime;

            Debug.Log(
                "TURNING BACK... "
                + failTimer.ToString("F1")
            );

            // Recover
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Recover();
            }

            // Entity catches player
            if (failTimer <= 0)
            {
                player.Die();
            }
        }
    }

    void TriggerWhisper()
    {
        entity.SetPanic(false);

        ScreenEffects.instance.SetWhisper();

        AudioManager.instance.PlayWhisper();
        AudioManager.instance.StartHeartbeat();

        CameraShake.instance.Shake(0.1f, 0.2f);

        whisperActive = true;

        reactionTimer = reactionTime;

        Debug.Log("WHISPER... PRESS SPACE!");

        UIManager.instance.ShowWarning();
    }

    void ResistWhisper()
    {
        AudioManager.instance.StopHeartbeat();

        whisperActive = false;

        Debug.Log("RESISTED");

        UIManager.instance.HideWarning();

        SetNextTrigger();
    }

    void StartTurningBack()
    {
        entity.SetPanic(true);

        ScreenEffects.instance.SetPanic();

        CameraShake.instance.Shake(0.25f, 0.3f);

        whisperActive = false;

        turningBack = true;

        failTimer = failTime;

        Debug.Log("TURNING BACK!");

        UIManager.instance.ShowWarning();

        player.StartTurning();
    }

    void Recover()
    {
        entity.SetPanic(false);

        ScreenEffects.instance.SetWhisper();

        AudioManager.instance.StopHeartbeat();

        CameraShake.instance.Shake(0.1f, 0.1f);

        turningBack = false;

        Debug.Log("RECOVERED!");

        UIManager.instance.HideWarning();

        player.StopTurning();

        SetNextTrigger();
    }

    void SetNextTrigger()
    {
        triggerTimer = Random.Range(
            minTriggerTime,
            maxTriggerTime
        );
    }
}