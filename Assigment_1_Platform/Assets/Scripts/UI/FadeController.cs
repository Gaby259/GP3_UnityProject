
using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CanvasGroup))]
public class FadeController : MonoBehaviour
{
    [Range(0.05f, 2f)] public float fadeDuration = 0.6f;
    public AnimationCurve ease = AnimationCurve.EaseInOut(0,0, 1,1); // suave
    CanvasGroup cg;

    void Awake() {
        cg = GetComponent<CanvasGroup>();
        cg.blocksRaycasts = true; // bloquea clics durante el fade
    }

    public IEnumerator FadeOut() // negro -> transparente
    {
        float t = 0f;
        float start = 1f, end = 0f;
        while (t < 1f) {
            t += Time.unscaledDeltaTime / fadeDuration;
            cg.alpha = Mathf.LerpUnclamped(start, end, ease.Evaluate(t));
            yield return null;
        }
        cg.alpha = 0f;
        cg.blocksRaycasts = false; // ya se puede clickear
    }

    public IEnumerator FadeIn() // transparente -> negro
    {
        cg.blocksRaycasts = true;
        float t = 0f;
        float start = 0f, end = 1f;
        while (t < 1f) {
            t += Time.unscaledDeltaTime / fadeDuration;
            cg.alpha = Mathf.LerpUnclamped(start, end, ease.Evaluate(t));
            yield return null;
        }
        cg.alpha = 1f;
    }
}