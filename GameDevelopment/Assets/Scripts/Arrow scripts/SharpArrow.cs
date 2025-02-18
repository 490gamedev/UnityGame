using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharpArrow : MonoBehaviour
{
    public float fallSpeed = 5f;       // Speed of the arrow falling down
    public float riseSpeed = 5f;      // Speed of the arrow rising back up
    public float pauseTime = 1f;      // Time the arrow pauses on the ground
    public float groundY = -3f;       // Y position of the ground

    private Vector3 startPosition;    // Starting position of the arrow
    private bool isFalling = true;    // Is the arrow currently falling?
    private bool isRising = false;    // Is the arrow currently rising?
    private float pauseTimer;         // Timer for the pause at the ground

    void Start()
    {
        // Save the initial position
        startPosition = transform.position;
        pauseTimer = pauseTime;
    }

    void Update()
    {
        if (isFalling)
        {
            // Arrow falls downward
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, groundY, transform.position.z), fallSpeed * Time.deltaTime);

            // Check if the arrow reached the ground
            if (Mathf.Abs(transform.position.y - groundY) < 0.01f)
            {
                isFalling = false; // Stop falling
                pauseTimer = pauseTime; // Reset pause timer
            }
        }
        else if (!isRising)
        {
            // Pause on the ground
            pauseTimer -= Time.deltaTime;
            if (pauseTimer <= 0)
            {
                isRising = true; // Start rising back up
            }
        }
        else if (isRising)
        {
            // Arrow rises back to its starting position
            transform.position = Vector3.MoveTowards(transform.position, startPosition, riseSpeed * Time.deltaTime);

            // Check if the arrow reached the starting position
            if (Mathf.Abs(transform.position.y - startPosition.y) < 0.01f)
            {
                isRising = false; // Stop rising
                isFalling = true; // Reset to falling
            }
        }
    }
}
