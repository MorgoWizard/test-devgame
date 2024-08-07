using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterAttack : MonoBehaviour
{
    [SerializeField] private Weapon[] weapons;
    [SerializeField] private Weapon defaultWeapon;
    
    private Weapon _currentWeapon;
    private PlayerControls _playerControls;
    private Camera _mainCamera;

    private bool _isAttacking;

    private void Awake()
    {
        _playerControls = new PlayerControls();
        _mainCamera = Camera.main;

        EquipWeapon(defaultWeapon);
    }

    private void OnEnable()
    {
        _playerControls.Character.Enable();
        _playerControls.Character.Attack.performed += OnAttackPerformed;
        _playerControls.Character.Attack.canceled += OnAttackCanceled;
    }

    private void OnDisable()
    {
        _playerControls.Character.Disable();
        _playerControls.Character.Attack.performed -= OnAttackPerformed;
        _playerControls.Character.Attack.canceled -= OnAttackCanceled;
    }

    private void FixedUpdate()
    {
        if (_isAttacking)
        {
            Vector2 mousePosition = _playerControls.Character.Look.ReadValue<Vector2>();
            Vector2 worldPosition = _mainCamera.ScreenToWorldPoint(mousePosition);
            _currentWeapon.Attack(worldPosition);
        }
    }

    private void OnAttackPerformed(InputAction.CallbackContext context)
    {
        _isAttacking = true;
    }

    private void OnAttackCanceled(InputAction.CallbackContext context)
    {
        _isAttacking = false;
    }

    private void EquipWeapon(Weapon newWeapon)
    {
        foreach (var weapon in weapons)
        {
            if (weapon == newWeapon)
            {
                weapon.gameObject.SetActive(true);
                _currentWeapon = newWeapon;
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }
        }
    }
}
 