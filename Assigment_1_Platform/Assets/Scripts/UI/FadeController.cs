using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeController : Singleton<FadeController>
{
    public static FadeController Instance;
    
    [Header("Fade Settings")]
    public Image image; 
    public float defaultDuration = 1f;
    
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StartCoroutine(FadeOut(defaultDuration));
    }
    
     public IEnumerator FadeIn( float duration)
    {
        float time = 0f;
        Color color = image.color;
        while (time < duration)
        {
            time += Time.deltaTime;
            color.a = time / duration;
            image.color = color;
            yield return null;
        }
    }

    public IEnumerator FadeOut(float duration)
    {
        float time = 0f;
        Color color = image.color;
        while (time < 1)
        {
            time += Time.deltaTime;
            color.a = 1f - (time / duration);
            image.color = color;
            yield return null;
        }
        color.a = 0f;
        image.color = color;
    }
    
    // Instance by which can be called in other scripts :)
    public void FadeInDefault() => StartCoroutine(FadeIn(defaultDuration));
    public void FadeOutDefault() => StartCoroutine(FadeOut(defaultDuration));
}
