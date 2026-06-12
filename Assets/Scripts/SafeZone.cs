using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SafeZone : MonoBehaviour
{
    public string nextScene;

    private bool triggered = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (
            collision.CompareTag("Player")
            && !triggered
        )
        {
            triggered = true;

            StartCoroutine(EndLevel());
        }
    }

    IEnumerator EndLevel()
    {
        PlayerController player =
            FindObjectOfType<PlayerController>();

        player.StopRunning();

        yield return new WaitForSeconds(1.5f);

        FindObjectOfType<LevelTransition>()
            .StartTransition();
    }
}