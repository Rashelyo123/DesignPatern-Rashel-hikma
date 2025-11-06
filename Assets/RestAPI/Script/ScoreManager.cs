using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI coinText;
    public string playerName = "Player";

    public float score;
    public int coins = 0;

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
        coins++;
        score += coinScore;
        coinText.text = "Coins: " + coins.ToString();
    }

    public void StopScore()
    {
        isRunning = false;
    }
}
