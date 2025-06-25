using System;
using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int health = 3;
    [SerializeField] private bool isAlive;
    
    [SerializeField] private TMP_Text healthText;
    
    private int _currentHealth;

    private void Init()
    {
        _currentHealth = health;
    }

    private void Awake()
    {
        GameManager.Instance.OnGameStart += Init;
        GameManager.Instance.OnGameRestart += Init;
    }

    void Start()
    {
        isAlive = true;
        healthText.text = "HP: " + _currentHealth.ToString();
    }
    
    void Update()
    {
        if (_currentHealth > 0 || !isAlive) return;
        
        isAlive = false;
        GameOver();
        GameManager.Instance.OnGameOver += GameOver;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Obstacle"))
        {
            _currentHealth -= 1;
            SetTextHealth();
        }
    }

    private void SetTextHealth()
    {
        healthText.text = "HP: " + _currentHealth.ToString();
    }

    private void GameOver()
    {
        
    }
}
