using UnityEngine;

public class EntityFollow : MonoBehaviour
{
    public Transform player;

    [Header("Movement")]
    public float normalSpeed = 2f;
    public float panicSpeed = 12f;

    [Header("Distance")]
    public float followDistance = 6f;
    public float catchDistance = 0.7f;

    private bool panicMode = false;

    void Update()
    {
        if (player == null)
            return;

        if (!panicMode)
        {
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
            transform.position = Vector3.MoveTowards(
                transform.position,
                player.position,
                panicSpeed * Time.deltaTime
            );

            if (Vector2.Distance(transform.position, player.position) <= catchDistance)
            {
                PlayerController controller = player.GetComponent<PlayerController>();

                if (controller != null)
                    controller.Die();
            }
        }
    }

    public void SetPanic(bool state)
    {
        panicMode = state;
    }
}