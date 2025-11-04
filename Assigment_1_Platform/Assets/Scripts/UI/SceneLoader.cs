using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneLoader : MonoBehaviour
{
    [Header("Config")]
    [Tooltip("Nombre de la escena de juego (añádela al Build Settings)")]
    public string gameplaySceneName = "Game"; 

    [Header("References")]
    public FadeController fade;      // arrastra tu FadeCanvas
    void Start() {
        if (fade != null) StartCoroutine(fade.FadeOut());
    }

    public void StartGame()
    {
        StartCoroutine(LoadGameRoutine());
    }

    IEnumerator LoadGameRoutine()
    {
        if (fade != null) yield return StartCoroutine(fade.FadeIn());
        AsyncOperation op = SceneManager.LoadSceneAsync(gameplaySceneName);
        op.allowSceneActivation = true; // activación directa tras fade
        yield return null;
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
    
}