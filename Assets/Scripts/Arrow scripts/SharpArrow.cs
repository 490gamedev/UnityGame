using UnityEngine;

public class SharpArrow : MonoBehaviour
{
    public float fallSpeed = 5f;
    public float riseSpeed = 5f;
    public float pauseTime = 1f;
    public float groundY = -3f;

    private Vector3 startPosition;
    private bool isFalling = true;
    private bool isRising = false;
    private float pauseTimer;

    void Start()
    {
        startPosition = transform.position;
        pauseTimer = pauseTime;
    }

    void Update()
    {
        if (isFalling)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, groundY, transform.position.z), fallSpeed * Time.deltaTime);

            if (Mathf.Abs(transform.position.y - groundY) < 0.01f)
            {
                isFalling = false;
                pauseTimer = pauseTime;
            }
        }
        else if (!isRising)
        {
            pauseTimer -= Time.deltaTime;
            if (pauseTimer <= 0)
            {
                isRising = true;
            }
        }
        else if (isRising)
        {
            transform.position = Vector3.MoveTowards(transform.position, startPosition, riseSpeed * Time.deltaTime);

            if (Mathf.Abs(transform.position.y - startPosition.y) < 0.01f)
            {
                isRising = false;
                isFalling = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            PlayerDamage damage = collider.GetComponent<PlayerDamage>();
            if (damage != null)
            {
                damage.DealDamage();
            }
        }
    }
}
