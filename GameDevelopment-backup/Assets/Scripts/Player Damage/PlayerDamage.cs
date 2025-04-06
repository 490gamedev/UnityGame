using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerDamage : MonoBehaviour
{
    private Text lifeText;
    private int lifeScoreCount;

    private bool canDamage;

    public PlayerMovement playerMovement;
    private SpriteRenderer playerSprite;

    private void Awake()
    {
        lifeText = GameObject.Find("LifeText").GetComponent<Text>();
        lifeScoreCount = 6;
        lifeText.text = "x" + lifeScoreCount;

        playerMovement = GetComponent<PlayerMovement>();
        playerSprite = GetComponent<SpriteRenderer>();
        canDamage = true;
    }

    public void DealDamage()
    {
        if (canDamage)
        {
            lifeScoreCount--;

            if (lifeScoreCount >= 0)
            {
                lifeText.text = "x" + lifeScoreCount;
            }

            if(lifeScoreCount == 0)
            {
                playerMovement.KillPlayer();

            }
            StartCoroutine(FlashPlayer());
            canDamage = false;
            StartCoroutine(WaitForDamage());
        }
    }

    IEnumerator WaitForDamage()
    {
        yield return new WaitForSeconds(2f);
        canDamage = true;

    }

    //makes the player blink 5 times over 1 second
    IEnumerator FlashPlayer()
    {
        for(int i = 0; i < 5; i++)
        {
            playerSprite.enabled = false;
            yield return new WaitForSeconds(0.1f);
            playerSprite.enabled = true;
            yield return new WaitForSeconds(0.1f);

        }
    }

}
