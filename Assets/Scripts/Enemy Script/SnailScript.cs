using UnityEngine;
using System.Collections;

public class SnailScript : MonoBehaviour
{
    public float speed = 3f;
    private Rigidbody2D myBody;
    private Animator anim;

    private bool moveLeft;
    private bool canMove = true;
    private bool stunned = false;

    public LayerMask playerLayer;
    public Transform left_Collision, right_Collision, top_Collision, down_Collision;
    private Vector3 left_Collision_Pos, right_Collision_Pos;

    void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        left_Collision_Pos = left_Collision.position;
        right_Collision_Pos = right_Collision.position;
    }

    void Start()
    {
        moveLeft = true;
    }

    void Update()
    {
        Debug.Log("Snail Update is running!");

        if (canMove)
        {
            myBody.velocity = new Vector2(moveLeft ? -speed : speed, myBody.velocity.y);
        }

        CheckCollision();
    }
    
     void CheckCollision()
     {
         Collider2D topHit = Physics2D.OverlapCircle(top_Collision.position, 0.3f, playerLayer);
         Collider2D playerSideCollision = Physics2D.OverlapCircle(left_Collision.position, 0.2f, playerLayer) ??
      Physics2D.OverlapCircle(right_Collision.position, 0.2f, playerLayer);

         if (topHit != null && topHit.CompareTag("Player"))
         {
             if (!stunned)
             {
                 Rigidbody2D playerRb = topHit.GetComponent<Rigidbody2D>();
                 playerRb.velocity = new Vector2(playerRb.velocity.x, 7f);

                 canMove = false;
                 myBody.velocity = Vector2.zero;
                 anim.Play("Stunnedd");
                 stunned = true;

                 StartCoroutine(DestroyAfterStun());
             }
         }

         if (playerSideCollision != null && playerSideCollision.CompareTag("Player"))
         {
             PlayerDamage playerDamage = playerSideCollision.GetComponent<PlayerDamage>();
             playerDamage?.DealDamage();
         }

         int groundLayer = LayerMask.GetMask("Ground");
         if (!Physics2D.Raycast(down_Collision.position, Vector2.down, 0.5f, groundLayer))
         {
             ReverseDirection();
         }
     }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        // Make sure the other object has the SnailScript
        if (collision.gameObject.TryGetComponent<SnailScript>(out var otherSnail))
        {
            // Avoid double-reversing by only reversing if this object's instance ID is smaller (or use any condition)
            if (this.GetInstanceID() < otherSnail.GetInstanceID())
            {
                ReverseDirection();
                otherSnail.ReverseDirection();
            }
        }

        // Check for Trampoline or Log
        string otherTag = collision.gameObject.tag;
        if (otherTag == "Trampoline" || otherTag == "Log")
        {
            ReverseDirection();
        }

    }

    IEnumerator DestroyAfterStun()
    {
        yield return new WaitForSeconds(0.2f);
        gameObject.SetActive(false);
    }

    public void ReverseDirection()
    {
        moveLeft = !moveLeft;
        myBody.velocity = new Vector2(-myBody.velocity.x, myBody.velocity.y);

        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}