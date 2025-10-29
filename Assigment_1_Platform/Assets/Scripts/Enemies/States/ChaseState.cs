using UnityEngine;

public class ChaseState : IState
{
    private StateManager _stateManager;

    public ChaseState  (StateManager stateManager)
    {
        _stateManager = stateManager;
    }
    public void OnEnter()
    {
        Debug.Log("Chase State Enter");
    }

    public void UpdateState()
    {
       
    }

    public void FixedUpdateState()
    {
        if (_stateManager.PlayerChecker.IsPlayerInRange())
        {
            _stateManager.CharacterMover.MoveTo(_stateManager.PlayerChecker.GetPlayerPosition());
        }
        else
        {
            _stateManager.ChangeState(_stateManager.PatrolState);
        }
    }

    public void OnExit()
    {
        Debug.Log("Chase State Exit");
    }
}