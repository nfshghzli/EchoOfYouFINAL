using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu instance;


    [Header("Panels")]
    public GameObject pausePanel;
    public GameObject settingsPanel;
    public GameObject pauseButton;


    [Header("Background Music")]
    public AudioSource bgmSource;


    private bool isPaused = false;



    void Awake()
    {
        instance = this;

        Time.timeScale = 1f;


        if (pausePanel != null)
            pausePanel.SetActive(false);


        if (settingsPanel != null)
            settingsPanel.SetActive(false);
    }





    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(!isPaused)
            {
                PauseGame();
            }
            else
            {
                if(settingsPanel != null && settingsPanel.activeSelf)
                {
                    CloseSettings();
                }
                else
                {
                    ResumeGame();
                }
            }
        }
    }





    public void PauseGame()
    {
        Debug.Log("GAME PAUSED");


        isPaused = true;



        if(pauseButton != null)
            pauseButton.SetActive(false);



        if(pausePanel != null)
            pausePanel.SetActive(true);



        if(settingsPanel != null)
            settingsPanel.SetActive(false);





        // Pause BGM
        if(bgmSource != null)
        {
            bgmSource.Pause();
        }





        // Pause SFX
        if(AudioManager.instance != null)
        {
            AudioManager.instance.PauseAllAudio();
        }





        Time.timeScale = 0f;
    }







    public void ResumeGame()
    {
        Debug.Log("GAME RESUME");


        isPaused = false;



        if(pausePanel != null)
            pausePanel.SetActive(false);



        if(settingsPanel != null)
            settingsPanel.SetActive(false);



        if(pauseButton != null)
            pauseButton.SetActive(true);





        Time.timeScale = 1f;





        // Resume BGM
        if(bgmSource != null)
        {
            bgmSource.UnPause();
        }





        // Resume SFX
        if(AudioManager.instance != null)
        {
            AudioManager.instance.ResumeAllAudio();
        }

    }







    public void OpenSettings()
    {
        Debug.Log("OPEN SETTINGS");


        if(pausePanel != null)
            pausePanel.SetActive(false);



        if(settingsPanel != null)
            settingsPanel.SetActive(true);
    }







    public void CloseSettings()
    {
        Debug.Log("CLOSE SETTINGS");


        if(settingsPanel != null)
            settingsPanel.SetActive(false);



        if(pausePanel != null)
            pausePanel.SetActive(true);
    }







    public void ReturnToMainMenu()
    {
        Debug.Log("RETURN TO MENU");


        Time.timeScale = 1f;


        SceneManager.LoadScene("MainMenu");
    }
}