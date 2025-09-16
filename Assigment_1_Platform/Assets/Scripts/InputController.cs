uusing System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    private GameControls _gameControls;
    
    //Player Controls
    public event Action JumpEvent;
    public event Action<Vector2> MoveEvent;
    public event Action<Vector2> MouseLookEvent;

    public event Action AttackEvent;
    public event Action AttackEventCancelled;
    
    public event Action InteractEvent;
    public event Action ShieldEvent;
    public event Action ShieldEventCancelled;
    private void Awake()
    {
        _gameControls = new GameControls();
    }

    private void OnEnable()
    {
        _gameControls.Player.Enable();
        
        _gameControls.Player.Move.performed += OnMovePerformed;
        _gameControls.Player.Move.canceled += OnMoveCancelled;
        _gameControls.Player.Jump.performed += OnJumpPerformed; //performed a jump call this function
        _gameControls.Player.Look.performed += OnMouseMove;
        _gameControls.Player.Look.canceled += OnMouseMoveCancelled;
        _gameControls.Player.Attack.performed += OnAttackPerformed;
        _gameControls.Player.Attack.canceled += OnAttackCancelled;
        _gameControls.Player.Interact.performed += OnInteractPerformed;
        _gameControls.Player.Shield.performed += OnShieldPerformed;
        _gameControls.Player.Shield.canceled += OnShieldCancelled;
    }

    private void OnShieldPerformed(InputAction.CallbackContext context)
    {
        ShieldEvent?.Invoke();
    }

    private void OnShieldCancelled(InputAction.CallbackContext context)
    {
       ShieldEventCancelled?.Invoke();
    }
    
    private void OnInteractPerformed(InputAction.CallbackContext context)
    {
        InteractEvent?.Invoke();
    }
    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        MoveEvent?.Invoke(context.ReadValue<Vector2>()); //excellent
    }

    private void OnMoveCancelled(InputAction.CallbackContext context)
    {
        MoveEvent?.Invoke(Vector2.zero);
    }

    private void OnJumpPerformed(InputAction.CallbackContext context)
    {
        JumpEvent?.Invoke();
        
    }

    private void OnMouseMove(InputAction.CallbackContext context)
    {
       MouseLookEvent?.Invoke(context.ReadValue<Vector2>());
    }
    private void OnMouseMoveCancelled(InputAction.CallbackContext context)
    {
        MouseLookEvent?.Invoke(Vector2.zero);
    }
    private void OnAttackPerformed(InputAction.CallbackContext context)
    {
        AttackEvent?.Invoke();
    }
    
    private void OnAttackCancelled(InputAction.CallbackContext context)
    {
        AttackEventCancelled?.Invoke();
    }
    
    
}
