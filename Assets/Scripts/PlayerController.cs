using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]

    public float forwardSpeed = 5f;
    public float runSpeed = 5f;
    public float jumpForce = 12f;
    public int maxJumps = 2;
    private int jumpsLeft;

    [Header("Health")]
    public int hp = 3;

    [Header("Turning")]
    public float turnSpeed = 120f;

    private Rigidbody2D rb;

    private bool isGrounded;
    private bool isSliding;
    private bool isTurning;
    private bool isDead = false;

    private Vector3 originalScale;

    void Start()
    {
        Time.timeScale = 1f;

        rb = GetComponent<Rigidbody2D>();

        originalScale = transform.localScale;

        jumpsLeft = maxJumps;
    }

    void Update()
    {
        transform.position += Vector3.right
            * forwardSpeed
            * Time.deltaTime;

        // STOP EVERYTHING IF DEAD
        if (isDead)
            return;

        // JUMP
        if (Input.GetKeyDown(KeyCode.W)
            && jumpsLeft > 0
            && !isSliding)
        {
            rb.linearVelocity =
                new Vector2(
                    rb.linearVelocity.x,
                    jumpForce
                );

            jumpsLeft--;
        }

        // SLIDE START
        if (Input.GetKeyDown(KeyCode.S))
        {
            StartSlide();
        }

        // SLIDE END
        if (Input.GetKeyUp(KeyCode.S))
        {
            StopSlide();
        }

        // TURNING EFFECT
        if (isTurning)
        {
            transform.rotation =
                Quaternion.RotateTowards(
                    transform.rotation,
                    Quaternion.Euler(0, 180, 0),
                    turnSpeed * Time.deltaTime
                );
        }
    }

    void StartSlide()
    {
        isSliding = true;

        transform.localScale =
            new Vector3(
                originalScale.x,
                originalScale.y / 2,
                originalScale.z
            );
    }

    void StopSlide()
    {
        isSliding = false;

        transform.localScale = originalScale;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // IGNORE COLLISION IF DEAD
        if (isDead)
            return;

        // GROUND CHECK
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;

            jumpsLeft = maxJumps;
        }

        // OBSTACLE HIT
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            hp--;

            // PREVENT NEGATIVE HP
            hp = Mathf.Max(hp, 0);

            // EFFECTS
            ScreenEffects.instance.Flash();

            AudioManager.instance.PlayHit();

            CameraShake.instance.Shake(0.15f, 0.15f);

            UIManager.instance.UpdateHP(hp);

            Debug.Log("HP: " + hp);

            Destroy(collision.gameObject);

            // GAME OVER
            if (hp <= 0)
            {
                Die();
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    public void Die()
    {
        if (isDead)
            return;

        isDead = true;

        // RESET TIME
        Time.timeScale = 1f;

        // STOP MOVEMENT
        rb.linearVelocity = Vector2.zero;

        // STOP PHYSICS
        rb.bodyType = RigidbodyType2D.Kinematic;

        // EFFECT
        CameraShake.instance.Shake(0.4f, 0.5f);

        Debug.Log("GAME OVER");

        PlayerPrefs.SetString(
            "LastLevel",
            SceneManager.GetActiveScene().name
        );
        // LOAD GAME OVER
        Invoke(nameof(LoadGameOverScene), 1.5f);
    }

    void LoadGameOverScene()
    {
        SceneManager.LoadScene("GameOver");
    }

    public void StartTurning()
    {
        if (isDead)
            return;

        isTurning = true;
    }

    public void StopTurning()
    {
        if (isDead)
            return;

        isTurning = false;

        transform.rotation =
            Quaternion.Euler(0, 0, 0);
    }
}