using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button controlsButton;
    [SerializeField] private Button mainMenuButton;

    [Header("UI Panels")]
    [SerializeField] private GameObject confirmPopup; // BackToMainMenuPopup

    private void Start()
    {
        // Resume
        resumeButton.onClick.AddListener(() =>
        {
            GameStateManager.Instance.ChangeState(GameStateManager.Instance.PlayingState);
        });

        // Controls Menu
        controlsButton.onClick.AddListener(() =>
        {
           GameStateManager.Instance.ChangeState(GameStateManager.Instance.ControlsMenuState);
        });

        // Open Confirmation Popup
        mainMenuButton.onClick.AddListener(() =>
        {
            
            GameStateManager.Instance.ChangeState(GameStateManager.Instance.ConfirmReturnState);
            confirmPopup.SetActive(true);
        });
    }
}