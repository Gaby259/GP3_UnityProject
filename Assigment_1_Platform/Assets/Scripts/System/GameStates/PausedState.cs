using Unity.VisualScripting;
using UnityEngine;

public class PausedState : IGameStates
{
    private GameStateManager _manager;

    public PausedState(GameStateManager manager)
    {
        _manager = manager;
    }

    public void Enter()
    {
        Time.timeScale = 0;

        _manager.Input.PauseEvent += OnPausePressed;

        Debug.Log("Pause menu Entered");
    }

    public void Update()
    {
        //Do nothing
    }

    public void Exit()
    {
        _manager.Input.PauseEvent -= OnPausePressed;

        Debug.Log("Pause menu Quit");
    }

    private void OnPausePressed()
    {
        _manager.ChangeState(_manager.PlayingState);
    }
}