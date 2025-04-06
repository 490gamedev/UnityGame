using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //this will call the deactivate function to deactivate are game object 
        Invoke("Deactivate", 4f);
        
    }

    void Deactivate()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == MyTags.PLAYER_TAG)
        {
            collision.GetComponent<PlayerDamage>().DealDamage();
            gameObject.SetActive(false);
        }
    }
}
