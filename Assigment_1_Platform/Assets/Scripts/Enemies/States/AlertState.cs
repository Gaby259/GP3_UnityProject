using UnityEngine;

public class AlertState : IState

{
    private StateManager _stateManager;

    public AlertState (StateManager stateManager)
    {
        _stateManager = stateManager;
    }
    public void OnEnter()
    {
        throw new System.NotImplementedException();
    }

    public void UpdateState()
    {
        throw new System.NotImplementedException();
    }

    public void FixedUpdateState()
    {
        throw new System.NotImplementedException();
    }

    public void OnExit()
    {
        throw new System.NotImplementedException();
    }
}