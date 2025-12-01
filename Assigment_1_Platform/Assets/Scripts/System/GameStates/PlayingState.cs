using UnityEngine;

public class PlayingState : IGameStates
{
    private GameStateManager _manager;

    public PlayingState(GameStateManager manager)
    {
        _manager = manager;
    }

    public void Enter()
    {
        Time.timeScale = 1;

        _manager.Input.PauseEvent += OnPausePressed;

        Debug.Log("Entered PLAYING state");
    }

    public void Update()
    {
        //Do noting
    }

    public void Exit()
    {
        _manager.Input.PauseEvent -= OnPausePressed;

        Debug.Log("Exited PLAYING state");
    }

    private void OnPausePressed()
    {
        _manager.ChangeState(_manager.PausedState);
    }
}