using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("Panels")]
    public GameObject settingsPanel;
    public GameObject exitPanel;

    // START GAME
    public void StartGame()
    {
        SceneManager.LoadScene("Level1_Denial");
    }

    // SETTINGS PANEL
    public void OpenSettings()
    {
        settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
    }

    // EXIT PANEL
    public void OpenExitPanel()
    {
        exitPanel.SetActive(true);
    }

    public void CloseExitPanel()
    {
        exitPanel.SetActive(false);
    }

    // QUIT GAME
    public void QuitGame()
    {
        Application.Quit();

        Debug.Log("QUIT GAME");
    }
}
