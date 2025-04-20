using UnityEngine;

public class PlayerShot : MonoBehaviour
{
    public GameObject bulletPrefab; // assign the fire bullet prefab
    public Transform firePoint;     // where the bullet spawns from
    public float bulletSpeed = 10f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F)) // fire when pressing F (can change)
        {
            Fire();
        }
    }

    void Fire()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = transform.localScale.x * Vector2.right * bulletSpeed; // direction based on player facing
    }
}
