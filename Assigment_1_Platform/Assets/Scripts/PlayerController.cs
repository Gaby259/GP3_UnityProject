using System;
using System.Linq;
using UnityEngine;
using Random = System.Random;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    private InputController _inputController;
    private CharacterController _characterController;
    
    [Header("Movement")]
    [SerializeField] LayerMask groundLayer;
    [SerializeField] private PlayerControllerConfig controllerConfig;
    private Vector2 _moveInput;
    private Vector3 _currentVelocity;
    private bool _isGrounded;
    private bool _canMove = true;
    
    [Header("Dash")]
    private bool _isDashing = true;
    private bool _hasAirDashed = false;
    private Vector3 _dashDirection;
    private float _dashTimeLeft = 0f;
    private float _dashCooldownLeft = 0f;
    private bool _canDash = true;

     

   void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _inputController = GetComponent<InputController>();
    }

    void OnEnable()
    {
        if (_inputController != null) 
        {
            _inputController.MoveEvent += MovementInput;
            _inputController.JumpEvent += JumpInput;
            _inputController.DashEvent +=DashPressed;
        }
    }
    private void OnDisable() // this is for handling the error MissingReferenceException
    {
        if (_inputController != null)
        {
            _inputController.MoveEvent -= MovementInput;
            _inputController.JumpEvent -= JumpInput;
            _inputController.DashEvent -= DashPressed;
        }
    }

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    

    private void Update()
    {
        // Resetea el dash cuando toca el suelo
        if (IsGrounded() && !_canDash)
        {
            _canDash = true;
            _hasAirDashed = false;
        }

        HandleDash();
        if (_canMove)
        {
            Movement();
            Jump();
        }
    }


    private void MovementInput (Vector2 movement)
    {
       _moveInput = movement; 
    }

    private void Movement()
    {
        Vector3 targetDirection = transform.right * _moveInput.x;
        Vector3 targetVelocity = targetDirection * controllerConfig.MovementSpeed;

        // Si detectas pared, corta la velocidad horizontal
        if (Physics.Raycast(transform.position, targetDirection, out RaycastHit hit, 0.6f, groundLayer))
        {
            if (hit.collider.CompareTag("Wall") && Mathf.Abs(_moveInput.x) > 0) // if player touches the wall velocity = 0
            {
                _currentVelocity.x = 0;
            }
        }

        float acceleration = IsGrounded() ? controllerConfig.groundAcceleration : controllerConfig.AirAcceleration;

        // Si el input es contrario a la velocidad, dobla la aceleración
        if (_moveInput.x != 0 && _moveInput.x * _currentVelocity.x < 0)
            acceleration *= controllerConfig.accelerationMultiplier;

        _currentVelocity = Vector3.MoveTowards(_currentVelocity, targetVelocity, acceleration * Time.deltaTime);

        Vector3 horizontalFinalVelocity = new Vector3(_currentVelocity.x, 0, _currentVelocity.z);

        // Deceleración si no hay input
        if (targetDirection == Vector3.zero)
        {
            horizontalFinalVelocity = Vector3.MoveTowards(horizontalFinalVelocity, 
                Vector3.zero, controllerConfig.groundDeceleration * Time.deltaTime);
        }

        _currentVelocity.x = horizontalFinalVelocity.x;
        _currentVelocity.z = horizontalFinalVelocity.z;
    }
    
    private void JumpInput()
    {
        
        if (IsGrounded())
        {
            _currentVelocity.y = controllerConfig.jumpHeight;
        }
        
    }

    private bool IsGrounded()
    {
        _isGrounded = Physics.SphereCast(transform.position, .5f, Vector3.down, out RaycastHit hit, .6f, groundLayer);
        return _isGrounded;
    }

    private void Jump()
    {
        if (!IsGrounded()) //if the player is not touching the floor do this...
        {
            _currentVelocity.y += Physics.gravity.y * controllerConfig.gravity *Time.deltaTime;
        }
        _characterController.Move(_currentVelocity * Time.deltaTime);
    
    }

    private void DashPressed()
    {
        if (!_canDash || _dashCooldownLeft > 0f) return;

        if (!IsGrounded())
        {
            if (!controllerConfig.allowAirDash || _hasAirDashed) return;
            _hasAirDashed = true;
        }

        _isDashing = true;
        _canDash = false;
        _dashTimeLeft = controllerConfig.dashDuration;
        _dashCooldownLeft = controllerConfig.dashCooldown;
    
        Vector3 dashInput = transform.right * _moveInput.x;
        if (dashInput == Vector3.zero) dashInput = transform.right;
        Debug.DrawRay(transform.position, _dashDirection * 5f, Color.red);

        _dashDirection = dashInput.normalized;

        Debug.Log("Dash started");
    }
    private void HandleDash()
    {
        // Resets the dash if the player touches the ground
        if (IsGrounded() && !_canDash) //Player is in the ground but CAN'T dash
        {
            _canDash = true;
            _hasAirDashed = false; //Resets the air Dash
        }
        //Dash Cooldown
        if (_dashCooldownLeft > 0f)
        {
            _dashCooldownLeft -= Time.deltaTime;
        }
        //The dash is activated
        if (_canDash && _isDashing)
        {
            _characterController.Move(_dashDirection * controllerConfig.dashSpeed * Time.deltaTime);
            _dashTimeLeft -= Time.deltaTime;
            if (_dashTimeLeft <= 0f)
            {
                _isDashing = false;
            }
        }
    }
   
}