using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndScreenController : MonoBehaviour
{
    [SerializeField] private Button mainMenuButton;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        mainMenuButton.onClick.AddListener(ReturnToMainMenu);
    }

    private void ReturnToMainMenu()
    {
        FadeController fade = FindObjectOfType<FadeController>();
        if (fade != null)
            StartCoroutine(ReturnRoutine(fade));
        else
            SceneManager.LoadScene("MainMenu");
    }

    private IEnumerator ReturnRoutine(FadeController fade)
    {
        yield return fade.FadeIn(1f);
        SceneManager.LoadScene("MainMenu");
    }
}