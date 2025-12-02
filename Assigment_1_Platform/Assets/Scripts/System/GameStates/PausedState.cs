using UnityEngine;

public class PausedState : IGameStates
{
    private GameStateManager _manager;
    private GameObject _pauseMenuUI;

    public PausedState(GameStateManager manager, GameObject ui)
    {
        _manager = manager;
        _pauseMenuUI = ui;
    }

    public void Awake()
    {
        _pauseMenuUI.SetActive(false);
    }
    public void Enter()
    {
        Time.timeScale = 0;
        _pauseMenuUI.SetActive(true);
        _manager.Input.PauseEvent += OnPausePressed;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Update()
    {
       //Do nothing
    }

    public void Exit()
    {
        _pauseMenuUI.SetActive(false);
        _manager.Input.PauseEvent -= OnPausePressed;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnPausePressed()
    {
        _manager.ChangeState(_manager.PlayingState);
    }
}