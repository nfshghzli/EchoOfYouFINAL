using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;

public class OpeningCutsceneManager : MonoBehaviour
{
    [Header("Fade")]
    public Image blackFade;


    [Header("Character")]
    public Transform player;
    public Transform walkTarget;
    public float walkSpeed = 2f;
    public Animator playerAnimator;



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

    public AudioClip walkingSFX;
    public AudioClip windSFX;
    public AudioClip whisperSFX;
    public AudioClip entityAppearSFX;



    [Header("Entity")]
    public GameObject entity;
    public SpriteRenderer entityRenderer;

    [Range(0f,1f)]
    public float entityMaxAlpha = 0.4f;



    [Header("Next Scene")]
    public string nextSceneName = "Level1";



    [Header("Skip Button")]
    public GameObject skipButton;
    public CanvasGroup skipCanvasGroup;
    public float showSkipAfter = 7f;



    private bool skipAllowed = false;



    void Start()
    {
        skipButton.SetActive(false);

        skipCanvasGroup.alpha = 0;
        skipCanvasGroup.interactable = false;
        skipCanvasGroup.blocksRaycasts = false;


        StartCoroutine(CutsceneFlow());

        StartCoroutine(EnableSkipAfterDelay());
    }



    // ============================
    // SKIP SYSTEM
    // ============================


    IEnumerator EnableSkipAfterDelay()
    {
        yield return new WaitForSeconds(showSkipAfter);


        skipButton.SetActive(true);


        float t = 0;

        while(t < 1)
        {
            t += Time.deltaTime;

            skipCanvasGroup.alpha = t;

            yield return null;
        }


        skipCanvasGroup.alpha = 1;

        skipCanvasGroup.interactable = true;
        skipCanvasGroup.blocksRaycasts = true;


        skipAllowed = true;
    }



    public void SkipCutscene()
    {
        if(!skipAllowed)
            return;


        StartCoroutine(SkipRoutine());
    }



    IEnumerator SkipRoutine()
    {
        yield return StartCoroutine(FadeOut());


        SceneManager.LoadScene(nextSceneName);
    }




    // ============================
    // CUTSCENE FLOW
    // ============================


    IEnumerator CutsceneFlow()
    {
        yield return StartCoroutine(FadeIn());


        yield return StartCoroutine(PlayerWalk());


        yield return StartCoroutine(PlayCutscene());


        SceneManager.LoadScene(nextSceneName);
    }





    IEnumerator FadeIn()
    {
        Color c = blackFade.color;

        float t = 0;


        while(t < 2)
        {
            t += Time.deltaTime;

            c.a = Mathf.Lerp(
                1,
                0,
                t / 2
            );


            blackFade.color = c;


            yield return null;
        }
    }





    IEnumerator FadeOut()
    {
        Color c = blackFade.color;

        float t = 0;


        while(t < 2)
        {
            t += Time.deltaTime;

            c.a = Mathf.Lerp(
                0,
                1,
                t / 2
            );


            blackFade.color = c;


            yield return null;
        }
    }





    IEnumerator PlayLine(string subtitle, AudioClip clip)
    {
        subtitleText.text = subtitle;


        if(clip != null)
        {
            voiceSource.clip = clip;

            voiceSource.Play();


            yield return new WaitWhile(
                () => voiceSource.isPlaying
            );
        }
        else
        {
            yield return new WaitForSeconds(2f);
        }


        subtitleText.text = "";
    }






    IEnumerator FadeInEntity()
    {
        Color c = entityRenderer.color;


        float t = 0;


        while(t < 3)
        {
            t += Time.deltaTime;


            c.a = Mathf.Lerp(
                0,
                entityMaxAlpha,
                t / 3
            );


            entityRenderer.color = c;


            yield return null;
        }


        c.a = entityMaxAlpha;

        entityRenderer.color = c;
    }





    IEnumerator PlayCutscene()
    {

        // Atmosphere

        if(windSFX != null)
        {
            sfxSource.PlayOneShot(windSFX);
        }



        // Sayy dialogue


        yield return PlayLine(
            "Why did I stay this late again...",
            S1
        );


        yield return PlayLine(
            "It's just a shortcut. I'll be fine.",
            S2
        );


        yield return PlayLine(
            "Why does this place feel so quiet?",
            S3
        );




        // First whisper


        if(whisperSFX != null)
        {
            sfxSource.PlayOneShot(whisperSFX);
        }


        yield return PlayLine(
            "Sayy...",
            E1
        );



        yield return PlayLine(
            "Did someone just call me?",
            S4
        );



        yield return PlayLine(
            "No...I'm just imagining things.",
            S5
        );



        yield return PlayLine(
            "I just need to get home.",
            S6
        );




        // Second whisper


        if(whisperSFX != null)
        {
            sfxSource.PlayOneShot(whisperSFX);
        }


        yield return PlayLine(
            "Sayy...",
            E2
        );



        yield return PlayLine(
            "Wait...What was that?",
            S7
        );




        // Entity reveal


        if(entityAppearSFX != null)
        {
            sfxSource.PlayOneShot(entityAppearSFX);
        }


        yield return FadeInEntity();


        yield return new WaitForSeconds(1f);


        yield return FadeOut();
    }





    IEnumerator PlayerWalk()
    {
        playerAnimator.Play("PlayerWalk");



        if(walkingSFX != null)
        {
            sfxSource.clip = walkingSFX;

            sfxSource.loop = true;

            sfxSource.Play();
        }




        while(
            Vector2.Distance(
                player.position,
                walkTarget.position
            ) > 0.05f
        )
        {
            player.position =
                Vector2.MoveTowards(
                    player.position,
                    walkTarget.position,
                    walkSpeed * Time.deltaTime
                );


            yield return null;
        }



        sfxSource.Stop();


        playerAnimator.Play("PlayerIdle");
    }
}