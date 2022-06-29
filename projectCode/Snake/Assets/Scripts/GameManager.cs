using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField]
    private Snake snake;
    private int score;

    [SerializeField]
    private GameObject gameOverPanel;
    [SerializeField]
    private Text scoreText;
    [SerializeField]
    private Text highScoreText;

    private void Awake()
    {
        if (Instance == null) // establish singleton pattern
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    private void Start()
    {
        NewRound();
    }

    public void NewRound()
    {
        Time.timeScale = 1;
        snake.ResetState();
        score = 0;
        gameOverPanel.SetActive(false);
    }

    public void GameOver()
    {
        Time.timeScale = 0;

        CheckStats();

        gameOverPanel.SetActive(true);
    }

    public void IncreaseScore()
    {
        score++;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        scoreText.text = "SCORE: " + score.ToString();
    }

    private void CheckStats()
    {
        if (!PlayerPrefs.HasKey("SnakeHighScore")) // if high score doesnt exist, initialize to zero
        {
            PlayerPrefs.SetInt("SnakeHighScore", 0);
        }

        if (PlayerPrefs.GetInt("SnakeHighScore") < score) // if score greater than high score, high score becomes score
        {
            PlayerPrefs.SetInt("SnakeHighScore", score);
        }

        highScoreText.text = "HIGH SCORE: " + PlayerPrefs.GetInt("SnakeHighScore").ToString();
        UpdateScoreText();
    }
}
