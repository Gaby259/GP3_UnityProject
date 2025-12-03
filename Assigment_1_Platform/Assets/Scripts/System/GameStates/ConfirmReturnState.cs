using UnityEngine;
using UnityEngine.SceneManagement;

public class ConfirmReturnState : IGameStates
{
    private GameStateManager _manager;
    private GameObject _popupUI;

    public ConfirmReturnState(GameStateManager manager, GameObject popupUI)
    {
        _manager = manager;
        _popupUI = popupUI;
    }

    public void Enter()
    {
        Time.timeScale = 0;
        _popupUI.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Update()
    {
      //Do nothing
    }

    public void Exit()
    {
        _popupUI.SetActive(false);
    }
    public void ConfirmExitToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
        _popupUI.SetActive(false);
    }
}
