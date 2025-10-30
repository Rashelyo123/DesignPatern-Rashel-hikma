using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI coinText;
    public float score;

    public float scorePerSecond = 10f;
    public int coinScore = 10;
    private bool isRunning = true;
    void Awake()
    {
        instance = this;
    }
    void Update()
    {
        if (isRunning)
        {
            score += scorePerSecond * Time.deltaTime;
            scoreText.text = "Score: " + Mathf.FloorToInt(score).ToString();
        }
    }
    public void AddCoin()
    {
        score += coinScore;
        coinText.text = "Coins: " + Mathf.FloorToInt(score).ToString();
    }

    public void StopScore()
    {
        isRunning = false;
    }

}
