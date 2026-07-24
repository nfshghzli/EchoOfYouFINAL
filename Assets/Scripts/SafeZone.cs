using System.Collections;
using UnityEngine;

public class SafeZone : MonoBehaviour
{

    private bool triggered = false;



    private void OnTriggerEnter2D(Collider2D collision)
    {

        if(collision.CompareTag("Player") && !triggered)
        {

            triggered = true;

            StartCoroutine(EndLevel());

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



        // Give player a short relief moment
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

        }

    }

}