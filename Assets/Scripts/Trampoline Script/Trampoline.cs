using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour
{
    public float bounceForce = 10f; // Force applied to the player when bouncing
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the colliding object is the player
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();

            if (playerRb != null)
            {
                // Apply bounce force to the player
                playerRb.velocity = new Vector2(playerRb.velocity.x, bounceForce);

                // Trigger the trampoline animation
                animator.SetTrigger("Bounce");
            }
        }
    }

}
