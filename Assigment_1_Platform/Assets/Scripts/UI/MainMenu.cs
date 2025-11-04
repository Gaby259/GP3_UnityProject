using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _quitButton;
    [SerializeField] private Button _yesButton;
    [SerializeField] private Button _noButton;

    [Header("UI References")]
    [SerializeField] private GameObject _quitConfirmationPopup;

    void Start()
    {
        // Start game
        _startButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("SampleScene");
        });

        // Show confirmation popup
        _quitButton.onClick.AddListener(() =>
        {
            _quitConfirmationPopup.SetActive(true);
        });

        // Close popup (No button)
        _noButton.onClick.AddListener(() =>
        {
            _quitConfirmationPopup.SetActive(false);
        });

        // Quit game (Yes button)
        _yesButton.onClick.AddListener(() =>
        {
            QuitGame();
        });
    }

    void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}