using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    public float speed = 5f;

    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);

        // Destroy when off screen
        if (transform.position.x < -20)
        {
            Destroy(gameObject);
        }
    }
}