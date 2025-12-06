using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeController : MonoBehaviour
{
    [Header("Fade Settings")]
    public Image image; 
    public float defaultDuration = 1f;

    void Start()
    {
        StartCoroutine(FadeOut(1f));
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

    public IEnumerator FadeOut( float duration)
    {
        float time = 0f;
        Color color = image.color;
        while (time < 1)
        {
            time += Time.deltaTime;
            color.a = 1f - (time / 1f);
            image.color = color;
            yield return null;
        }
    }
}
