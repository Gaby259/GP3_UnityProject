using UnityEngine;

public class ControlsMenuState : IGameStates    
{
    private GameStateManager _gameStateManager;
    private GameObject _controlsUI;

    public ControlsMenuState(GameStateManager gameStateManager, GameObject controlsUI)
    {
        _gameStateManager = gameStateManager;
        _controlsUI = controlsUI;
    }
    public void Enter()
    {
        Time.timeScale = 0f;
        _controlsUI.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Debug.Log("Entered CONTROLS MENU state");
    }

    void IGameStates.Update()
    {
        //Nothing Here
    }

    public void Exit()
    {
       _controlsUI.SetActive(false);
       Debug.Log("Exited CONTROLS MENU state");
    }

    public void BackToPauseMenu()
    {
        _gameStateManager.ChangeState(_gameStateManager.PausedState);
    }
}
