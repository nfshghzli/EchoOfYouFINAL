using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScreenFade : MonoBehaviour
{
    public static ScreenFade instance;

    public Image fadeImage;
    public GameObject glitchOverlay;

    void Awake()
    {
        instance = this;
    }

    public void FadeToBlack(float duration)
    {
        StartCoroutine(FadeRoutine(duration));
    }

    IEnumerator FadeRoutine(float duration)
    {
        float timer = 0;

        Color color = fadeImage.color;

        while (timer < duration)
        {
            timer += Time.deltaTime;

            color.a = Mathf.Lerp(
                0,
                1,
                timer / duration
            );

            fadeImage.color = color;

            yield return null;
        }

        color.a = 1;
        fadeImage.color = color;
    }

    public IEnumerator PlayDeathSequence()
    {
        // Show glitch immediately
        glitchOverlay.SetActive(true);

        // Stop gameplay feeling
        Time.timeScale = 0f;

        float timer = 0f;

        // Hold glitch for 1 second
        while (timer < 1f)
        {
            timer += Time.unscaledDeltaTime;
            yield return null;
        }

        // Fade black over glitch
        Color c = fadeImage.color;

        timer = 0f;

        while (timer < 1f)
        {
            timer += Time.unscaledDeltaTime;

            c.a = Mathf.Lerp(
                0,
                1,
                timer
            );

            fadeImage.color = c;

            yield return null;
        }

        // Fully black now
        yield return new WaitForSecondsRealtime(0.3f);

        Time.timeScale = 1f;

        UnityEngine.SceneManagement.SceneManager
            .LoadScene("GameOver");
    }
}