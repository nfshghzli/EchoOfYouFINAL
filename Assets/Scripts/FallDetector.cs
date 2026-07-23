using UnityEngine;

public class FallDetector : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("SOMETHING ENTERED FALL DETECTOR: " + collision.name);


        if(collision.CompareTag("Player"))
        {
            Debug.Log("PLAYER DETECTED!");

            PlayerController player =
                collision.GetComponent<PlayerController>();

            if(player != null)
            {
                Debug.Log("CALLING PLAYER FALL");

                player.PlayerFall();
            }
            else
            {
                Debug.LogError("PLAYER CONTROLLER NOT FOUND!");
            }
        }
    }
}