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
    public string glitchAnimationName = "GlitchAnimation";


    [Header("Timing")]
    public float glitchDuration = 1.5f;




    void Awake()
    {
        instance = this;
    }





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

            yield return null;
        }





        // Reset time before loading scene
        Time.timeScale = 1f;




        SceneManager.LoadScene("GameOver");
    }
}