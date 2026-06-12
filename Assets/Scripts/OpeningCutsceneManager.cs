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

    public AudioClip line1;
    public AudioClip line2;
    public AudioClip line3;
    public AudioClip line4;
    public AudioClip line5;
    public AudioClip line6;

    [Header("Entity")]
    public GameObject entity;
    public SpriteRenderer entityRenderer;

    [Range (0f, 1f)]
    public float entityMaxAlpha = 0.4f;

    [Header("Next Scene")]
    public string nextSceneName = "Level1";




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

            yield return new WaitWhile(
                () => voiceSource.isPlaying
            );
        }
        else
        {
            yield return new WaitForSeconds(2f);
        }
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

    IEnumerator PlayerWalk()
    {
        playerAnimator.Play("PlayerWalk");

        while (
            Vector2.Distance(
                player.position,
                walkTarget.position
            ) > 0.05f)
        {
            player.position =
                Vector2.MoveTowards(
                    player.position,
                    walkTarget.position,
                    walkSpeed * Time.deltaTime
                );

            yield return null;
        }

        playerAnimator.Play("PlayerIdle");
    }
}