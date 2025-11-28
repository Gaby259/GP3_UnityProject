using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    private GameControls _gameControls;
    
    //Player Controls
    public event Action JumpEvent;
    public event Action<Vector2> MoveEvent;
    public event Action DashEvent;
    public event Action ShieldEvent;
    public event Action AttackEvent;

    public event Action HealEvent;
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
        _gameControls.Player.Heal.performed += OnHealPerformed;
        _gameControls.Player.Shield.performed += OnShieldPerformed;;


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
        ShieldEvent?.Invoke();
    }
    
    
    private void OnAttackPerformed(InputAction.CallbackContext context)
    {
        AttackEvent?.Invoke();
    }
    
    
    private void OnHealPerformed(InputAction.CallbackContext context)
    {
       HealEvent?.Invoke();
    }
    
    
}
