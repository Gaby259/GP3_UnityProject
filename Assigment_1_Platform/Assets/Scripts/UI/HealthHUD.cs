using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthHUD : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] private PlayerHealth playerHealth;   // auto-find if null
    [SerializeField] private Transform heartsParent;      // panel con HorizontalLayoutGroup
    [SerializeField] private Image heartPrefab;           // prefab azul de Assets (Image)

    [Header("Sprites")]
    [SerializeField] private Sprite fullHeart;
    [SerializeField] private Sprite emptyHeart;

    [Header("Layout")]
    [SerializeField] private bool rightToLeft = true;

    private readonly List<Image> _hearts = new List<Image>();

    private void Awake()
    {
        if (playerHealth == null) playerHealth = FindObjectOfType<PlayerHealth>();
    }

    private void OnEnable()
    {
        if (!HasAllRefs()) return;

        RebuildHearts(playerHealth.MaxHealth);

        playerHealth.OnHealthChanged += UpdateHeartsDisplay;
        playerHealth.OnPlayerDeath  += HandlePlayerDeath;

        UpdateHeartsDisplay(playerHealth.CurrentHealth, playerHealth.MaxHealth);
    }

    private void OnDisable()
    {
        if (playerHealth == null) return;
        playerHealth.OnHealthChanged -= UpdateHeartsDisplay;
        playerHealth.OnPlayerDeath  -= HandlePlayerDeath;
    }

    private bool HasAllRefs()
    {
        if (playerHealth == null || heartsParent == null || heartPrefab == null || fullHeart == null || emptyHeart == null)
        {
            Debug.LogWarning("[HealthHUD] Missing refs (PlayerHealth/HeartsParent/HeartPrefab/Sprites).");
            return false;
        }
        return true;
    }

    private void RebuildHearts(int max)
    {
        // 1) Borra TODO lo que haya dentro del panel (por si quedó algo a mano)
        for (int i = heartsParent.childCount - 1; i >= 0; i--)
            Destroy(heartsParent.GetChild(i).gameObject);

        _hearts.Clear();
        if (max <= 0) return;

        // 2) Crea N corazones
        for (int i = 0; i < max; i++)
        {
            // Instanciar y parentear conservando correctamente el RectTransform
            var img = Instantiate(heartPrefab);
            img.rectTransform.SetParent(heartsParent, false);

            // Normalizar rect (por si el prefab trae offsets raros)
            var rt = img.rectTransform;
            rt.anchorMin = rt.anchorMax = new Vector2(0.5f, 0.5f);
            rt.pivot = new Vector2(0.5f, 0.5f);
            rt.anchoredPosition = Vector2.zero;
            rt.localScale = Vector3.one;

            img.sprite = fullHeart;
            img.preserveAspect = true;

            _hearts.Add(img);
        }
    }

    private void UpdateHeartsDisplay(int current, int max)
    {
        if (_hearts.Count != max) RebuildHearts(max);

        for (int i = 0; i < _hearts.Count; i++)
        {
            int logicalIndex = rightToLeft ? (max - 1 - i) : i; // 0 = corazón más a la derecha si rightToLeft
            bool filled = logicalIndex < current;

            var img = _hearts[i];
            if (img != null) img.sprite = filled ? fullHeart : emptyHeart;
        }
    }

    private void HandlePlayerDeath()
    {
        // hook para UI de muerte si quieres
    }
}
