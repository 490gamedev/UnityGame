using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 20f;
    private Rigidbody2D body;
    private Animator animator;

    public Transform groundPosition;
    public LayerMask groundLayer;
    private float groundCheckDistance = 0.5f;

    private bool isGrounded;
    private bool jumped;
    public float jumpHeight = 5f;

    private bool isDead = false; // Prevent multiple deaths

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        body.interpolation = RigidbodyInterpolation2D.Interpolate;
    }

    void Update()
    {
        if (!isDead)
        {
            CheckIfGrounded();
            HandleJump();
        }
    }

    void FixedUpdate()
    {
        if (!isDead)
        {
            HandleMovement();
        }
    }

    void HandleMovement()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");

        if (transform.parent != null && transform.parent.CompareTag("MovingPlatform"))
        {
            body.velocity = new Vector2(moveInput * speed, body.velocity.y);
        }
        else
        {
            if (moveInput != 0)
            {
                body.velocity = new Vector2(moveInput * speed, body.velocity.y);
                ChangeDirection(moveInput);
                animator.SetInteger("Speed", Mathf.Abs((int)body.velocity.x));
            }
            else
            {
                body.velocity = new Vector2(0, body.velocity.y);
                animator.SetInteger("Speed", 0);
            }
        }
    }

    void ChangeDirection(float direction)
    {
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x) * Mathf.Sign(direction);
        transform.localScale = scale;
    }

    void CheckIfGrounded()
    {
        isGrounded = Physics2D.Raycast(groundPosition.position, Vector2.down, groundCheckDistance, groundLayer);
        animator.SetBool("Jumpp", !isGrounded);

        if (isGrounded && jumped)
        {
            jumped = false;
        }
    }

    void HandleJump()
    {
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            jumped = true;
            body.velocity = new Vector2(body.velocity.x, jumpHeight);
            animator.SetBool("Jumpp", true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Water"))
        {
            StartCoroutine(FallThroughWater());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Beetle") || collision.gameObject.CompareTag("Snail") || collision.gameObject.CompareTag("Purple"))
        {
            Debug.Log("Touched enemy - damage should be handled by enemy script");
        }
    }

    IEnumerator FallThroughWater()
    {
        isDead = true; // Prevent multiple respawns
        body.velocity = new Vector2(body.velocity.x, -5f); // Simulate sinking
        yield return new WaitForSeconds(0.5f); // Allow falling through water
        Respawn();
    }

    public void KillPlayer()
    {
        if (isDead) return; // Prevent multiple triggers

        isDead = true;
        animator.SetTrigger("DieTrigger");
        body.velocity = Vector2.zero;

        StartCoroutine(RestartGame());
    }

    IEnumerator RestartGame()
    {
        
        yield return new WaitForSecondsRealtime(1f); // Small delay
        //Time.timeScale = 1; // Resume game
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void Respawn()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Restart level
    }
}