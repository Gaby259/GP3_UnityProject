using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] FadeController fade;

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadRoutine(sceneName));
    }

    IEnumerator LoadRoutine(string sceneName)
    {
        yield return StartCoroutine(fade.FadeIn(1f));

        SceneManager.LoadScene(sceneName);

        yield return StartCoroutine(fade.FadeOut(1f));
    }
}