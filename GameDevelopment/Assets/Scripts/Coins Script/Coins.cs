using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Coin : MonoBehaviour
{
    // Trigger method for when the player touches the coin
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the collider belongs to the player
        if (collision.CompareTag("Player"))
        {
            // You can add logic here for scoring or sound effects
            Debug.Log("Coin collected!");

            // Destroy the coin game object
            Destroy(gameObject);
        }
    }

}
