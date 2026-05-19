using UnityEngine;

public class EntityFollow : MonoBehaviour
{
    public Transform player;

    [Header("Movement")]
    public float normalSpeed = 2f;
    public float panicSpeed = 8f;

    [Header("Distance")]
    public float followDistance = 6f;
    public float catchDistance = 1.2f;

    private bool panicMode = false;

    void Update()
    {
        if (!panicMode)
        {
            // Stay behind player normally
            Vector3 targetPos = new Vector3(
                player.position.x - followDistance,
                transform.position.y,
                transform.position.z
            );

            transform.position = Vector3.Lerp(
                transform.position,
                targetPos,
                normalSpeed * Time.deltaTime
            );
        }
        else
        {
            // PANIC CHASE
            transform.position = Vector3.MoveTowards(
                transform.position,
                player.position,
                panicSpeed * Time.deltaTime
            );

            // Check catch distance
            float distance = Vector3.Distance(
                transform.position,
                player.position
            );

            if (distance <= catchDistance)
            {
                player.GetComponent<PlayerController>().Die();
            }
        }
    }

    public void SetPanic(bool state)
    {
        panicMode = state;
    }
}