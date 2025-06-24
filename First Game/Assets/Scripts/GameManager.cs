using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;

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

    public void RestartGame()
    {
        Time.timeScale = 1;
    }

    public void GameOver()
    {
        Debug.Log("Game Over");
        Time.timeScale = 0;
    }
}
