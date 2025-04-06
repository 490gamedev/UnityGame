using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // The player or target to follow
    public float smoothSpeed = 0.125f; // Adjust smoothness (lower value = slower)
    public float fixedYPosition = 0f; // Fixed Y position for the camera
    private Vector3 offset; // Offset to maintain initial distance from the player

    private void Start()
    {
        if (target == null)
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
            if (playerObject != null)
            {
                target = playerObject.transform;
            }
            else
            {
                Debug.LogError("Player with tag 'Player' not found.");
                return;
            }
        }

        // Calculate initial offset based on target's position
        offset = transform.position - target.position;
    }

    void LateUpdate()
    {
        if (target != null)
        {
            Vector3 targetPosition = new Vector3(target.position.x + offset.x, fixedYPosition, target.position.z + offset.z);
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);
        }
    }
}
