using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float forwardSpeed = 5f;
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



    [Header("Health")]
    public int hp = 3;



    [Header("Wall Hit")]
    public float wallPushDistance = 1.5f;
    public float wallPushDuration = 0.3f;

    private bool wallHitCooldown;



    private Rigidbody2D rb;
    private Animator animator;


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



            rb.linearVelocity = new Vector2(
                rb.linearVelocity.x,
                jumpForce
            );


            jumpsLeft--;


            Debug.Log("JUMP");
        }



        // SLIDE

        if(
            Input.GetKey(KeyCode.S) ||
            Input.GetKey(KeyCode.DownArrow)
        )
        {
            StartSlide();
        }
        else
        {
            StopSlide();
        }

    }




    void SavePreviousPosition()
    {
        saveTimer += Time.deltaTime;


        if(saveTimer >= rewindTime)
        {
            lastSafePosition = transform.position;
            saveTimer = 0f;
        }
    }





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
    {
        if(!isSliding)
            return;


        isSliding = false;


        StartRunningSFX();


        if(animator != null)
            animator.SetBool("IsSliding",false);
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


        isDead=true;


        if(AudioManager.instance != null)
            AudioManager.instance.PlayDeath();



        rb.linearVelocity=Vector2.zero;


        rb.bodyType=RigidbodyType2D.Kinematic;



        StopRunningSFX();



        if(CameraShake.instance != null)
            CameraShake.instance.Shake(0.4f,0.5f);




        PlayerPrefs.SetString(
            "LastLevel",
            SceneManager.GetActiveScene().name
        );



        if(ScreenFade.instance != null)
        {
            StartCoroutine(
                ScreenFade.instance.PlayDeathSequence()
            );
        }

    }







    private void OnDrawGizmosSelected()
    {

        if(groundCheck == null)
            return;


        Gizmos.color = Color.green;


        Gizmos.DrawWireSphere(
            groundCheck.position,
            groundCheckRadius
        );

    }

}