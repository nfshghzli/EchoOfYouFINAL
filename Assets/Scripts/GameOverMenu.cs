using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public void RetryLevel()
    {
        string lastLevel =
            PlayerPrefs.GetString(
                "LastLevel",
                "Level1_Denial"
            );

        SceneManager.LoadScene(lastLevel);
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}