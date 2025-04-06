using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    public GameObject stone;
    public Transform attackInstantiate;

   
    private string coroutine_Name = "StartAttack";

    private Animator anim;
    public Transform player;

    private bool isPlayerInRange;
    private float attackDistance = 28f; // Boss starts attacking when player is within 10 units


    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        //this would start the animation for the attack 
        StartCoroutine(coroutine_Name);
    }

    private void Update()
    {
        if (player != null)
        {
            // Check if player is close enough to attack
            float distance = Vector2.Distance(transform.position, player.position);
            isPlayerInRange = distance <= attackDistance;
        }
    }

    //we want to use the instantiate postion using QUATERNION.identity
    void Attack()
    {
        GameObject obj = Instantiate(stone, attackInstantiate.position, Quaternion.identity);
        Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();

        // Calculate direction toward the player
        Vector2 direction = (player.position - attackInstantiate.position).normalized;

        // Launch the stone
        float throwSpeed = 14f;
        rb.velocity = direction * throwSpeed;

       
    }

    //create 2 different animation events
    void BackToIdle()
    {
        anim.Play("BossIdle");
    }


    IEnumerator StartAttack()
    {
        while (true)
        {
            //wait about 2 to 5 seconds before attacking
            yield return new WaitForSeconds(Random.Range(1f, 1.2f));
            // Only attack if player is close enough
            if (isPlayerInRange)
            {
                anim.Play("BossAttack");
            }
        }
    }

}
