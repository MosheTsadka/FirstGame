using System;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }
    
    [SerializeField] private int score;
    [SerializeField] private TMP_Text scoreText;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnGameStart += Init;
            GameManager.Instance.OnGameRestart += Init;
        }
        else
        {
            Debug.LogWarning("GameManager instance not found when ScoreManager started.");
        }
    }

    private void OnDisable()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnGameStart -= Init;
            GameManager.Instance.OnGameRestart -= Init;
        }
    }

    private void Init()
    {
        score = 0;
        scoreText.text = "Score: " + score.ToString();
    }

    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score.ToString();
    }
}
