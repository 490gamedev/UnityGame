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

    

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        body.interpolation = RigidbodyInterpolation2D.Interpolate;
    }

    void Update()
    {
        CheckIfGrounded();
        HandleJump();
    }

    void FixedUpdate()
    {
        HandleMovement();
    }

    void HandleMovement()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");

        // Check if the player is parented to a platform
        if (transform.parent != null && transform.parent.CompareTag("MovingPlatform"))
        {
            // Allow the platform to move the player
            body.velocity = new Vector2(moveInput * speed, body.velocity.y);
        }
        else
        {
            // Normal movement when not on a platform
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
        if (collider.CompareTag("Water") || collider.CompareTag("SharpArrow"))
        {
            animator.SetTrigger("DieTrigger");
            StartCoroutine(Respawn());
        }
    }

    private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

/*public class PlayerMovement : MonoBehaviour
{
    public float speed = 20f;
    private Rigidbody2D body;
    private Animator animator;

    public Transform groundPosition;
    public LayerMask groundLayer;
    private float groundCheckDistance = 0.1f;

    private bool isGrounded;
    private bool jumped;
    public float jumpHeight = 5f;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        body.interpolation = RigidbodyInterpolation2D.Interpolate;
    }

    void Update()
    {
        CheckIfGrounded();
        HandleJump();
    }

    void FixedUpdate()
    {
        HandleMovement();
    }

    void HandleMovement()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");

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
        if (collider.CompareTag("Water") || collider.CompareTag("SharpArrow"))
        {
            animator.SetTrigger("DieTrigger");
            StartCoroutine(Respawn());
        }
    }

    private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}*/
