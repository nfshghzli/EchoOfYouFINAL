using UnityEngine;
<<<<<<< HEAD
using UnityEngine.SceneManagement;
=======
using UnityEngine.UI;
>>>>>>> b47d0c4f3253e44916ff3d2e0af2482b73a1aa6c
using System.Collections;

public class ScreenFade : MonoBehaviour
{
    public static ScreenFade instance;

<<<<<<< HEAD

    [Header("Glitch")]
    public GameObject glitchOverlay;
    public Animator glitchAnimator;


    [Header("Animation Name")]
    public string glitchAnimationName = "GlitchAnimation";


    [Header("Timing")]
    public float glitchDuration = 1.5f;



=======
    public Image fadeImage;
    public GameObject glitchOverlay;
>>>>>>> b47d0c4f3253e44916ff3d2e0af2482b73a1aa6c

    void Awake()
    {
        instance = this;
    }

<<<<<<< HEAD




    public IEnumerator PlayDeathSequence()
    {
        Debug.Log("DEATH SEQUENCE START");



        // Freeze gameplay
        Time.timeScale = 0f;





        // Show glitch
        if(glitchOverlay != null)
        {
            glitchOverlay.SetActive(true);



            // Restart glitch animation from beginning
            if(glitchAnimator != null)
            {
                glitchAnimator.Play(
                    glitchAnimationName,
                    0,
                    0f
                );
            }
        }





        // Wait while time is frozen
        float timer = 0f;


        while(timer < glitchDuration)
        {
            timer += Time.unscaledDeltaTime;
=======
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
>>>>>>> b47d0c4f3253e44916ff3d2e0af2482b73a1aa6c

            yield return null;
        }

<<<<<<< HEAD




        // Reset time before loading scene
        Time.timeScale = 1f;




        SceneManager.LoadScene("GameOver");
=======
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
>>>>>>> b47d0c4f3253e44916ff3d2e0af2482b73a1aa6c
    }
}