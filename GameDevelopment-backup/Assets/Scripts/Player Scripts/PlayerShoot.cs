using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{

    public GameObject fireBullet;

    // Update is called once per frame
    void Update()
    {
        ShootBullet(); 
    }
    void ShootBullet()
    {
        //have to press the X key button in order to work
        if (Input.GetKeyDown(KeyCode.C))
        {
            //this would create a copy of the fire bullet
            //Quaternion.identity short cut of writing 0,0,0 for the rotation 
            GameObject bullet = Instantiate(fireBullet, transform.position,Quaternion.identity);

            /* multiplying the speed value which is positive to are local scale X
             * the reason is because the speed value is positive and won't accept negative which
             * is why were multiplying it by localScale.X that way it could use positive or negative
            */
            bullet.GetComponent<FireBullet>().Speed *= transform.localScale.x;
                
            
        }
    }
}
