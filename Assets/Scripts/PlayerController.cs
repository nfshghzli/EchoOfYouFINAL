using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float forwardSpeed = 5f;
    public float jumpForce = 18f;

    [Header("Health")]
    public int hp = 3;

    [Header("Turning")]
    public float turnSpeed = 120f;

    private Rigidbody2D rb;
    private Animator animator;

    private bool isGrounded;
    private bool isSliding;
    private bool isTurning;
    private bool isDead = false;
    private bool canMove = true;

    private void Start()
    {
        Time.timeScale = 1f;

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        UIManager.instance.UpdateHP(hp);

        animator.SetBool("IsRunning", true);
        animator.SetBool("IsJumping", false);
        animator.SetBool("IsSliding", false);
    }

    private void Update()
    {
            Debug.Log("UPDATE RUNNING");

            if (!canMove || isDead)
                return;

        if (Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log("W DETECTED");
        }

        if (!canMove || isDead)
            return;

        // Auto Run
        rb.linearVelocity = new Vector2(
            forwardSpeed,
            rb.linearVelocity.y
        );

        // Jump
        if (
            (Input.GetKeyDown(KeyCode.W) ||
             Input.GetKeyDown(KeyCode.UpArrow))
            &&
            !isSliding
        )
        {
            rb.linearVelocity = new Vector2(
                rb.linearVelocity.x,
                jumpForce
            );

            isGrounded = false;

            animator.SetBool("IsJumping", true);
        }

        // Slide Start
        if (
            Input.GetKeyDown(KeyCode.S) ||
            Input.GetKeyDown(KeyCode.DownArrow)
        )
        {
            StartSlide();
        }

        // Slide End
        if (
            Input.GetKeyUp(KeyCode.S) ||
            Input.GetKeyUp(KeyCode.DownArrow)
        )
        {
            StopSlide();
        }

        // Turning
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

    private void StartSlide()
    {
        Debug.Log("SLIDE START");

        if (isDead)
            return;

        isSliding = true;

        animator.SetBool("IsRunning", false);
        animator.SetBool("IsSliding", true);
    }

    private void StopSlide()
    {
        isSliding = false;

        animator.SetBool("IsSliding", false);
        animator.SetBool("IsRunning", true);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("GROUND DETECTED");

            isGrounded = true;

            animator.SetBool("IsJumping", false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDead)
            return;

        if (collision.gameObject.CompareTag("Obstacle"))
        {
            hp--;
            hp = Mathf.Max(hp, 0);

            ScreenEffects.instance.Flash();
            AudioManager.instance.PlayHit();
            CameraShake.instance.Shake(0.15f, 0.15f);

            UIManager.instance.UpdateHP(hp);

            Destroy(collision.gameObject);

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

        rb.linearVelocity = Vector2.zero;
        rb.bodyType = RigidbodyType2D.Kinematic;

        animator.SetBool("IsRunning", false);

        CameraShake.instance.Shake(
            0.4f,
            0.5f
        );

        PlayerPrefs.SetString(
            "LastLevel",
            SceneManager.GetActiveScene().name
        );

        StartCoroutine(
            ScreenFade.instance.PlayDeathSequence()
        );
    }

    public void StartTurning()
    {
        if (isDead) return;

        animator.Play("PlayerTurn");
    }

    public void StopTurning()
    {
        if (isDead) return;

        animator.Play("PlayerRun");
    }

    public void StopRunning()
    {
        canMove = false;

        rb.linearVelocity = Vector2.zero;

        animator.SetBool(
            "IsRunning",
            false
        );
    }
}
