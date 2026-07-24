using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class EndingManager : MonoBehaviour
{


    [Header("Subtitle")]
    public TextMeshProUGUI subtitleText;



    [Header("Voice")]
    public AudioSource voiceSource;


    public AudioClip S1;
    public AudioClip S2;

    public AudioClip E1;

    public AudioClip S3;

    public AudioClip E2;
    public AudioClip E3;





    [Header("SFX")]
    public AudioSource sfxSource;


    public AudioClip whisperSFX;
    public AudioClip glitchSFX;






    [Header("Entity")]
    public GameObject entity;

    public SpriteRenderer entityRenderer;

    public float entityAlpha = 1f;








    [Header("Fade")]
    public Image blackFade;







    [Header("Game Title")]
    public GameObject titleCard;

    public GameObject comingSoonCard;

    public float titleDelay = 3f;






    [Header("Ending Buttons")]
    public GameObject buttonPanel;

    public string firstLevelScene = "Level1";

    public string mainMenuScene = "MainMenu";





    void Start()
    {


        if(subtitleText != null)
            subtitleText.text = "";



        if(titleCard != null)
            titleCard.SetActive(false);


        if(comingSoonCard != null)
            comingSoonCard.SetActive(false);



        if(buttonPanel != null)
            buttonPanel.SetActive(false);




        if(entity != null)
            entity.SetActive(false);




        if(entityRenderer != null)
        {
            Color c = entityRenderer.color;
            c.a = 0;
            entityRenderer.color = c;
        }




        if(blackFade != null)
        {
            Color fade = blackFade.color;
            fade.a = 1;
            blackFade.color = fade;
        }





        StartCoroutine(
            EndingSequence()
        );

    }







    IEnumerator EndingSequence()
    {


        yield return StartCoroutine(FadeIn());




        // SAYY S1

        yield return PlayLine(
            "I made it.",
            S1
        );




        // SAYY S2

        yield return PlayLine(
            "It's finally over.",
            S2
        );





        yield return new WaitForSeconds(2f);






        // ENTITY APPEARS


        if(entity != null)
            entity.SetActive(true);



        yield return StartCoroutine(
            FadeInEntity()
        );



        PlayWhisper();







        // ENTITY E1


        yield return PlayLine(
            "You never escaped.",
            E1
        );







        yield return new WaitForSeconds(2f);







        // SAYY S3


        yield return PlayLine(
            "No...That's impossible.",
            S3
        );







        yield return new WaitForSeconds(2f);





        PlayWhisper();








        // ENTITY E2


        yield return PlayLine(
            "Look back.",
            E2
        );






        yield return new WaitForSeconds(2f);







        // ENTITY E3


        yield return PlayLine(
            "Sayy..",
            E3
        );









        // GLITCH


        if(
            glitchSFX != null &&
            sfxSource != null
        )
        {
            sfxSource.PlayOneShot(
                glitchSFX
            );
        }






        yield return new WaitForSeconds(2f);




        if(subtitleText != null)
            subtitleText.text = "";




        yield return StartCoroutine(
            FadeOut()
        );




        yield return StartCoroutine(
            ShowTitle()
        );

    }









    IEnumerator ShowTitle()
    {


        if(titleCard != null)
            titleCard.SetActive(true);




        yield return new WaitForSeconds(
            titleDelay
        );





        if(comingSoonCard != null)
            comingSoonCard.SetActive(true);





        yield return new WaitForSeconds(1f);





        if(buttonPanel != null)
            buttonPanel.SetActive(true);

    }









    void PlayWhisper()
    {

        if(
            whisperSFX != null &&
            sfxSource != null
        )
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


        if(subtitleText != null)
            subtitleText.text = subtitle;





        if(
            clip != null &&
            voiceSource != null
        )
        {

            voiceSource.clip = clip;

            voiceSource.Play();



            yield return new WaitWhile(
                ()=> voiceSource.isPlaying
            );

        }

        else
        {

            yield return new WaitForSeconds(2f);

        }





        if(subtitleText != null)
            subtitleText.text = "";

    }









    IEnumerator FadeIn()
    {

        if(blackFade == null)
            yield break;



        Color color = blackFade.color;


        float timer = 0;



        while(timer < 2f)
        {

            timer += Time.deltaTime;


            color.a = Mathf.Lerp(
                1,
                0,
                timer / 2f
            );


            blackFade.color = color;


            yield return null;

        }

    }









    IEnumerator FadeOut()
    {

        if(blackFade == null)
            yield break;



        Color color = blackFade.color;


        float timer = 0;



        while(timer < 2f)
        {

            timer += Time.deltaTime;



            color.a = Mathf.Lerp(
                0,
                1,
                timer / 2f
            );


            blackFade.color = color;


            yield return null;

        }

    }









    IEnumerator FadeInEntity()
    {

        if(entityRenderer == null)
            yield break;



        Color color = entityRenderer.color;


        float timer = 0;



        while(timer < 2f)
        {

            timer += Time.deltaTime;



            color.a = Mathf.Lerp(
                0,
                entityAlpha,
                timer / 2f
            );



            entityRenderer.color = color;



            yield return null;

        }


    }







    // =========================
    // BUTTON FUNCTIONS
    // =========================


    public void PlayAgain()
    {

        Time.timeScale = 1f;


        SceneManager.LoadScene(
            firstLevelScene
        );

    }







    public void MainMenu()
    {

        Time.timeScale = 1f;


        SceneManager.LoadScene(
            mainMenuScene
        );

    }

}