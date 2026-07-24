using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float forwardSpeed = 5f;
<<<<<<< HEAD
    public float jumpForce = 12f;
    public int maxJumps = 2;
    public float maxHeight = 4f;


    [Header("Ground Check")]
    public Transform groundCheck;
    public LayerMask groundLayer;
    public float groundCheckRadius = 0.5f;


    [Header("Fall Rewind")]
    public float rewindTime = 5f;

    private Vector3 lastSafePosition;
    private float saveTimer;

    private bool isRewinding;
    private bool invincible;


=======
    public float jumpForce = 18f;
>>>>>>> b47d0c4f3253e44916ff3d2e0af2482b73a1aa6c

    [Header("Health")]
    public int hp = 3;



    [Header("Wall Hit")]
    public float wallPushDistance = 1.5f;
    public float wallPushDuration = 0.3f;

    private bool wallHitCooldown;



    private Rigidbody2D rb;
    private Animator animator;
<<<<<<< HEAD


    private bool isGrounded;
    private bool isSliding;
    private bool isDead;
    private bool canMove = true;


    private int jumpsLeft;



    void Start()
    {
        Time.timeScale = 1f;


        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        jumpsLeft = maxJumps;


        lastSafePosition = transform.position;


        if(UIManager.instance != null)
            UIManager.instance.UpdateHP(hp);


        if(animator != null)
            animator.SetBool("IsRunning", true);


        StartRunningSFX();
    }



    void Update()
    {

        if(!canMove || isDead)
            return;



        SavePreviousPosition();



        // AUTO RUN
        rb.linearVelocity = new Vector2(
            forwardSpeed,
            rb.linearVelocity.y
        );



        // GROUND CHECK

        isGrounded = Physics2D.OverlapCircle(
            groundCheck.position,
            groundCheckRadius,
            groundLayer
        );



        if(isGrounded)
        {
            jumpsLeft = maxJumps;
            StartRunningSFX();
        }



        if(animator != null)
        {
            animator.SetBool(
                "IsJumping",
                Mathf.Abs(rb.linearVelocity.y) > 0.1f
            );
        }



        // JUMP

        if(
            (Input.GetKeyDown(KeyCode.W) ||
            Input.GetKeyDown(KeyCode.UpArrow))
            &&
            jumpsLeft > 0
            &&
            !isSliding
            &&
            transform.position.y < maxHeight
        )
        {

            StopRunningSFX();


            if(AudioManager.instance != null)
                AudioManager.instance.PlayJump();



=======

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
>>>>>>> b47d0c4f3253e44916ff3d2e0af2482b73a1aa6c
            rb.linearVelocity = new Vector2(
                rb.linearVelocity.x,
                jumpForce
            );

<<<<<<< HEAD

            jumpsLeft--;


            Debug.Log("JUMP");
        }



        // SLIDE

        if(
            Input.GetKey(KeyCode.S) ||
            Input.GetKey(KeyCode.DownArrow)
=======
            isGrounded = false;

            animator.SetBool("IsJumping", true);
        }

        // Slide Start
        if (
            Input.GetKeyDown(KeyCode.S) ||
            Input.GetKeyDown(KeyCode.DownArrow)
>>>>>>> b47d0c4f3253e44916ff3d2e0af2482b73a1aa6c
        )
        {
            StartSlide();
        }
<<<<<<< HEAD
        else
=======

        // Slide End
        if (
            Input.GetKeyUp(KeyCode.S) ||
            Input.GetKeyUp(KeyCode.DownArrow)
        )
>>>>>>> b47d0c4f3253e44916ff3d2e0af2482b73a1aa6c
        {
            StopSlide();
        }

<<<<<<< HEAD
    }




    void SavePreviousPosition()
    {
        saveTimer += Time.deltaTime;


        if(saveTimer >= rewindTime)
=======
        // Turning
        if (isTurning)
>>>>>>> b47d0c4f3253e44916ff3d2e0af2482b73a1aa6c
        {
            lastSafePosition = transform.position;
            saveTimer = 0f;
        }
    }

<<<<<<< HEAD




    void StartSlide()
    {
        if(isSliding || isDead)
            return;


        isSliding = true;


        StopRunningSFX();


        if(AudioManager.instance != null)
            AudioManager.instance.PlaySlide();


        if(animator != null)
            animator.SetBool("IsSliding",true);
    }




    void StopSlide()
=======
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
>>>>>>> b47d0c4f3253e44916ff3d2e0af2482b73a1aa6c
    {
        if(!isSliding)
            return;


        isSliding = false;

<<<<<<< HEAD

        StartRunningSFX();


        if(animator != null)
            animator.SetBool("IsSliding",false);
=======
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
>>>>>>> b47d0c4f3253e44916ff3d2e0af2482b73a1aa6c
    }






    void StartRunningSFX()
    {
        if(AudioManager.instance != null)
            AudioManager.instance.StartRunning();
    }



    void StopRunningSFX()
    {
        if(AudioManager.instance != null)
            AudioManager.instance.StopRunning();
    }





    // FALL FROM BUILDING ONLY

    public void PlayerFall()
    {

        if(
            isDead ||
            isRewinding ||
            invincible
        )
            return;



        hp--;


        if(UIManager.instance != null)
            UIManager.instance.UpdateHP(hp);



        if(hp <= 0)
        {
            Die();
            return;
        }


        StartCoroutine(RewindPlayer());

    }





    IEnumerator RewindPlayer()
    {

        isRewinding = true;
        invincible = true;


        StopRunningSFX();


        rb.linearVelocity = Vector2.zero;


        transform.position = lastSafePosition;



        yield return new WaitForSeconds(0.5f);



        StartRunningSFX();



        yield return new WaitForSeconds(2f);



        invincible = false;
        isRewinding = false;

    }






    private void OnCollisionEnter2D(Collision2D collision)
    {
<<<<<<< HEAD

        if(isDead)
            return;



        // WALL

        if(collision.gameObject.CompareTag("Wall"))
        {

            if(wallHitCooldown)
                return;


            wallHitCooldown = true;


            TakeDamage();



            StartCoroutine(WallPush());


            StartCoroutine(WallCooldown());

            return;
        }





        // NORMAL OBSTACLE

        if(collision.gameObject.CompareTag("Obstacle"))
        {

            TakeDamage();


            Destroy(collision.gameObject);


=======
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
>>>>>>> b47d0c4f3253e44916ff3d2e0af2482b73a1aa6c
        }

    }






    void TakeDamage()
    {

        hp--;

        hp = Mathf.Max(hp,0);



        if(ScreenEffects.instance != null)
            ScreenEffects.instance.Flash();



        if(AudioManager.instance != null)
            AudioManager.instance.PlayHit();



        if(CameraShake.instance != null)
            CameraShake.instance.Shake(0.15f,0.15f);



        if(UIManager.instance != null)
            UIManager.instance.UpdateHP(hp);



        if(hp <= 0)
            Die();

    }







    IEnumerator WallPush()
    {

        canMove = false;


        float timer = 0;


        while(timer < wallPushDuration)
        {

            transform.position += 
                Vector3.left *
                (wallPushDistance / wallPushDuration)
                *
                Time.deltaTime;


            timer += Time.deltaTime;


            yield return null;
        }



        canMove = true;

    }






    IEnumerator WallCooldown()
    {

        yield return new WaitForSeconds(1f);


        wallHitCooldown = false;

    }







    public void StartTurning()
    {
        if(isDead)
            return;


        if(animator != null)
            animator.SetBool("IsTurning",true);
    }



    public void StopTurning()
    {
        if(isDead)
            return;


        if(animator != null)
            animator.SetBool("IsTurning",false);
    }





    public void StopRunning()
    {

        canMove=false;


        rb.linearVelocity=Vector2.zero;


        StopRunningSFX();


        if(animator != null)
            animator.SetBool("IsRunning",false);

    }






    public void Die()
    {

        if(isDead)
            return;


<<<<<<< HEAD
        isDead=true;


        if(AudioManager.instance != null)
            AudioManager.instance.PlayDeath();



        rb.linearVelocity=Vector2.zero;


        rb.bodyType=RigidbodyType2D.Kinematic;



        StopRunningSFX();



        if(CameraShake.instance != null)
            CameraShake.instance.Shake(0.4f,0.5f);



=======
        rb.linearVelocity = Vector2.zero;
        rb.bodyType = RigidbodyType2D.Kinematic;

        animator.SetBool("IsRunning", false);

        CameraShake.instance.Shake(
            0.4f,
            0.5f
        );
>>>>>>> b47d0c4f3253e44916ff3d2e0af2482b73a1aa6c

        PlayerPrefs.SetString(
            "LastLevel",
            SceneManager.GetActiveScene().name
        );
<<<<<<< HEAD



        if(ScreenFade.instance != null)
        {
            StartCoroutine(
                ScreenFade.instance.PlayDeathSequence()
            );
        }

    }

=======

        StartCoroutine(
            ScreenFade.instance.PlayDeathSequence()
        );
    }
>>>>>>> b47d0c4f3253e44916ff3d2e0af2482b73a1aa6c






    private void OnDrawGizmosSelected()
    {
<<<<<<< HEAD

        if(groundCheck == null)
            return;


        Gizmos.color = Color.green;


        Gizmos.DrawWireSphere(
            groundCheck.position,
            groundCheckRadius
        );

    }

}
=======
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
>>>>>>> b47d0c4f3253e44916ff3d2e0af2482b73a1aa6c
