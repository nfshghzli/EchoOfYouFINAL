using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ScreenFade : MonoBehaviour
{

    public static ScreenFade instance;



    [Header("Glitch")]
    public GameObject glitchOverlay;
    public Animator glitchAnimator;



    [Header("Animation Name")]
    public string glitchAnimationName = "GLITCH_IDLE";



    [Header("Timing")]
    public float glitchDuration = 1.5f;



    [Header("Death Audio")]
    public AudioSource deathAudioSource;
    public AudioClip deathVoice;



    private bool deathStarted = false;



    void Awake()
    {

        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

    }







    public IEnumerator PlayDeathSequence()
    {

        if(deathStarted)
            yield break;


        deathStarted = true;



        Debug.Log("DEATH SEQUENCE START");



        // Freeze gameplay
        Time.timeScale = 0f;






        // Play Sayy's death reaction

        if(
            deathAudioSource != null &&
            deathVoice != null
        )
        {
            deathAudioSource.PlayOneShot(
                deathVoice
            );
        }








        // Show glitch overlay

        if(glitchOverlay != null)
        {

            glitchOverlay.SetActive(true);



            if(glitchAnimator != null)
            {

                // Check animation exists first

                RuntimeAnimatorController controller =
                    glitchAnimator.runtimeAnimatorController;



                bool foundAnimation = false;



                if(controller != null)
                {

                    foreach(AnimationClip clip in controller.animationClips)
                    {

                        if(clip.name == glitchAnimationName)
                        {

                            foundAnimation = true;
                            break;

                        }

                    }

                }





                if(foundAnimation)
                {

                    glitchAnimator.Play(
                        glitchAnimationName,
                        0,
                        0f
                    );

                }
                else
                {

                    Debug.LogWarning(
                        "Glitch animation not found: "
                        + glitchAnimationName
                    );

                }

            }

        }








        // Wait even when time is frozen

        float timer = 0f;


        while(timer < glitchDuration)
        {

            timer += Time.unscaledDeltaTime;


            yield return null;

        }








        // Restore time

        Time.timeScale = 1f;






        SceneManager.LoadScene(
            "GameOver"
        );

    }

}