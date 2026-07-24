using System.Collections;
using UnityEngine;

public class SafeZone : MonoBehaviour
{
    private bool triggered = false;


    private bool triggered = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
<<<<<<< HEAD
        if(collision.CompareTag("Player") && !triggered)
=======
        if (
            collision.CompareTag("Player")
            && !triggered
        )
>>>>>>> b47d0c4f3253e44916ff3d2e0af2482b73a1aa6c
        {
            triggered = true;

            StartCoroutine(EndLevel());
<<<<<<< HEAD
        }
    }



    IEnumerator EndLevel()
    {
        PlayerController player =
            FindObjectOfType<PlayerController>();


        // Stop Sayy movement
        if(player != null)
        {
            player.StopRunning();
        }



        // Give moment of relief
        yield return new WaitForSeconds(1.5f);



        // Open Level Complete Screen
        if(LevelCompleteManager.instance != null)
        {
            LevelCompleteManager.instance.CompleteLevel();
        }
        else
        {
            Debug.LogError(
                "LevelCompleteManager not found!"
            );
=======
>>>>>>> b47d0c4f3253e44916ff3d2e0af2482b73a1aa6c
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