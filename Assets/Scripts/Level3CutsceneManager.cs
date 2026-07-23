using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;


public class Level3CutsceneManager : MonoBehaviour
{

    [Header("Fade")]
    public Image blackFade;



    [Header("Background Transition")]
    public GameObject sewerBackground;
    public GameObject rooftopBackground;

    public float blackScreenDuration = 1f;



    [Header("Subtitle")]
    public TextMeshProUGUI subtitleText;



    [Header("Voice Over")]
    public AudioSource voiceSource;


    public AudioClip S1;
    public AudioClip S2;
    public AudioClip S3;

    public AudioClip E1;

    public AudioClip S4;
    public AudioClip S5;
    public AudioClip S6;

    public AudioClip E2;

    public AudioClip S7;





    [Header("Cutscene SFX")]
    public AudioSource sfxSource;


    public AudioClip lampFlickerSFX;
    public AudioClip blackoutSFX;
    public AudioClip whisperSFX;
    public AudioClip glitchSFX;





    [Header("Entity")]
    public GameObject entity;

    public SpriteRenderer entityRenderer;

    [Range(0f,1f)]
    public float entityMaxAlpha = 0.4f;






    [Header("Lighting")]
    public LightFlicker lightFlicker;

    public Light2D spotLight;







    [Header("Next Scene")]
    public string nextSceneName = "Level3";







    [Header("Skip")]
    public GameObject skipButton;

    public CanvasGroup skipCanvasGroup;

    public float showSkipAfter = 7f;



    private bool skipAllowed = false;





    void Start()
    {

        subtitleText.text = "";

        if(spotLight != null)
        spotLight.enabled = true;


        // Background setup

        if(sewerBackground != null)
            sewerBackground.SetActive(true);


        if(rooftopBackground != null)
            rooftopBackground.SetActive(false);




        // Entity setup

        if(entity != null)
            entity.SetActive(false);



        if(entityRenderer != null)
        {
            Color c = entityRenderer.color;
            c.a = 0;
            entityRenderer.color = c;
        }





        // Skip setup

        skipButton.SetActive(false);

        skipCanvasGroup.alpha = 0;

        skipCanvasGroup.interactable = false;

        skipCanvasGroup.blocksRaycasts = false;



        StartCoroutine(
            ShowSkipButton()
        );



        StartCoroutine(
            StartCutscene()
        );

    }







    IEnumerator StartCutscene()
    {

        yield return FadeIn();


        yield return PlayCutscene();

    }








    IEnumerator PlayCutscene()
    {



        // S1

        yield return PlayLine(
            "I can't...",
            S1
        );




        // S2

        yield return PlayLine(
            "How long have I been running?",
            S2
        );




        // S3

        yield return PlayLine(
            "Why won't it stop?",
            S3
        );





        // ENTITY WHISPER

        PlayWhisper();



        yield return PlayLine(
            "Sayy..",
            E1
        );







        // S4

        yield return PlayLine(
            "You're not real.",
            S4
        );







        // FIRST LIGHT FLICKER

        if(lampFlickerSFX != null)
            sfxSource.PlayOneShot(lampFlickerSFX);



        if(lightFlicker != null)
        {
            yield return lightFlicker.Flicker();
        }








        // S5

        yield return PlayLine(
            "No...I'm going home.",
            S5
        );









        // SECOND FLICKER + BLACKOUT


        if(lampFlickerSFX != null)
            sfxSource.PlayOneShot(lampFlickerSFX);



        if(lightFlicker != null)
        {
            yield return lightFlicker.Flicker();
        }





        yield return StartCoroutine(
            ChangeBackground()
        );









        // S6

        yield return PlayLine(
            "The rooftop? How did I get here?",
            S6
        );









        // ENTITY AGAIN


        PlayWhisper();



        yield return PlayLine(
            "Sayy...",
            E2
        );









        // S7

        yield return PlayLine(
            "Nooooo!",
            S7
        );






        if(glitchSFX != null)
        {
            sfxSource.PlayOneShot(glitchSFX);
        }






        yield return FadeOut();



        SceneManager.LoadScene(
            nextSceneName
        );

    }









    IEnumerator ChangeBackground()
    {


        if(blackoutSFX != null)
        {
            sfxSource.PlayOneShot(
                blackoutSFX
            );
        }





        Color c = blackFade.color;


        float timer = 0f;





        // Fade black

        while(timer < 1f)
        {

            timer += Time.deltaTime;


            c.a = Mathf.Lerp(
                0,
                1,
                timer
            );


            blackFade.color = c;


            yield return null;

        }






        // CHANGE BACKGROUND


        if(sewerBackground != null)
            sewerBackground.SetActive(false);



        if(rooftopBackground != null)
            rooftopBackground.SetActive(true);


        // Turn off the lamp after reaching the rooftop
        if(spotLight != null)
            spotLight.enabled = false;



        yield return new WaitForSeconds(
            blackScreenDuration
        );






        // Remove black


        timer = 0f;



        while(timer < 1f)
        {

            timer += Time.deltaTime;


            c.a = Mathf.Lerp(
                1,
                0,
                timer
            );


            blackFade.color = c;


            yield return null;

        }


    }









    void PlayWhisper()
    {

        if(whisperSFX != null)
        {
            sfxSource.PlayOneShot(
                whisperSFX
            );
        }

    }









    IEnumerator PlayLine(
        string subtitle,
        AudioClip clip
    )
    {


        subtitleText.text = subtitle;



        if(
            clip != null &&
            voiceSource != null
        )
        {

            voiceSource.clip = clip;

            voiceSource.Play();



            yield return new WaitWhile(
                ()=>voiceSource.isPlaying
            );

        }
        else
        {
            yield return new WaitForSeconds(2f);
        }



        subtitleText.text = "";

    }









    IEnumerator FadeIn()
    {

        Color c = blackFade.color;


        float timer = 0f;



        while(timer < 2f)
        {

            timer += Time.deltaTime;



            c.a = Mathf.Lerp(
                1,
                0,
                timer / 2
            );


            blackFade.color = c;



            yield return null;

        }

    }









    IEnumerator FadeOut()
    {

        Color c = blackFade.color;


        float timer = 0f;



        while(timer < 2f)
        {

            timer += Time.deltaTime;



            c.a = Mathf.Lerp(
                0,
                1,
                timer / 2
            );


            blackFade.color = c;



            yield return null;

        }

    }









    IEnumerator ShowSkipButton()
    {

        yield return new WaitForSeconds(
            showSkipAfter
        );



        skipButton.SetActive(true);



        float t = 0;



        while(t < 1)
        {

            t += Time.deltaTime;



            skipCanvasGroup.alpha = t;



            yield return null;

        }



        skipCanvasGroup.interactable = true;

        skipCanvasGroup.blocksRaycasts = true;


        skipAllowed = true;

    }









    public void SkipCutscene()
    {

        if(!skipAllowed)
            return;



        StopAllCoroutines();


        StartCoroutine(
            SkipRoutine()
        );

    }









    IEnumerator SkipRoutine()
    {

        if(voiceSource != null)
            voiceSource.Stop();



        yield return FadeOut();



        SceneManager.LoadScene(
            nextSceneName
        );

    }

}

