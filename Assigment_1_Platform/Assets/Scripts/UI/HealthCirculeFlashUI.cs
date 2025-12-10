using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthCircleFlashUI : MonoBehaviour
{
    public static HealthCircleFlashUI Instance;

    [SerializeField]private  Image circleImage;
    [SerializeField]private Color originalColor;
    [SerializeField]private Color flashColor;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        originalColor = circleImage.color;
    }

    public void FlashOnDamage()
    {
        StartCoroutine(FlashRoutine());
    }

    private IEnumerator FlashRoutine()
    {
        circleImage.color = flashColor;// flash
        yield return new WaitForSeconds(0.2f);
        circleImage.color = originalColor;
    }
}