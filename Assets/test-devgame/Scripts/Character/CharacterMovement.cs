using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 4f; // Скорость движения
    public float turnSpeed = 180f; // Скорость поворота
    private float _currentSpeed;

    private Rigidbody2D _rigidbody2D;
    private Vector2 _moveInput;
    private Vector2 _lookInput;

    private Camera _mainCamera;

    private PlayerControls _playerControls;

    private void Awake()
    {
        _playerControls = new();
        
        _rigidbody2D = GetComponent<Rigidbody2D>();
        
        _mainCamera = Camera.main;

        ResetSpeed();
    }

    private void OnEnable()
    {
        _playerControls.Character.Enable();

        _playerControls.Character.Move.performed += OnMovePerformed;
        _playerControls.Character.Move.canceled += OnMoveCanceled;

        _playerControls.Character.Look.performed += OnLookPerformed;
        _playerControls.Character.Look.canceled += OnLookCanceled;
    }

    private void OnDisable()
    {
        _playerControls.Character.Disable();

        _playerControls.Character.Move.performed -= OnMovePerformed;
        _playerControls.Character.Move.canceled -= OnMoveCanceled;

        _playerControls.Character.Look.performed -= OnLookPerformed;
        _playerControls.Character.Look.canceled -= OnLookCanceled;
    }

    private void FixedUpdate()
    {
        // Поворот игрока в сторону курсора
        Vector2 direction = _lookInput - (Vector2)transform.position;
        // Рассчитываем угол к курсору
        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        
        // Линейное изменение угла
        float currentAngle = _rigidbody2D.rotation;
        float angle = Mathf.MoveTowardsAngle(currentAngle, targetAngle, turnSpeed * Time.deltaTime);
        _rigidbody2D.rotation = angle;

        // Движение игрока
        Vector2 moveDirection = _moveInput.normalized * (_currentSpeed * Time.deltaTime);
        _rigidbody2D.MovePosition(_rigidbody2D.position + moveDirection);
    }

    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
    }

    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        _moveInput = Vector2.zero;
    }

    private void OnLookPerformed(InputAction.CallbackContext context)
    {
        _lookInput = context.ReadValue<Vector2>();
        _lookInput = _mainCamera.ScreenToWorldPoint(new Vector3(_lookInput.x, _lookInput.y, _mainCamera.nearClipPlane));
    }

    private void OnLookCanceled(InputAction.CallbackContext context)
    {
        _lookInput = Vector2.zero;
    }
    
    // Метод для модификации скорости
    public void ModifySpeed(float speedFactor)
    {
        _currentSpeed = moveSpeed * speedFactor;
    }

    // Метод для сброса скорости к базовому значению
    public void ResetSpeed()
    {
        _currentSpeed = moveSpeed;
    }
}
