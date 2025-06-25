using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;
    
    public event Action OnGameStart;
    public event Action OnGameOver;
    public event Action OnGameRestart;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void GameStart()
    {
        OnGameStart?.Invoke();
    }

    private void GameOver()
    {
        OnGameOver?.Invoke();
    }

    private void GameRestart()
    {
        OnGameRestart?.Invoke();
    }
}
