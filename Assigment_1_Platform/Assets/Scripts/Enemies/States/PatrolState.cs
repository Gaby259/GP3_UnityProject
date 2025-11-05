using UnityEngine;

public class PatrolState : IState
{
    private StateManager _stateManager;

    public PatrolState (StateManager stateManager)
    {
        _stateManager = stateManager;
    }
    public void OnEnter()
    {
        Debug.Log("Entering Patrol State");
    }

    public void UpdateState()
    {
        
    }

    public void FixedUpdateState()
    {
        //TODO: Add fixed update logic 
        // check for the player in range
        if (!_stateManager.PlayerChecker.IsPlayerInRange()) return;
        if(_stateManager.ChaseOnSight)
        {
            _stateManager.ChangeState(_stateManager.ChaseState);
        }
        else
        {
            _stateManager.ChangeState(_stateManager.Attack);
        }
    }

    public void OnExit()
    {
        Debug.Log("Exiting Patrol State");
    }
}