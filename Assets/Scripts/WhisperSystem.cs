using UnityEngine;

public class WhisperSystem : MonoBehaviour
{
    public PlayerController player;
    public EntityFollow entity;


    [Header("Whisper Timing")]
    public float minTriggerTime = 10f;
    public float maxTriggerTime = 15f;


    [Header("Reaction Time")]
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

        // COUNTDOWN UNTIL NEXT WHISPER
        if(!whisperActive && !turningBack)
        {
            triggerTimer -= Time.deltaTime;


            if(triggerTimer <= 0)
            {
                TriggerWhisper();
            }
        }



        // PLAYER TRYING TO RESIST
        if(whisperActive)
        {

            reactionTimer -= Time.deltaTime;


            if(Input.GetKeyDown(KeyCode.Space))
            {
                ResistWhisper();
            }



            if(reactionTimer <= 0)
            {
                StartTurningBack();
            }

        }




        // PLAYER RECOVERING FROM TURNING
        if(turningBack)
        {

            if(Input.GetKeyDown(KeyCode.Space))
            {
                Recover();
            }

        }

    }





    public void TriggerWhisper()
    {

        if(whisperActive || turningBack)
            return;



        whisperActive = true;


        reactionTimer = reactionTime;



        entity.SetPanic(false);



        if(ScreenEffects.instance != null)
            ScreenEffects.instance.SetWhisper();



        if(AudioManager.instance != null)
        {
            AudioManager.instance.PlayWhisper();
            AudioManager.instance.StartHeartbeat();
        }



        if(CameraShake.instance != null)
            CameraShake.instance.Shake(0.1f,0.2f);



        if(UIManager.instance != null)
            UIManager.instance.ShowWarning();



        Debug.Log("WHISPER!");

    }







    void ResistWhisper()
    {

        whisperActive = false;



        if(AudioManager.instance != null)
            AudioManager.instance.StopHeartbeat();



        if(UIManager.instance != null)
            UIManager.instance.HideWarning();



        SetNextTrigger();



        Debug.Log("RESISTED");

    }









    void StartTurningBack()
    {

        whisperActive = false;

        turningBack = true;



        entity.SetPanic(true);



        if(ScreenEffects.instance != null)
            ScreenEffects.instance.SetPanic();



        if(CameraShake.instance != null)
            CameraShake.instance.Shake(0.25f,0.3f);



        player.StartTurning();



        if(UIManager.instance != null)
            UIManager.instance.ShowWarning();



        Debug.Log("TURNING BACK!");

    }









    void Recover()
    {

        turningBack = false;



        entity.SetPanic(false);



        player.StopTurning();



        if(AudioManager.instance != null)
            AudioManager.instance.StopHeartbeat();



        if(ScreenEffects.instance != null)
            ScreenEffects.instance.SetWhisper();



        if(CameraShake.instance != null)
            CameraShake.instance.Shake(0.1f,0.1f);



        if(UIManager.instance != null)
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