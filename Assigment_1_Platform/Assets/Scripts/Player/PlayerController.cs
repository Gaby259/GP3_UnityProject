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
    
    [Header("Rotation")]
    [SerializeField] private Transform modelPivot;  
    private Quaternion _modelRotation;
    private bool _facingRight = true; 
    
    [Header("Health")]
    private PlayerStamina _stamina;
    private PlayerHealth _health;
     

   void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _inputController = GetComponent<InputController>();
        _stamina = GetComponent<PlayerStamina>();  
        _health = GetComponent<PlayerHealth>();    
    }

    private void OnEnable()
    {
        if (_inputController != null) 
        {
            _inputController.MoveEvent += MovementInput;
            _inputController.JumpEvent += JumpInput;
            _inputController.HealEvent += HealPressed; 
            
        }
    }
    private void OnDisable() // this is for handling the error MissingReferenceException
    {
        
        if (_inputController != null)
        {
            _inputController.MoveEvent -= MovementInput;
            _inputController.JumpEvent -= JumpInput;
            _inputController.HealEvent -= HealPressed;
        }
    }

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        
        if (modelPivot)
            _modelRotation = modelPivot.localRotation; //takes the initial pose
    }
    

    private void Update()
    {
        // Pause or any other state will not have movement
        if (!IsGameplayActive())
        {
            _moveInput = Vector2.zero;
            _currentVelocity = Vector3.zero;
            return;
        }

        // if player is not using the shield move 
        if (!_canMove)
        {
            _moveInput = Vector2.zero;
            _currentVelocity = Vector3.zero;
            return;
        }

        //if the current state is the correct and is not using the shield move
        Movement();
        Jump();
        UpdateFacingRotation();
    }

    private bool IsGameplayActive()
    {
        return GameStateManager.Instance.CurrentState == GameStateManager.Instance.PlayingState;
    }

    private void MovementInput (Vector2 movement)
    {
       _moveInput = movement; 
    }

    private void Movement()
    {
        Vector3 targetDirection = transform.right * _moveInput.x;
        Vector3 targetVelocity = targetDirection * controllerConfig.MovementSpeed;

        // if player touches the wall stop the velocity 
        if (Physics.Raycast(transform.position, targetDirection, out RaycastHit hit, 0.6f, groundLayer))
        {
            if (hit.collider.CompareTag("Wall") && Mathf.Abs(_moveInput.x) > 0) // if player touches the wall velocity = 0
            {
                _currentVelocity.x = 0;
            }
        }

        float acceleration = IsGrounded() ? controllerConfig.groundAcceleration : controllerConfig.AirAcceleration;
        
        if (_moveInput.x != 0 && _moveInput.x * _currentVelocity.x < 0)
            acceleration *= controllerConfig.accelerationMultiplier;

        _currentVelocity = Vector3.MoveTowards(_currentVelocity, targetVelocity, acceleration * Time.deltaTime);

        Vector3 horizontalFinalVelocity = new Vector3(_currentVelocity.x, 0, _currentVelocity.z);

        // deceleration 
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
            SoundManager.PlaySFX("Jump");
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
          //  Debug.Log("Velocity "+_currentVelocity.y);
          
        }
        _characterController.Move(_currentVelocity * Time.deltaTime);
    
    }
    
    private void UpdateFacingRotation()
    {
        if (!modelPivot)
        {
            return;
        }
        // Read horizontal input
        float _inputX = _moveInput.x;
        if (Mathf.Abs(_inputX) < 0.01f)
        {
            return; // no movement intention
        }
        
        bool _wantRight = _inputX > 0f;
        // If player is already facing that direction, do nothing
        if (_wantRight == _facingRight)
        {
            return;
        }
        _facingRight = _wantRight;

        Quaternion _targetRotation;
       
        if (_facingRight)
        {
            _targetRotation = Quaternion.identity;
        }
        else
        {
            _targetRotation = Quaternion.AngleAxis(180f, modelPivot.up);
        }
        
        modelPivot.localRotation = _targetRotation;
        
    }
    
    private void HealPressed()
    {
        _stamina.Heal(_health);

    }

    public void SetMovementEnable(bool enable)
    {
        _canMove = enable;
        if (!enable)
        {
            _currentVelocity = Vector3.zero;
        }
    }

}