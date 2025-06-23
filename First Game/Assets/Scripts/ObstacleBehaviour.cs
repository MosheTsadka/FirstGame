using System;
using UnityEngine;

public class ObstacleBehaviour : MonoBehaviour
{
    [SerializeField] private float baseSpeed;
    [SerializeField] private float multiplierSpeed;
    private float _speed;

    private void OnEnable()
    {
        _speed = baseSpeed + (Spawner.SpawnCount * multiplierSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 movement = Vector3.down * (_speed * Time.deltaTime);
        transform.position += movement;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("DestroyObstacle")) return;
        
        ScoreManager.Instance.AddScore(1);
        gameObject.SetActive(false);
    }
}
