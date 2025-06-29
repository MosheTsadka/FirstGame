using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;
    
    public event Action OnGameStart;
    public event Action OnGameOver;
    public event Action OnGameRestart;

    private bool _isGameActive = false;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        StartGame();
    }

    public void StartGame()
    {
        _isGameActive = true;
        OnGameStart?.Invoke();
    }

    public void GameOver()
    {
        _isGameActive = false;
        OnGameOver?.Invoke();
    }

    public void RestartGame()
    {
        _isGameActive = true;
        OnGameRestart?.Invoke();
    }
}
