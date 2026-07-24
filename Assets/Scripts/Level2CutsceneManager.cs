using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;


public class Level2CutsceneManager : MonoBehaviour
{

    [Header("Fade")]
    public Image blackFade;



    [Header("Subtitle")]
    public TextMeshProUGUI subtitleText;



    [Header("Voice")]
    public AudioSource voiceSource;


    public AudioClip S1;
    public AudioClip S2;
    public AudioClip S3;

    public AudioClip E1;

    public AudioClip S4;

    public AudioClip S5;
    public AudioClip S6;

    public AudioClip E2;




    [Header("Cutscene SFX")]
    public AudioSource sfxSource;


    public AudioClip lampFlickerSFX;
    public AudioClip blackoutSFX;
    public AudioClip whisperSFX;
    public AudioClip sewerAmbience;
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
    public string nextSceneName = "Level2";




    [Header("Skip")]
    public GameObject skipButton;

    public CanvasGroup skipCanvasGroup;

    public float showSkipAfter = 7f;



    void Start()
    {
        StartCoroutine(StartCutscene());
    }




    IEnumerator StartCutscene()
    {

        subtitleText.text = "";


        entity.SetActive(false);


        Color c = entityRenderer.color;
        c.a = 0;
        entityRenderer.color = c;



        skipButton.SetActive(false);

        skipCanvasGroup.alpha = 0;

        skipCanvasGroup.interactable = false;

        skipCanvasGroup.blocksRaycasts = false;



        StartCoroutine(
            ShowSkipButton()
        );



        yield return FadeIn();



        yield return PlayCutscene();

    }





    IEnumerator PlayCutscene()
    {


        // Sewer atmosphere preparation

        if(sewerAmbience != null)
        {
            sfxSource.PlayOneShot(sewerAmbience);
        }



        yield return PlayLine(
            "Huh...It's gone...",
            S1
        );



        // Lamp flickers

        if(lampFlickerSFX != null)
        {
            sfxSource.PlayOneShot(lampFlickerSFX);
        }


        yield return lightFlicker.Flicker();



        yield return PlayLine(
            "No...",
            S2
        );



        yield return PlayLine(
            "Not again...",
            S3
        );




        // Entity whisper


        PlayWhisperSound();



        yield return PlayLine(
            "Sayy..",
            E1
        );




        yield return PlayLine(
            "What do you want from me?",
            S4
        );





        // BLACKOUT TRANSITION


        if(blackoutSFX != null)
        {
            sfxSource.PlayOneShot(blackoutSFX);
        }



        yield return FadeToBlack();



        // Switch background happens here
        // Scene loading to Level 2 happens after fade



        yield return new WaitForSeconds(1f);



        yield return PlayLine(
            "Where am I?",
            S5
        );



        yield return PlayLine(
            "This wasn't here before...",
            S6
        );



        PlayWhisperSound();



        yield return PlayLine(
            "Sayy..",
            E2
        );




        if(glitchSFX != null)
        {
            sfxSource.PlayOneShot(glitchSFX);
        }



        yield return FadeOut();



        SceneManager.LoadScene(nextSceneName);

    }





    void PlayWhisperSound()
    {
        if(whisperSFX != null)
        {
            sfxSource.PlayOneShot(whisperSFX);
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

        float timer = 0;


        while(timer < 2)
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

        float timer = 0;


        while(timer < 2)
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





    IEnumerator FadeToBlack()
    {

        Color c = blackFade.color;

        float timer = 0;


        while(timer < 1)
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

    }





    public void SkipCutscene()
    {

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