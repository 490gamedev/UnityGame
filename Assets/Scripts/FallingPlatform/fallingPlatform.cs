using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fallingPlatform : MonoBehaviour
{

    private Rigidbody2D rb;
    private bool isFalling = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic; // Prevent falling initially
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isFalling)
        {
            isFalling = true;
            StartCoroutine(Fall());
        }
    }

    IEnumerator Fall()
    {
        yield return new WaitForSeconds(0f); // Wait 1 second before falling
        rb.bodyType = RigidbodyType2D.Dynamic; // Enable physics
        rb.gravityScale = 2f; // Start falling
        yield return new WaitForSeconds(3f); // Wait before destroying
        Destroy(gameObject); // Remove platform
    }
}
