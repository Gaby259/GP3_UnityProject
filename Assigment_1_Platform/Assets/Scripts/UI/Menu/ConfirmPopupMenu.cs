using UnityEngine;
using UnityEngine.UI;

public class ConfirmPopupMenu : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button yesButton;
    [SerializeField] private Button noButton;

    private void Start()
    {
        // YES → Confirm exit to Main Menu
        yesButton.onClick.AddListener(() =>
        {
            GameStateManager.Instance.ConfirmReturnState.ConfirmExitToMainMenu();
        });

        // NO → Return to Pause Menu
        noButton.onClick.AddListener(() =>
        {
            GameStateManager.Instance.ChangeState(GameStateManager.Instance.PausedState);
        });
    }
}

