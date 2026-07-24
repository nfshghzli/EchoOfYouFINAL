using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;

public class OpeningCutsceneManager : MonoBehaviour
{
    [Header("Fade")]
    public Image blackFade;

<<<<<<< HEAD

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


=======
    [Header("Character")]
    public Transform player;
    public Transform walkTarget;

    public float walkSpeed = 2f;
    public Animator playerAnimator;

    [Header("Subtitle")]
    public TextMeshProUGUI subtitleText;

    [Header("Voice Over")]
    public AudioSource voiceSource;

    public AudioClip line1;
    public AudioClip line2;
    public AudioClip line3;
    public AudioClip line4;
    public AudioClip line5;
    public AudioClip line6;
>>>>>>> b47d0c4f3253e44916ff3d2e0af2482b73a1aa6c

    [Header("Entity")]
    public GameObject entity;
    public SpriteRenderer entityRenderer;

<<<<<<< HEAD
    [Range(0f,1f)]
    public float entityMaxAlpha = 0.4f;



=======
    [Range (0f, 1f)]
    public float entityMaxAlpha = 0.4f;

>>>>>>> b47d0c4f3253e44916ff3d2e0af2482b73a1aa6c
    [Header("Next Scene")]
    public string nextSceneName = "Level1";



<<<<<<< HEAD
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

=======

    IEnumerator Start()
    {
        yield return StartCoroutine(FadeIn());

        yield return StartCoroutine(PlayerWalk());

        yield return StartCoroutine(PlayCutscene());

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
>>>>>>> b47d0c4f3253e44916ff3d2e0af2482b73a1aa6c

            yield return null;
        }
    }

<<<<<<< HEAD




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

=======
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
>>>>>>> b47d0c4f3253e44916ff3d2e0af2482b73a1aa6c

            yield return null;
        }
    }

<<<<<<< HEAD




    IEnumerator PlayLine(string subtitle, AudioClip clip)
    {
        subtitleText.text = subtitle;


        if(clip != null)
        {
            voiceSource.clip = clip;

            voiceSource.Play();


=======
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

    IEnumerator PlayLine(
        string subtitle,
        AudioClip voiceClip
    )
    {
        subtitleText.text = subtitle;

        if (voiceClip != null)
        {
            voiceSource.clip = voiceClip;
            voiceSource.Play();

>>>>>>> b47d0c4f3253e44916ff3d2e0af2482b73a1aa6c
            yield return new WaitWhile(
                () => voiceSource.isPlaying
            );
        }
        else
        {
            yield return new WaitForSeconds(2f);
        }
<<<<<<< HEAD


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





=======
    }

    IEnumerator MoveEntity(
        float targetX,
        float speed
    )
    {
        while (
            Mathf.Abs(
                entity.transform.position.x
                - targetX
            ) > 0.1f)
        {
            entity.transform.position =
                Vector3.MoveTowards(
                    entity.transform.position,
                    new Vector3(
                        targetX,
                        entity.transform.position.y,
                        entity.transform.position.z
                    ),
                    speed * Time.deltaTime
                );

            yield return null;
        }
    }
    IEnumerator PlayCutscene()
    {
        yield return StartCoroutine(
            PlayLine(
                "I don't know when it started...",
                line1
            )
        );

        yield return StartCoroutine(
            PlayLine(
                "But lately...",
                line2
            )
        );

        yield return StartCoroutine(
            PlayLine(
                "It feels like something is following me.",
                line3
            )
        );

        yield return StartCoroutine(
            PlayLine(
                "No matter where I go...",
                line4
            )
        );

        yield return StartCoroutine(
            PlayLine(
                "I can feel it.",
                line5
            )
        );

        yield return StartCoroutine(
            PlayLine(
                "Maybe I'm imagining things.",
                line6
            )
        );

        // ENTITY APPEARS

       yield return StartCoroutine(
            FadeInEntity()
        );

        subtitleText.text = "...";

        yield return new WaitForSeconds(2f);

        subtitleText.text = "What...?";

        yield return new WaitForSeconds(2f);

        playerAnimator.Play("PlayerIdle");

        yield return StartCoroutine(
            MoveEntity(
                1f,
                1f
            )
        );

        subtitleText.text = "Who's there?";

        yield return new WaitForSeconds(2f);

        yield return StartCoroutine(
            MoveEntity(
                3f,
                1f
            )
        );

        yield return StartCoroutine(
            MoveEntity(
                4f,
                1f
            )
        );
        
        subtitleText.text = "\"Sayy...\"";

        yield return new WaitForSeconds(2f);

        subtitleText.text = "What did you just—";

        yield return new WaitForSeconds(2f);

        subtitleText.text = "\"Sayy...\"";

        yield return new WaitForSeconds(3f);

        subtitleText.text = "No...";

        yield return new WaitForSeconds(2f);

        subtitleText.text = "";

        yield return StartCoroutine(FadeOut());

        SceneManager.LoadScene(nextSceneName);
    }

>>>>>>> b47d0c4f3253e44916ff3d2e0af2482b73a1aa6c
    IEnumerator PlayerWalk()
    {
        playerAnimator.Play("PlayerWalk");

<<<<<<< HEAD


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
=======
        while (
            Vector2.Distance(
                player.position,
                walkTarget.position
            ) > 0.05f)
>>>>>>> b47d0c4f3253e44916ff3d2e0af2482b73a1aa6c
        {
            player.position =
                Vector2.MoveTowards(
                    player.position,
                    walkTarget.position,
                    walkSpeed * Time.deltaTime
                );

<<<<<<< HEAD

            yield return null;
        }



        sfxSource.Stop();


=======
            yield return null;
        }

>>>>>>> b47d0c4f3253e44916ff3d2e0af2482b73a1aa6c
        playerAnimator.Play("PlayerIdle");
    }
}