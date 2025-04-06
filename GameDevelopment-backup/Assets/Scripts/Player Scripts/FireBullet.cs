using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullet : MonoBehaviour
{
    private float speed = 4f;
    private Animator anim;

    private bool canMove;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        canMove = true;
        StartCoroutine(DisableBullet(5f));
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
    //this is going to move are game object 
    private void Move()
    {

        if (canMove)
        {
            //store the transform position in a temp position then we add the speed
            // time.deltatime is used to smooth movement in the update function
            Vector3 temp = transform.position;
            temp.x += speed * Time.deltaTime;
            transform.position = temp;
        }
       
    }

    //this is my accesser for Speed
    public float Speed
    {
        get
        {
            return speed;
        }
        set
        {
            speed = value;
        }
    }

    IEnumerator DisableBullet(float timer)
    {
        yield return new WaitForSeconds(timer);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == MyTags.BEETLE_TAG || collision.gameObject.tag == MyTags.SNAIL_TAG)
        {
            anim.Play("Explode");
            canMove = false;
            StartCoroutine(DisableBullet(0.1f));
        }
    }

}
