using System;
using UnityEngine;

public class SpawnerMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 3f;
    [SerializeField] private float minMaxX;
    [SerializeField] private Transform startPos;
    [SerializeField] private Vector3 pos;

    private int _dir;

    private void Init()
    {
        pos = startPos.position;
        _dir = 1;
    }

    private void OnEnable()
    {
        GameManager.Instance.OnGameStart += Init;
        GameManager.Instance.OnGameRestart += Init;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnGameStart -= Init;
        GameManager.Instance.OnGameRestart -= Init;
    }
    /*private void Start()
    {
        Init();
        _dir = 1;
    }*/

    void Update()
    {
        
        pos = transform.position;
        pos += Vector3.right * (movementSpeed * _dir * Time.deltaTime);

        if (pos.x > minMaxX)
        {
            _dir = -1;
        }

        if (pos.x < -minMaxX)
        {
            _dir = 1;
        }
        
        transform.position = pos;
    }
}