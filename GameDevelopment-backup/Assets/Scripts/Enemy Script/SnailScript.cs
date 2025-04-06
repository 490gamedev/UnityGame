using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        if (canMove)
        {
            myBody.velocity = new Vector2(moveLeft ? -speed : speed, myBody.velocity.y);
        }

        CheckCollision();
    }
    

    void CheckCollision()
    {
        // Top Collision Detection (Kill Enemy)
        Collider2D topHit = Physics2D.OverlapCircle(top_Collision.position, 0.3f, playerLayer);

        // Side Collision Detection (Kill Player)
        Collider2D playerSideCollision = Physics2D.OverlapCircle(left_Collision.position, 0.2f, playerLayer) ??
                                         Physics2D.OverlapCircle(right_Collision.position, 0.2f, playerLayer);

        // Kill Enemy if Player Lands on Top
        if (topHit != null && topHit.CompareTag("Player"))
        {
            if (!stunned)
            {
                Rigidbody2D playerRb = topHit.GetComponent<Rigidbody2D>();
                playerRb.velocity = new Vector2(playerRb.velocity.x, 7f); // Player bounces up

                canMove = false;
                myBody.velocity = Vector2.zero;
                anim.Play("Stunnedd"); // Play death animation
                stunned = true;

                StartCoroutine(DestroyAfterStun());
            }
        }
        // Apply Damage to Player when touched from the side -> MODIFIED
        if (playerSideCollision != null && playerSideCollision.CompareTag("Player"))
        {
            PlayerDamage playerDamage = playerSideCollision.GetComponent<PlayerDamage>();
            if (playerDamage != null) // Ensure the player has a PlayerDamage script
            {
                playerDamage.DealDamage(); // Apply damage to the player
            }
        }

        int groundLayer = LayerMask.GetMask("Ground");

        // Reverse direction if no ground ahead
        if (!Physics2D.Raycast(down_Collision.position, Vector2.down, 0.5f, groundLayer))
        {
            ReverseDirection();
        }
    }



    /* Kill Player if Touched from Side
     if (playerSideCollision != null && playerSideCollision.CompareTag("Player"))
     {
         PlayerDamage playerDamage = playerSideCollision.GetComponent<PlayerDamage>();
         playerDamage?.KillPlayer();
     }

     int groundLayer = LayerMask.GetMask("Ground");

     // Reverse direction if no ground ahead
     if (!Physics2D.Raycast(down_Collision.position, Vector2.down, 0.5f, groundLayer))
     {
         ReverseDirection();
     }
 }
*/

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collided with: " + collision.gameObject.tag);

        // Reverse direction when colliding with another enemy
        if ((collision.gameObject.CompareTag("Beetle") && gameObject.CompareTag("Snail")) ||
          (collision.gameObject.CompareTag("Snail") && collision.gameObject.CompareTag("Beetle")))

        {
            ReverseDirection();
            collision.gameObject.GetComponent<SnailScript>()?.ReverseDirection();
        }

        // Reverse direction when hitting obstacles
        if (collision.gameObject.CompareTag("Trampoline") || collision.gameObject.CompareTag("Log"))
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

         // Flip sprite to match movement direction
         Vector3 scale = transform.localScale;
         scale.x *= -1;
         transform.localScale = scale;
     }
    
}

