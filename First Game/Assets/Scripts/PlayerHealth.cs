using System;
using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int health = 3;
    [SerializeField] private bool isAlive;

    [SerializeField] private TMP_Text healthText;

    private int _currentHealth;
    private bool _isDead;

    private GameManager _gm;

    private void Init()
    {
        _currentHealth = health;
        healthText.text = "HP: " + _currentHealth.ToString();
    }

    private void OnEnable()
    {
        GameManager.Instance.OnGameStart += Init;
        GameManager.Instance.OnGameRestart += Init;
        
        Debug.Log("GameManager is: " + GameManager.Instance.enabled);
    }

    private void OnDisable()
    {
        GameManager.Instance.OnGameStart -= Init;
        GameManager.Instance.OnGameRestart -= Init;
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        healthText.text = "HP: " + _currentHealth.ToString();
        
        if (_currentHealth <= 0)
        {
            _currentHealth = 0;
            GameManager.Instance.GameOver();
        }
    }

    /*void Update()
    {
        if (_currentHealth > 0 && isAlive) return;

        isAlive = false;
        GameOver();
        
        if (!isAlive) GameRestart();
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
        if (_isDead) return;
        
        _isDead = true;
        Debug.Log("Game Over");
        isAlive = false;
        
        Time.timeScale = 0;

        _gm.GameOver();

        //GameManager.Instance.OnGameOver += GameOver;
        //GameManager.Instance.GameOver();
    }

    private void GameRestart()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Time.timeScale = 1;
        }
    }*/
}