using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinScript : MonoBehaviour
{
    private Text coinTextScore;
    private AudioSource audioManager;
    private int scoreCount = 0;

    private void Awake()
    {
        audioManager = GetComponent<AudioSource>();
    }

    void Start()
    {
        //this would get us the game object with that name it doesn't look for the tag 
        coinTextScore = GameObject.Find("CoinText").GetComponent<Text>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == MyTags.COIN_TAG)
        {
            collision.gameObject.SetActive(false);
            scoreCount++;

            coinTextScore.text = "x" + scoreCount;

            audioManager.Play();
        }
    }
}
