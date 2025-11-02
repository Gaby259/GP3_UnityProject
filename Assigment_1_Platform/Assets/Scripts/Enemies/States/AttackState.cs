 using UnityEngine;

public class AttackState : IState
{
    private StateManager _stateManager;
    private IAttackBehavior _attackBehavior;

    public AttackState (StateManager stateManager)
    {
        _stateManager = stateManager;
        // Try to get any IAttackBehavior attached to this GameObject
        _attackBehavior = stateManager.GetComponent<IAttackBehavior>();
    }
    public void OnEnter()
    {
        Debug.Log("Entered Attack State");
    }

    public void UpdateState()
    {
        // If there’s no attack behavior or player checker, bail out
        if (_attackBehavior == null || _stateManager.PlayerChecker == null)
        {
            Debug.LogWarning("No Attack Behavior found on " + _stateManager.name);
            return;
        }

        // If the player leaves range, go back to Chase or Patrol
        if (!_stateManager.PlayerChecker.IsPlayerInRange())
        {
            _stateManager.ChangeState(_stateManager.ChaseState);
            return;
        }

        // Otherwise, try to attack
        _attackBehavior.TryAttack(_stateManager);
    }

    public void FixedUpdateState()
    {
    }

    public void OnExit()
    {
        Debug.Log("Exited Attack State");
    }
}