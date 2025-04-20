using UnityEngine;
using UnityEngine.UI;

public class Coin : MonoBehaviour
{
    private Text coinTextScore;
    private AudioSource audioManager;
    private int scoreCount = 0;

    private void Awake()
    {
        audioManager = GetComponent<AudioSource>();
    }
    private void Start()
    {
        coinTextScore = GameObject.Find("CoinText").GetComponent<Text>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == MyTags.COIN_TAG)
        {
            other.gameObject.SetActive(false);
            scoreCount++;
            coinTextScore.text = "x" + scoreCount;
            audioManager.Play();
        }
    }
}
