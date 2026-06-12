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
        }
        else
        {
            yield return new WaitForSeconds(2f);
        }
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
