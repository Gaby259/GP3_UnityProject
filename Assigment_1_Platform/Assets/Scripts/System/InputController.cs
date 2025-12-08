using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    private PlayerInput _playerInput;
    private GameControls _gameControls;
    
   
    
    //Player Controls
    public event Action JumpEvent;
    public event Action<Vector2> MoveEvent;
    public event Action DashEvent;
    public event Action ShieldActivateEvent;    
    public event Action ShieldDeactivateEvent;
    public event Action StartAttackEvent;
    public event Action EndAttackEvent;
    public event Action HealEvent;
    
    public event Action PauseEvent;
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
        _gameControls.Player.Dash.performed += OnDashPerformed;
        _gameControls.Player.Attack.performed += OnAttackPerformed;
        _gameControls.Player.Attack.canceled += OnAttackCancelled;
        _gameControls.Player.Heal.performed += OnHealPerformed;
        _gameControls.Player.Shield.performed += OnShieldPerformed;
        _gameControls.Player.Shield.canceled += OnShieldCancelled;
        _gameControls.Player.Pause.performed += OnPausePerformed;


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

    private void OnDashPerformed(InputAction.CallbackContext context)
    {
        DashEvent?.Invoke();
    }
    
    private void OnShieldPerformed(InputAction.CallbackContext context)
    {
        ShieldActivateEvent?.Invoke();
    }

    private void OnShieldCancelled(InputAction.CallbackContext context)
    {
        ShieldDeactivateEvent?.Invoke();
    }
    
    private void OnAttackPerformed(InputAction.CallbackContext context)
    {
        StartAttackEvent?.Invoke();
    }

    private void OnAttackCancelled(InputAction.CallbackContext context)
    {
        EndAttackEvent?.Invoke();
    }
    private void OnHealPerformed(InputAction.CallbackContext context)
    {
       HealEvent?.Invoke();
    }

    private void OnPausePerformed(InputAction.CallbackContext context)
    {
        PauseEvent?.Invoke();
    }
    
}
