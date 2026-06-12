using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndingManager : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI subtitleText;
    public Image endingImage1;
    public Image endingImage2;

    [Header("Entity")]
    public GameObject entity;
    public SpriteRenderer entityRenderer;

    [Header("Fade")]
    public Image blackFade;

    IEnumerator Start()
    {
        // Hide ending cards
        endingImage1.gameObject.SetActive(false);
        endingImage2.gameObject.SetActive(false);

        // Hide entity
        entity.SetActive(false);

        // Make entity transparent
        Color entityColor = entityRenderer.color;
        entityColor.a = 0f;
        entityRenderer.color = entityColor;

        // Start black
        Color fadeColor = blackFade.color;
        fadeColor.a = 1f;
        blackFade.color = fadeColor;

        yield return StartCoroutine(FadeIn());

        yield return StartCoroutine(EndingSequence());
    }

    IEnumerator EndingSequence()
    {
        subtitleText.text = "I spent all this time running...";
        yield return new WaitForSeconds(3f);

        subtitleText.text = "Pretending it wasn't there.";
        yield return new WaitForSeconds(3f);

        subtitleText.text = "Pretending I couldn't hear it.";
        yield return new WaitForSeconds(3f);

        subtitleText.text = "Pretending I wasn't afraid.";
        yield return new WaitForSeconds(4f);

        subtitleText.text = "";
        yield return new WaitForSeconds(2f);

        // First entity appearance
        entity.SetActive(true);

        yield return StartCoroutine(FadeInEntity());

        yield return new WaitForSeconds(1f);

        yield return StartCoroutine(FadeOutEntity());

        entity.SetActive(false);

        yield return new WaitForSeconds(2f);

        subtitleText.text = "So why is it still here?";
        yield return new WaitForSeconds(4f);

        subtitleText.text = "\"Sayy...\"";
        yield return new WaitForSeconds(3f);

        subtitleText.text = "Who are you?";
        yield return new WaitForSeconds(3f);

        subtitleText.text = "\"You already know.\"";
        yield return new WaitForSeconds(4f);

        subtitleText.text = "...";
        yield return new WaitForSeconds(3f);

        subtitleText.text = "\"Soon.\"";
        yield return new WaitForSeconds(3f);

        subtitleText.text = "\"You'll remember.\"";
        yield return new WaitForSeconds(4f);

        subtitleText.text = "";

        // Final entity reveal
        entity.SetActive(true);

        yield return StartCoroutine(FadeInEntity());

        yield return new WaitForSeconds(2f);

        // Fade to black
        yield return StartCoroutine(FadeOut());

        yield return StartCoroutine(ShowEndingCard());
    }

    IEnumerator ShowEndingCard()
    {
        endingImage1.gameObject.SetActive(true);

        yield return new WaitForSeconds(3f);

        endingImage2.gameObject.SetActive(true);
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
        Color color = entityRenderer.color;

        float timer = 0f;

        while (timer < 2f)
        {
            timer += Time.deltaTime;

            color.a = Mathf.Lerp(
                0f,
                1f,
                timer / 2f
            );

            entityRenderer.color = color;

            yield return null;
        }
    }

    IEnumerator FadeOutEntity()
    {
        Color color = entityRenderer.color;

        float timer = 0f;

        while (timer < 2f)
        {
            timer += Time.deltaTime;

            color.a = Mathf.Lerp(
                1f,
                0f,
                timer / 2f
            );

            entityRenderer.color = color;

            yield return null;
        }
    }
}
