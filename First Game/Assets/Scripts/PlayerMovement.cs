using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float maxSpeed = 5f;
    [SerializeField] private float acceleration = 5f;
    [SerializeField] private float minX = -2.5f;
    [SerializeField] private float maxX = 2.5f;
    [SerializeField] private Transform startPos;

    private float _currentSpeed = 0f;
    private bool _isMobile;


    private void Init()
    {
        transform.position = startPos.position;
    }

    private void Start()
    {
        Init();
        _isMobile = Application.isMobilePlatform;
    }

    private void Update()
    {
        float inputX = GetInput();
        float targetSpeed = inputX * maxSpeed;
        _currentSpeed = Mathf.MoveTowards(_currentSpeed, targetSpeed, acceleration * Time.deltaTime);
        MovePlayer();
    }

    private float GetInput()
    {
        return _isMobile ? Input.acceleration.x : Input.GetAxis("Horizontal");
    }

    private void MovePlayer()
    {
        Vector3 newPosition = transform.position + Vector3.right * _currentSpeed * Time.deltaTime;
        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
        transform.position = newPosition;
    }
}