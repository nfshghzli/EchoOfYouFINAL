using UnityEngine;

public class WhisperSystem : MonoBehaviour
{
    public PlayerController player;
    public EntityFollow entity;

    [Header("Whisper Timing")]
    public float minTriggerTime = 10f;
    public float maxTriggerTime = 12f;

    [Header("Player Reaction")]
    public float reactionTime = 1.5f;

    private float triggerTimer;
    private float reactionTimer;

    private bool whisperActive = false;
    private bool turningBack = false;

    void Start()
    {
        SetNextTrigger();
    }

    void Update()
    {
        triggerTimer -= Time.deltaTime;

        if (triggerTimer <= 0 &&
            !whisperActive &&
            !turningBack)
        {
            TriggerWhisper();
        }

        // Resist whisper
        if (whisperActive &&
            Input.GetKeyDown(KeyCode.Space))
        {
            ResistWhisper();
        }

        // Countdown before turning
        if (whisperActive)
        {
            reactionTimer -= Time.deltaTime;

            if (reactionTimer <= 0)
            {
                StartTurningBack();
            }
        }

        // Recover while turning
        if (turningBack)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Recover();
            }
        }
    }

    void TriggerWhisper()
    {
        whisperActive = true;

        reactionTimer = reactionTime;

        entity.SetPanic(false);

        ScreenEffects.instance.SetWhisper();

        AudioManager.instance.PlayWhisper();
        AudioManager.instance.StartHeartbeat();

        CameraShake.instance.Shake(0.1f, 0.2f);

        UIManager.instance.ShowWarning();

        Debug.Log("WHISPER!");
    }

    void ResistWhisper()
    {
        whisperActive = false;

        AudioManager.instance.StopHeartbeat();

        UIManager.instance.HideWarning();

        SetNextTrigger();

        Debug.Log("RESISTED");
    }

    void StartTurningBack()
    {
        whisperActive = false;
        turningBack = true;

        entity.SetPanic(true);

        ScreenEffects.instance.SetPanic();

        CameraShake.instance.Shake(0.25f, 0.3f);

        player.StartTurning();

        UIManager.instance.ShowWarning();

        Debug.Log("TURNING BACK!");
    }

    void Recover()
    {
        turningBack = false;

        entity.SetPanic(false);

        player.StopTurning();

        AudioManager.instance.StopHeartbeat();

        ScreenEffects.instance.SetWhisper();

        CameraShake.instance.Shake(0.1f, 0.1f);

        UIManager.instance.HideWarning();

        SetNextTrigger();

        Debug.Log("RECOVERED");
    }

    void SetNextTrigger()
    {
        triggerTimer = Random.Range(
            minTriggerTime,
            maxTriggerTime
        );
    }
}