using System;
using UnityEngine;

public class ObstacleBehaviour : MonoBehaviour
{
    /*[SerializeField] private float baseSpeed;
    [SerializeField] private float multiplierSpeed;*/
    [SerializeField] private int damage;
    [SerializeField] private float _speed;

    /*private void OnEnable()
    {
        _speed = baseSpeed + (Spawner.SpawnCount * multiplierSpeed);
    }*/

    public void SetSpeed(float newSpeed)
    {
        _speed = newSpeed;
    }

    private void Update()
    {
        Vector3 movement = Vector3.down * (_speed * Time.deltaTime);
        transform.position += movement;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();

            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
        }
        
        else if (other.CompareTag("DestroyObstacle"))
        {
            ScoreManager.Instance.AddScore(1);
            gameObject.SetActive(false);
        }
    }
}