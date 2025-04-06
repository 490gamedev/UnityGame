using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrabEnemy : MonoBehaviour
{

    public float moveSpeed = 3f; // Speed of enemy movement
    public float moveDuration = 2f; // Time to move in one direction

    private Rigidbody2D rb;
    private bool movingLeft = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(MoveLeftAndRight());
    }

    IEnumerator MoveLeftAndRight()
    {
        while (true) // Infinite loop to keep moving
        {
            if (movingLeft)
            {
                rb.velocity = new Vector2(-moveSpeed, rb.velocity.y); // Move left
            }
            else
            {
                rb.velocity = new Vector2(moveSpeed, rb.velocity.y); // Move right
            }

            yield return new WaitForSeconds(moveDuration); // Wait for 2 seconds

            movingLeft = !movingLeft; // Change direction
        }
    }
}
