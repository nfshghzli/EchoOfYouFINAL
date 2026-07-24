using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;

<<<<<<< HEAD

public class Level2CutsceneManager : MonoBehaviour
{

    [Header("Fade")]
    public Image blackFade;



    [Header("Background Transition")]
    public GameObject level1Background;
    public GameObject sewerBackground;

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



    [Header("Cutscene SFX")]
    public AudioSource sfxSource;


    public AudioClip lampFlickerSFX;
    public AudioClip blackoutSFX;
    public AudioClip whisperSFX;
    public AudioClip glitchSFX;
    public AudioClip sewerAmbience;



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



    [Header("Skip Button")]
    public GameObject skipButton;

    public CanvasGroup skipCanvasGroup;

    public float showSkipAfter = 7f;



    private bool skipAllowed = false;




    void Start()
    {

        subtitleText.text = "";


        // Background setup

        if(level1Background != null)
            level1Background.SetActive(true);


        if(sewerBackground != null)
            sewerBackground.SetActive(false);



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

        // underground ambience preparation

        if(sewerAmbience != null)
        {
            sfxSource.PlayOneShot(sewerAmbience);
        }




        // S1

        yield return PlayLine(
            "Huh...It's gone...",
            S1
        );




        // light flicker moment

        if(lampFlickerSFX != null)
        {
            sfxSource.PlayOneShot(lampFlickerSFX);
        }


        if(lightFlicker != null)
        {
            yield return lightFlicker.Flicker();
        }





        // S2

        yield return PlayLine(
            "No...",
            S2
        );





        // S3

        yield return PlayLine(
            "Not again...",
            S3
        );






        // Entity whisper

        PlayWhisper();



        yield return PlayLine(
            "Sayy..",
            E1
        );






        // S4

        yield return PlayLine(
            "What do you want from me?",
            S4
        );






        // BACKGROUND CHANGE

        yield return StartCoroutine(
            ChangeBackground()
        );






        // S5

        yield return PlayLine(
            "Where am I?",
            S5
        );






        // S6

        yield return PlayLine(
            "This wasn't here before...",
            S6
        );






        // Second whisper

        PlayWhisper();



        yield return PlayLine(
            "Sayy..",
            E2
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

        // blackout sound

        if(blackoutSFX != null)
        {
            sfxSource.PlayOneShot(blackoutSFX);
        }




        Color c = blackFade.color;


        float timer = 0;



        // fade black

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






        // SWITCH BACKGROUND HERE


        if(level1Background != null)
            level1Background.SetActive(false);



        if(sewerBackground != null)
            sewerBackground.SetActive(true);







        yield return new WaitForSeconds(
            blackScreenDuration
        );







        // fade back


        timer = 0;



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

=======
public class Level2CutsceneManager : MonoBehaviour
{
    [Header("Fade")]
    public Image blackFade;

    [Header("Subtitle")]
    public TextMeshProUGUI subtitleText;

    [Header("Voice Over")]
    public AudioSource voiceSource;

    public AudioClip line1;
    public AudioClip line2;
    public AudioClip line3;
    public AudioClip line4;

    [Header("Entity")]
    public GameObject entity;
    public SpriteRenderer entityRenderer;
    [Range (0f, 1f)]
    public float entityMaxAlpha = 0.4f;

    [Header("Next Scene")]
    public string nextSceneName = "Level2";

    public LightFlicker lightFlicker;
    public Light2D spotLight;

    IEnumerator Start()
    {
        subtitleText.text = "";

        entity.SetActive(false);

        Color entityColor = entityRenderer.color;
        entityColor.a = 0f;
        entityRenderer.color = entityColor;

        yield return StartCoroutine(FadeIn());

        yield return StartCoroutine(PlayCutscene());
    }

    IEnumerator PlayCutscene()
    {
        yield return StartCoroutine(
            PlayLine(
                "Who was that...?",
                line1
            )
        );

        yield return StartCoroutine(
            PlayLine(
                "Why does it keep whispering my name?",
                line2
            )
        );

        yield return StartCoroutine(
            PlayLine(
                "It felt real.",
                line3
            )
        );

        yield return StartCoroutine(
            PlayLine(
                "No... it IS real.",
                line4
            )
        );

        yield return new WaitForSeconds(1f);

        yield return StartCoroutine(
            lightFlicker.Flicker()
        );

        entity.SetActive(true);

        yield return StartCoroutine(
            FadeInEntity()
        );

        yield return new WaitForSeconds(1f);

        subtitleText.text =
            "\"Sayy...\"";

        yield return new WaitForSeconds(3f);

        subtitleText.text =
            "\"Turn around...\"";

        yield return new WaitForSeconds(3f);

       subtitleText.text =
            "No.";

        yield return new WaitForSeconds(0.5f);

        spotLight.enabled = false;

        subtitleText.text = "";

        yield return new WaitForSeconds(1f);

        subtitleText.text =
            "\"Sayy...\"";

        yield return new WaitForSeconds(2f);

        subtitleText.text = "";

        yield return StartCoroutine(
            FadeOut()
        );

        SceneManager.LoadScene(nextSceneName);
    }

    IEnumerator PlayLine(
        string subtitle,
        AudioClip voiceClip
    )
    {
        subtitleText.text = subtitle;

        if (
            voiceClip != null
            && voiceSource != null
        )
        {
            voiceSource.clip = voiceClip;
            voiceSource.Play();

            yield return new WaitWhile(
                () => voiceSource.isPlaying
            );
>>>>>>> b47d0c4f3253e44916ff3d2e0af2482b73a1aa6c
        }
        else
        {
            yield return new WaitForSeconds(2f);
        }
<<<<<<< HEAD



        subtitleText.text = "";

    }







    IEnumerator FadeIn()
    {

        Color c = blackFade.color;

        float timer = 0;



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

        float timer = 0;



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







    IEnumerator FadeInEntity()
    {

        Color c = entityRenderer.color;


        float timer = 0;



        while(timer < 3f)
        {

            timer += Time.deltaTime;


            c.a = Mathf.Lerp(
                0,
                entityMaxAlpha,
                timer / 3
            );


            entityRenderer.color = c;



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
=======
    }

    IEnumerator FadeIn()
    {
        Color color = blackFade.color;

        float timer = 0f;

        while (timer < 2f)
        {
            timer += Time.deltaTime;

            color.a = Mathf.Lerp(
                1f,
                0f,
                timer / 2f
            );

            blackFade.color = color;

            yield return null;
        }
    }

    IEnumerator FadeOut()
    {
        Color color = blackFade.color;

        float timer = 0f;

        while (timer < 2f)
        {
            timer += Time.deltaTime;

            color.a = Mathf.Lerp(
                0f,
                1f,
                timer / 2f
            );

            blackFade.color = color;

            yield return null;
        }
    }

    IEnumerator FadeInEntity()
    {
        if (entityRenderer == null)
        {
            Debug.LogError("Entity Renderer not assigned!");
            yield break;
        }

        Color color = entityRenderer.color;

        float timer = 0f;

        while (timer < 3f)
        {
            timer += Time.deltaTime;

            color.a = Mathf.Lerp(
                0f,
                entityMaxAlpha,
                timer / 2f
            );

            entityRenderer.color = color;

            yield return null;
        }


        color.a = entityMaxAlpha;
        entityRenderer.color = color;
    }

   
}
>>>>>>> b47d0c4f3253e44916ff3d2e0af2482b73a1aa6c
