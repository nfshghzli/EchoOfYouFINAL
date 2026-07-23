using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCompleteManager : MonoBehaviour
{
    public static LevelCompleteManager instance;


    [Header("UI")]
    public GameObject levelCompletePanel;


    [Header("Next Scene")]
    public string nextSceneName;



    private bool completed = false;



    void Awake()
    {
        instance = this;
    }



    void Start()
    {
        if(levelCompletePanel != null)
        {
            levelCompletePanel.SetActive(false);
        }
    }




    public void CompleteLevel()
    {
        if(completed)
            return;


        completed = true;


        Time.timeScale = 0f;


        if(levelCompletePanel != null)
        {
            levelCompletePanel.SetActive(true);
        }
    }





    public void Continue()
    {
        Time.timeScale = 1f;


        SceneManager.LoadScene(nextSceneName);
    }
}