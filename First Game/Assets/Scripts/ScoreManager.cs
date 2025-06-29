using System;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }
    
    [SerializeField] private int score;
    [SerializeField] private TMP_Text scoreText;

    private void OnEnable()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        GameManager.Instance.OnGameStart += Init;
        GameManager.Instance.OnGameRestart += Init;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnGameStart -= Init;
        GameManager.Instance.OnGameRestart -= Init;
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
