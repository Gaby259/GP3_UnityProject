using UnityEngine;
using UnityEngine.UI;

public class ControlsMenu : MonoBehaviour
{
    [SerializeField] private Button backButton;

    private void Start()
    {
        backButton.onClick.AddListener(() =>
        {
            GameStateManager.Instance.ControlsMenuState.BackToPauseMenu();
        });
    }
}
