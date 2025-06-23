using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int health = 3;
    [SerializeField] private bool isAlive;
    
    [SerializeField] private TMP_Text healthText;
    
    void Start()
    {
        isAlive = true;
        healthText.text = "HP: " + health.ToString();
    }
    
    void Update()
    {
        if (health > 0 || !isAlive) return;
        
        isAlive = false;
        GameOver();
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Obstacle"))
        {
            health -= 1;
            SetTextHealth();
        }
    }

    private void SetTextHealth()
    {
        healthText.text = "HP: " + health.ToString();
    }

    private void GameOver()
    {
        Debug.Log("Game Over");
        Time.timeScale = 0;
    }
}
